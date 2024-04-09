using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InControl
{
    public class OnSignalDetect : MonoBehaviour, ISignalReceiver
    {
        public UnityEvent onSignalReceived;
        public void ReceiveSignal()
        {
            onSignalReceived?.Invoke();
        }
    }
}