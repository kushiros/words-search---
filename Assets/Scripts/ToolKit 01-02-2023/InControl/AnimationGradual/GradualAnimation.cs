using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace InControl
{

    public abstract class GradualAnimation : ToolsParent
    {

        protected bool isActive;

        [SerializeField] float easeCurveTopSpeed;
        [SerializeField][Min(0.0001f)] float easeCurveDistance;
        [BoundedCurve(1, 0, 0, 1, 1)] public AnimationCurve easeCurve;

        [SerializeField] protected eventOnReachedDestiny onReachDestiny;
        [System.Serializable]
        public struct eventOnReachedDestiny
        {
            public bool checkIfReachedDestiny;
            public float destinyMinDistance;
            public UnityEvent OnDestinyReached;
            public UnityEvent OnDestinyLost;
            public bool destinyReached;
            public eventOnReachedDestiny(bool check = false, float minDistance = 1)
            {
                checkIfReachedDestiny = check;
                destinyReached = false;
                destinyMinDistance = minDistance;
                OnDestinyReached = new UnityEvent();
                OnDestinyLost = new UnityEvent();
            }
        }
        [SerializeField] UnityEvent<float> UpdateDistance;


        protected override void _OnRunAction()
        {
            isActive = true;
        }

        public virtual void _Deactivate()
        {
            isActive = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isActive) CalculateAnimationUpdate();
        }

        private void CalculateAnimationUpdate()
        {
            float _distanceSample = SampleDistanceFromObjective();
            UpdateDistance?.Invoke(_distanceSample);
            float _easeCurvePoint = _distanceSample / easeCurveDistance;

            float _animateAmmount = easeCurve.Evaluate(Mathf.Clamp01(_easeCurvePoint)) * Time.deltaTime * easeCurveTopSpeed;
            if (_animateAmmount > _distanceSample) _animateAmmount = _distanceSample;
            UpdateAnimation(_animateAmmount);

            if (onReachDestiny.checkIfReachedDestiny)
                if (_distanceSample <= onReachDestiny.destinyMinDistance)
                {
                    if (!onReachDestiny.destinyReached)
                    {
                        onReachDestiny.OnDestinyReached?.Invoke();
                        onReachDestiny.destinyReached = true;
                    }
                }
                else
                {
                    if (onReachDestiny.destinyReached)
                    {
                        onReachDestiny.OnDestinyLost?.Invoke();
                        onReachDestiny.destinyReached = false;
                    }
                }
        }

        /// <summary>
        /// For better performance 1 should mean 1 unit of the desired movement
        /// </summary>
        /// <param name="ammount"></param>
        protected abstract void UpdateAnimation(float ammount);

        /// <summary>
        /// Checks every frame the distance left to reach the destination
        /// </summary>
        /// <returns></returns>
        protected abstract float SampleDistanceFromObjective();
    }
}