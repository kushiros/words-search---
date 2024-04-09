using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InControl
{
    public class LeanMaterialColorAnimation : LeanAnimation
    {
        public Renderer animObjective;
        public Image imageObjective;
        public Color color;
        Color startColor;

        protected override void _OnRunAction()
        {
            base._OnRunAction();
            if (animObjective) startColor = animObjective.material.color;
            if (imageObjective) startColor = imageObjective.color;
        }

        public override void UpdateAnimation(float percentage)
        {
            if (animObjective) animObjective.material.color = Color.Lerp(startColor, color, percentage);
            if (imageObjective) imageObjective.color = Color.Lerp(startColor, color, percentage);
        }

    }
}