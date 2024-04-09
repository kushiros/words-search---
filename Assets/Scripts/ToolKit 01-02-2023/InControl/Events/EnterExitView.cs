using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InControl
{
    public class EnterExitView : MonoBehaviour
    {
        public bool canActivate = true;
        public UnityEvent OnEnterView;
        public UnityEvent OnExitView;

        public void _SetCanActivate(bool _canActivate)
        {
            canActivate = _canActivate;
        }

        private void OnBecameVisible()
        {
            if (canActivate) OnEnterView?.Invoke();
        }

        private void OnBecameInvisible()
        {
            if (canActivate) OnExitView?.Invoke();
        }
    }
}
