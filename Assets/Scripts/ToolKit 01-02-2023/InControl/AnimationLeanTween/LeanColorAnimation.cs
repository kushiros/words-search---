using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace InControl
{
    public class LeanColorAnimation : LeanAnimation
    {
 
        public Color startColorValue;
        public Color endColorValue;
        public ColorEvent animatedColor;

        public override void UpdateAnimation(float percentage)
        {
            Color currentValue = Color.Lerp(startColorValue, endColorValue, percentage);

            animatedColor.Invoke(currentValue);
        }
    }
}