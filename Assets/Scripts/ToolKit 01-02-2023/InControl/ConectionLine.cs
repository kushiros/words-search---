using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class ConectionLine : MonoBehaviour
    {
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] Transform[] conectedTransforms;

        [SerializeField] bool simulate = true;

        public void SetIfSimulate(bool state)
        {
            simulate = state;
        }

        private void Start()
        {
            lineRenderer.positionCount = conectedTransforms.Length;
        }

        // Update is called once per frame
        void Update()
        {
            Simulate();
        }

        protected void Simulate()
        {
            if (!simulate || !lineRenderer.enabled) return;
            for (int i = 0; i < conectedTransforms.Length; i++)
            {
                lineRenderer.SetPosition(i, conectedTransforms[i].position);
            }
        }


        private void OnDrawGizmosSelected()
        {
            if (lineRenderer == null && !TryGetComponent(out lineRenderer)) Debug.LogError("No lineRenderer detected");
        }

    }
}