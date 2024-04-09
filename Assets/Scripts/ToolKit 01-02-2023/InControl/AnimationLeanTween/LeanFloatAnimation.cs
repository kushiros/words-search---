using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace InControl
{
    public class LeanFloatAnimation : LeanAnimation
    {
 
        public float startFloatValue;
        public float endFloatValue;
        public FloatEvent animatedFloat;

        public override void UpdateAnimation(float percentage)
        {
            float currentValue = Mathf.Lerp(startFloatValue, endFloatValue, percentage);

            animatedFloat.Invoke(currentValue);
        }
    }
}