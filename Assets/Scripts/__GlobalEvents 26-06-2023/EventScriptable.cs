using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SRC
{
    [CreateAssetMenu(fileName = "GlobalEvent", menuName = "ScriptableObjects/GlobalEvent/void", order = 1)]
    public class EventScriptable : ScriptableObject
    {
        [HideInInspector]public UnityEvent globalEvent;

       public void CallEvent()
        {
            globalEvent?.Invoke();
        }

        public void SuscribeToEvent(UnityAction call)
        {
            globalEvent.AddListener(call);
        }

        public void UnSuscribeToEvent(UnityAction call)
        {
            globalEvent.RemoveListener(call);
        }
    }
}