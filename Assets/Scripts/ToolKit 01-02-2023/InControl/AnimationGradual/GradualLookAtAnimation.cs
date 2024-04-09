using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class GradualLookAtAnimation : GradualAnimation
    {
        [SerializeField] Transform objective;
        [SerializeField] Transform destiny;

        [SerializeField] bool startLookingAtDestiny = false;

        protected override void _OnRunAction()
        {
            if (startLookingAtDestiny) objective.forward = destiny.position - objective.position;
        }


        protected override float SampleDistanceFromObjective()
        {
            Vector3 finalLookDirection = destiny.position - objective.position;
            float _degreesFromObjective = Mathf.Abs(Vector3.Angle(objective.forward, finalLookDirection));
            return _degreesFromObjective;
        }

        protected override void UpdateAnimation(float _ammount)
        {
            Vector3 finalLookDirection = destiny.position - objective.position;
            objective.forward = Vector3.RotateTowards(objective.forward, finalLookDirection, _ammount * Mathf.Deg2Rad, float.MaxValue);
        }

    }
}