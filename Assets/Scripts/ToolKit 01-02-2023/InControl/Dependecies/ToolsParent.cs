using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public abstract class ToolsParent : MonoBehaviour
    {
        public StartSettings startSettings;
        [Serializable]
        public struct StartSettings
        {
            public bool runOnStart;
            public bool runOnEnable;
            [Space(5)]
            public float timeToStart;
        }

        private void Start()
        {
         
            if (startSettings.runOnStart)
            {
                _RunAction();
            }
        }

        public void _AbortCoroutines()
        {
            StopAllCoroutines();
        }

        private void OnEnable()
        {
            if (startSettings.runOnEnable)
            {
                _RunAction();
            }
        }

        public void _RunAction()
        {
            if (!enabled || !gameObject.activeInHierarchy) return;

            if (startSettings.timeToStart > 0)
            {
                StartCoroutine(StartDelay());
            }
            else
            {
                _OnRunAction();
            }
        }

        public IEnumerator StartDelay()
        {
            yield return new WaitForSeconds(startSettings.timeToStart);
            _OnRunAction();
        } 

        protected abstract void _OnRunAction();
    }
}