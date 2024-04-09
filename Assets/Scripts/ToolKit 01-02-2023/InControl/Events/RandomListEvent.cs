using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InControl
{
    public class RandomListEvent : ToolsParent
    {
        public UnityEvent[] events;

        protected override void _OnRunAction()
        {
            events[Random.Range(0,events.Length)]?.Invoke();
        }
    }
}