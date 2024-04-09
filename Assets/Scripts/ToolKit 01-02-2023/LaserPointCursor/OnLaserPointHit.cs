using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InControl
{
    public class OnLaserPointHit : MonoBehaviour, IOnLaserPointCursorHit
    {
        public UnityEvent onLaserPointHit;

        public void OnLaserPointCursorHit()
        {
            onLaserPointHit?.Invoke();
        }
    }
}