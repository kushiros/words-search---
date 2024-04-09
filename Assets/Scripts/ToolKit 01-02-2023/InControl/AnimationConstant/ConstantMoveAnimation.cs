using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class ConstantMoveAnimation : ConstantAnimation
    {
        public bool makeRelativeToThisForward = true;
        public Transform objective;

        public override void UpdateAnimation(float _changePerSecond)
        {
            objective.position += (makeRelativeToThisForward ? transform.forward : objective.forward).normalized * _changePerSecond;
        }
    }
}