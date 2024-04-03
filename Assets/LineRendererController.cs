using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
    public class LineRendererController : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public float width = 0f;

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            SetLineWidth();
        }

        void Update()
        {
            // Opcional: si quieres que el ancho se actualice continuamente en el editor
            SetLineWidth();
        }

        private void SetLineWidth()
        {
            if (lineRenderer != null)
            {
                lineRenderer.startWidth = width;
                lineRenderer.endWidth = width;
            }
        }
    
}
