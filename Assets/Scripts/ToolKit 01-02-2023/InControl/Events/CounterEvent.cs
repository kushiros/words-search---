using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InControl
{
    public class CounterEvent : MonoBehaviour
    {
        public bool isActive = true;
        public float countObjective = 10;
        public float currentCount;

        public UnityEvent onCountObjectiveReached;

        public void _SetActive(bool state)
        {
            isActive = state;
        }

        public void _Add(float ammount = 1)
        {
            currentCount += ammount;
            CheckCount();
        }

        public void _Subtract(float ammount = 1)
        {
            currentCount -= ammount;
            CheckCount();
        }

        public void CheckCount()
        {
            if (isActive && currentCount >= countObjective) onCountObjectiveReached.Invoke();
        }

    }
}