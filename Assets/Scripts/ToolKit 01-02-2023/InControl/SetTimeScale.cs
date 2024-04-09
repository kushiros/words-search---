using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class SetTimeScale : ToolsParent
    {
       public float timeScale;
        public bool keepFixedDeltaTimeConstant;

        float fixedDeltatimeBase = -1;

        private void OnEnable()
        {
            if(fixedDeltatimeBase <= 0)
            fixedDeltatimeBase = Time.fixedDeltaTime;
        }

        protected override void _OnRunAction()
        {
            _SetTimeScale(timeScale);
        }

        public void _SetTimeScale(float _timeScale)
        {
            Time.timeScale = _timeScale;
            if(keepFixedDeltaTimeConstant) Time.fixedDeltaTime = fixedDeltatimeBase * Time.timeScale;
        }
    }
}