using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SRC
{
    public class EventCaller : MonoBehaviour
    {
        [SerializeField] EventScriptable globalEvent;

        public void _CallEvent()
        {
            globalEvent.CallEvent();
        }


    }
}