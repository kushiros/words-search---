using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SRC
{
    public class EventListenerGeneric<TDataType> : MonoBehaviour
    {
        [SerializeField] EventScriptable[] listeningEvents;

        [SerializeField] UnityEvent OnEventCalled;

        private void Start()
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