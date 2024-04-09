using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SRC
{
    public class GlobalToggleListener : MonoBehaviour
    {
        [SerializeField] GlobalToggleScriptable listeningToggle;

        public UnityEvent OnToggleOn;
        public UnityEvent OnToggleOff;

        private void Start()
        {
            listeningToggle.OnToggleOff.AddListener(OnGlobalTriggerToggleOff);
            listeningToggle.OnToggleOn.AddListener(OnGlobalTriggerToggleOn);
            ToggleUpdate();
        }

        protected void ToggleUpdate()
        {
            if (listeningToggle.CheckToggleState())
            {
                OnToggleOn?.Invoke();
            }
            else
            {
                OnToggleOff?.Invoke();
            }
        }

        protected void OnGlobalTriggerToggleOn()
        {
            ToggleUpdate();
        }

        protected void OnGlobalTriggerToggleOff()
        {
            ToggleUpdate();
        }
    }
}