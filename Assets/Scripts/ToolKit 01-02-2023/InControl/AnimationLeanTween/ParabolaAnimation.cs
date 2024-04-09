using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace InControl
{
    public class ParabolaAnimation : ToolsParent
    {
        [SerializeField] float height = 10;
        [SerializeField] float timeAnim = 0.5f;
        [SerializeField] LeanTweenType ease = LeanTweenType.linear;

        [SerializeField] public Transform animationObjective;
        [SerializeField]public Transform finalPos;

        [SerializeField] public UnityEvent OnStartAnimation;
        [SerializeField] public UnityEvent OnFinishedAnimation;

        [SerializeField] MoveSettings moveSettings;
        [Serializable]
        public struct MoveSettings
        {
            public MoveSettings(bool _lookForward = true, float _lookForwardAmmount = 0.01f)
            {
                lookForward = _lookForward;
                lookForwardAmmount = _lookForwardAmmount;
            }
            public bool lookForward;
            public float lookForwardAmmount;
        }

        Vector3 startPoint;

        protected override void _OnRunAction()
        {
            AnimateToTransform(finalPos);
        }

        public void AnimateToTransform(Transform _objectivePos)
        {
            if (animationObjective == null) animationObjective = this.transform;
            finalPos = _objectivePos;
            OnStartAnimation?.Invoke();
            startPoint = animationObjective.position;
            LeanTween.value(gameObject, AnimationUpdate, 0, 1, timeAnim).setEase(ease).setOnComplete(OnAnimationEnd);
        }

        public void AnimationUpdate(float percentage)
        {
            animationObjective.position = VectorMath.Parabola(startPoint, finalPos.position, finalPos.position.y + height, percentage);

            if (moveSettings.lookForward)
            {
                Vector3 point2LookAt = VectorMath.Parabola(startPoint, finalPos.position, finalPos.position.y + height, Mathf.Clamp(percentage + moveSettings.lookForwardAmmount,0,1));
                animationObjective.rotation = Quaternion.LookRotation(point2LookAt - animationObjective.position);
            }
        }

        public void OnAnimationEnd()
        {
            OnFinishedAnimation?.Invoke();
        }
    }

}
