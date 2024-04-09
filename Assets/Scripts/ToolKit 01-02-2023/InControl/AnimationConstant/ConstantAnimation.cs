using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace InControl
{
    public abstract class ConstantAnimation : ToolsParent
    {
        bool isActive;

        public float changePerSecond;
        public UpdateMethod updateMethod;
        [Serializable]
        public enum UpdateMethod
        {
            Update,
            FixedUpdate
        }

        // Update is called once per frame
        void Update()
        {
            if (isActive && updateMethod == UpdateMethod.Update) UpdateAnimation(Time.deltaTime * changePerSecond);
        }

        private void FixedUpdate()
        {
            if (isActive && updateMethod == UpdateMethod.FixedUpdate) UpdateAnimation(Time.fixedDeltaTime * changePerSecond);
        }

        protected override void _OnRunAction()
        {
            isActive = true;
        }

        public void StopAction()
        {
            isActive = false;
        }


        public abstract void UpdateAnimation(float _changePerSecond);
    }
}