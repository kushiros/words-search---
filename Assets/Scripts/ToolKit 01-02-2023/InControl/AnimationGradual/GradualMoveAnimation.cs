using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class GradualMoveAnimation : GradualAnimation
    {
        [SerializeField] Transform objective;
        [SerializeField] Transform destiny;
        protected override float SampleDistanceFromObjective()
        {
            float _distanceFromObjective = Mathf.Abs((destiny.position - objective.position).magnitude);
            return _distanceFromObjective;
        }

        protected override void UpdateAnimation(float _ammount)
        {
            objective.position += (destiny.position - objective.position).normalized * _ammount;
        }

    }
}