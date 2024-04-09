using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class GradualRotateAnimation : GradualAnimation
    {
        [SerializeField] Transform objective;
        [SerializeField] Transform destiny;
        protected override float SampleDistanceFromObjective()
        {
            float _degreesFromObjective = Mathf.Abs(Vector3.Angle(objective.forward, destiny.forward));
            return _degreesFromObjective;
        }

        protected override void UpdateAnimation(float _ammount)
        {
            objective.forward = Vector3.RotateTowards(objective.forward, destiny.forward, _ammount * Mathf.Deg2Rad, float.MaxValue);
        }

    }
}