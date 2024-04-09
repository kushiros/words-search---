using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InControl
{
    public class EventSecuence : ToolsParent
    {
        [Header("Secuence Settings")]
        public bool isLoop;
        public Event[] events;
        [Serializable]
        public struct Event
        {
            public float timeUntilEvent;
            public UnityEvent events;
        }

        protected override void _OnRunAction()
        {
            StartCoroutine(Secuence());
        }

        IEnumerator Secuence()
        {
            int count = 0;
            foreach (var item in events)
            {
                count++;
                yield return new WaitForSeconds(item.timeUntilEvent);
                item.events?.Invoke();
               // print(gameObject.name + " executing event number: " + count);
            }
            if (isLoop) _OnRunAction();
        }

    }
}
