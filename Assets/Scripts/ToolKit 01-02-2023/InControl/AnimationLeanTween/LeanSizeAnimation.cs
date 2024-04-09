using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class LeanSizeAnimation : LeanAnimation
    {
        public Transform animObjective;
        public Transform finalSize;
        public Vector3 relativeFinalSize;
        Vector3 startSize;

        protected override void _OnRunAction()
        {
            base._OnRunAction();
            startSize = animObjective.localScale;
        }

        public override void UpdateAnimation(float percentage)
        {
            if(finalSize != null)
            {
                animObjective.localScale = Vector3.Lerp(startSize, finalSize.localScale + relativeFinalSize, percentage);
            }
            else
            {
                animObjective.localScale = Vector3.Lerp(startSize, relativeFinalSize + startSize, percentage);
            }
        }

    }
}