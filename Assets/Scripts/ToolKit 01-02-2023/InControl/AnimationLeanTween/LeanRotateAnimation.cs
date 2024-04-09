using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class LeanRotateAnimation : LeanAnimation
    {
        [SerializeField] Transform animObjective;
        [SerializeField] Transform finalRotation;
        Quaternion startRotQuat;
        [SerializeField] bool useGlobalRotation;


        protected override void _OnRunAction()
        {
            startRotQuat = animObjective.localRotation;
            base._OnRunAction();
        }

        public override void UpdateAnimation(float percentage)
        {
            Quaternion finalRotationQuat;
            if (useGlobalRotation)
                finalRotationQuat = finalRotation.rotation;
            else
                finalRotationQuat = finalRotation.localRotation;

            animObjective.localRotation = Quaternion.Lerp(startRotQuat, finalRotationQuat, percentage);

        }

    }
}