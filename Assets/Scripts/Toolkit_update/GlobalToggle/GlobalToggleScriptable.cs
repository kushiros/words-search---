using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SRC
{
    [CreateAssetMenu(fileName = "GlobalEvent", menuName = "ScriptableObjects/GlobalToggle", order = 1)]
    public class GlobalToggleScriptable : ScriptableObject
    {
        [HideInInspector] public UnityEvent OnToggleOn;
        [HideInInspector] public UnityEvent OnToggleOff;
        [SerializeField] public bool toggleState;

       public void ToggleOn()
        {
            toggleState = true;
            OnToggleOn?.Invoke();
        }

        public void ToggleOff()
        {
            toggleState = false;
            OnToggleOff?.Invoke();
        }

        public bool CheckToggleState()
        {
            return toggleState;
        }
    }
}