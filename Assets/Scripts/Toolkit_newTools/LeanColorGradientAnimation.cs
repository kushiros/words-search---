using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace InControl
{
    public class LeanColorGradientAnimation : LeanAnimation
    {
 
        public Gradient startColorValue;
        public ColorEvent animatedColor;

        public override void UpdateAnimation(float percentage)
        {
            Color currentValue = startColorValue.Evaluate(percentage); 

            animatedColor.Invoke(currentValue);
        }
    }
}