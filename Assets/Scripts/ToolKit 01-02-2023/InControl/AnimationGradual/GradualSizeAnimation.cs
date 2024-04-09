using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class GradualSizeAnimation : GradualAnimation
    {
        [SerializeField] Transform objective;
        [SerializeField] Transform destiny;
        protected override float SampleDistanceFromObjective()
        {
            float _distanceFromObjective = Mathf.Abs((destiny.localScale - objective.localScale).magnitude);
            return _distanceFromObjective;
        }

        protected override void UpdateAnimation(float _ammount)
        {
            objective.localScale += (destiny.localScale - objective.localScale).normalized * _ammount;
        }

    }
}