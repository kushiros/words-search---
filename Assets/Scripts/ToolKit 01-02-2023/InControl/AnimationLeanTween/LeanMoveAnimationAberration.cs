using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class LeanMoveAnimationAberration : LeanAnimation
    {
        public Transform animObjective;

        /// <summary>
        /// Leave empty to use objective's transform as start position
        /// </summary>
        public Transform forceStartPos;
        private bool hasStartPos => forceStartPos != null;
        public Transform finalPos;
        public Vector3 relativeFinalPosCoords;
        Vector3 startPos;

        public float randomMaxStartTime;
        public float randomMinStartTime;

        private void Awake()
        {
            RandimiceStartTime();
        }

        public void RandimiceStartTime()
        {
            startSettings.timeToStart = Random.Range(randomMinStartTime, randomMaxStartTime);
            //print(startSettings.timeToStart);
        }

        protected override void _OnRunAction()
        {
            if (!this.gameObject.activeInHierarchy) return;
            base._OnRunAction();
            startPos = animObjective.position;
           // relativeFinalPosCoords += startPos;
        }

        public override void UpdateAnimation(float percentage)
        {
            if (hasStartPos) startPos = forceStartPos.position;
            if(finalPos != null)
            {
                animObjective.position = Vector3.Lerp(startPos, finalPos.position + relativeFinalPosCoords, percentage);
            }
            else
            {
                animObjective.position = Vector3.Lerp(startPos, relativeFinalPosCoords + startPos, percentage);
            }
        }

    }
}