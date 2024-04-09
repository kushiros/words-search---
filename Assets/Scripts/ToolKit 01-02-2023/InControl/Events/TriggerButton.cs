using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InControl
{
    public class TriggerButton : ToolsParent
    {
        public TriggerOnButtonPress[] buttons;
        [Serializable]
        public struct TriggerOnButtonPress
        {
            public KeyCode button;
            public UnityEvent eventTrigger;
            public UnityEvent eventReleaseTrigger;
        }

        private void Update()
        {
            foreach (var item in buttons)
            {
                if (Input.GetKeyDown(item.button))
                {
                    item.eventTrigger?.Invoke();
                }
                if (Input.GetKeyUp(item.button))
                {
                    item.eventReleaseTrigger?.Invoke();
                }
            }
        }

        protected override void _OnRunAction()
        {
            foreach (var item in buttons)
            {
                item.eventTrigger?.Invoke();
            }
        }

    }
}