using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SRC
{
    public class EventCallerGeneric<TDataType, TEventScriptable> : MonoBehaviour where TEventScriptable : EventScriptableGeneric<TDataType>
    {
        [SerializeField] TEventScriptable globalEvent;

        public void _CallEvent(TDataType data)
        {
            globalEvent.CallEvent();
        }
    }
}