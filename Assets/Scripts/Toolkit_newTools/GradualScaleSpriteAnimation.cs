using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class GradualScaleSpriteAnimation : GradualAnimation
    {
        [SerializeField] SpriteRenderer objective;
        [SerializeField] SpriteRenderer destiny;
        protected override float SampleDistanceFromObjective()
        {
            float _distanceFromObjective = Mathf.Abs((destiny.size - objective.size).magnitude);
            return _distanceFromObjective;
        }

        protected override void UpdateAnimation(float _ammount)
        {
            objective.size += (destiny.size - objective.size).normalized * _ammount;
        }

    }
}