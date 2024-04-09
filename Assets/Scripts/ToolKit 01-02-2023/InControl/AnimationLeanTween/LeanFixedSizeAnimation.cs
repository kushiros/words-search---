using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class LeanFixedSizeAnimation : LeanAnimation
    {
        public Transform animObjective;
        public Transform startSize;
        public Transform finalSize;

        protected override void _OnRunAction()
        {
            base._OnRunAction();
        }

        public override void UpdateAnimation(float percentage)
        {
                animObjective.localScale = Vector3.Lerp(startSize.localScale, finalSize.localScale, percentage);
        }

    }
}