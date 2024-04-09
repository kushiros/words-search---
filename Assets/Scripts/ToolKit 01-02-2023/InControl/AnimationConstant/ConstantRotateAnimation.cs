using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class ConstantRotateAnimation : ConstantAnimation
    {
        //public bool makeRelativeToThisForward = true;
        public Vector3 degreesPerSecond;
        public Transform objective;
        public Space space;

        public override void UpdateAnimation(float _changePerSecond)
        {
            objective.Rotate(degreesPerSecond * _changePerSecond, space);
        }
    }
}