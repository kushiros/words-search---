using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class LeanScaleSpriteAnimation : LeanAnimation
    {
        public SpriteRenderer animObjective;

        /// <summary>
        /// Leave empty to use objective's transform as start position
        /// </summary>
        public SpriteRenderer forceStartPos;
        private bool hasStartPos => forceStartPos != null;
        public SpriteRenderer finalPos;
        public Vector2 relativeFinalPosCoords;
        Vector2 startPos;

        protected override void _OnRunAction()
        {
            base._OnRunAction();
            startPos = animObjective.size;
           // relativeFinalPosCoords += startPos;
        }

        public override void UpdateAnimation(float percentage)
        {
            if (hasStartPos) startPos = forceStartPos.size;
            if(finalPos != null)
            {
                animObjective.size = Vector3.Lerp(startPos, finalPos.size + relativeFinalPosCoords, percentage);
            }
            else
            {
                animObjective.size = Vector3.Lerp(startPos, relativeFinalPosCoords + startPos, percentage);
            }
        }

    }
}