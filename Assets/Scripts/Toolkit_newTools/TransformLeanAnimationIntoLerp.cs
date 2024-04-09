using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class TransformLeanAnimationIntoLerp : MonoBehaviour
    {
        [SerializeField] LeanAnimation leanAnimation;
        [SerializeField] float inputValueRemapAs0 = 0;
        [SerializeField] float inputValueRemapAs1 = 1;

        // Start is called before the first frame update
        void Start()
        {
            leanAnimation._RunAction();
            leanAnimation._PauseAnimation();
        }

        public void GiveNewLerpValue(float value)
        {
            if (!this.isActiveAndEnabled) return;
            if(inputValueRemapAs0 < inputValueRemapAs1)
            value = Mathf.Clamp(value, inputValueRemapAs0, inputValueRemapAs1);
            else
            value = Mathf.Clamp(value, inputValueRemapAs1, inputValueRemapAs0);

            value = Remap(value, inputValueRemapAs0, 0, inputValueRemapAs1, 1);

            leanAnimation.UpdateAnimation(value);
        }

        //public float Remap(float value, float from1, float to1, float from2, float to2)
        //{
        //    return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        //}

        public float Remap( float from, float fromMin, float toMin, float fromMax, float toMax)
        {
            var fromAbs = from - fromMin;
            var fromMaxAbs = fromMax - fromMin;

            var normal = fromAbs / fromMaxAbs;

            var toMaxAbs = toMax - toMin;
            var toAbs = toMaxAbs * normal;

            var to = toAbs + toMin;

            return to;
        }
    }
}