using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SRC
{
    public class EventListener : MonoBehaviour
    {
        [SerializeField] EventScriptable[] listeningEvents;

        [SerializeField] UnityEvent OnEventCalled;

        private void Awake()
        {
            foreach (var globalEvent in listeningEvents)
            {
                globalEvent.SuscribeToEvent(OnGlobalEvent);
            }
        }

        private void OnDisable()
        {
            foreach (var globalEvent in listeningEvents)
            {
                globalEvent.UnSuscribeToEvent(OnGlobalEvent);
            }
        }

        protected void OnGlobalEvent()
        {
            if(this.isActiveAndEnabled)
            OnEventCalled?.Invoke();
        }
    }
}