using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace InControl
{

    public class LaserPointCursor : MonoBehaviour
    {
        [SerializeField] public Camera objectiveCamera;
        [SerializeField] public KeyCode cursorkey;
        [SerializeField] public LayerMask laserCollisionLayers;
        [SerializeField] public float laserLength;

        public UnityEvent press;
        public UnityEvent hold;
        public UnityEvent release;

        [Serializable]
        public class VectorEvent : UnityEvent<Vector3> { }
        public VectorEvent pointlaserHit;

        [Serializable]
        public class TransformEvent : UnityEvent<Transform> { }
        public TransformEvent transformLaserHit;
        public UnityEvent releaseAfterHit;

        Ray cursorRay = new Ray();
        bool hasHit;
        // Start is called before the first frame update
        void Start()
        {
            if (objectiveCamera == null) objectiveCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            CursorImput();
        }

        protected void CursorImput()
        {
            if (Input.GetKeyDown(cursorkey))
            {
                press?.Invoke();
                MakeRayFromCamera(Input.mousePosition);
            }
            if (Input.GetKey(cursorkey))
            {
                hold?.Invoke();
                UpdateRay(Input.mousePosition);
            }
            if (Input.GetKeyUp(cursorkey))
            {
                if (hasHit)
                {
                    releaseAfterHit?.Invoke();
                    hasHit = false;
                }
            }
        }

        protected void UpdateRay(Vector2 screenPosition)
        {
            cursorRay = Camera.main.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(cursorRay, out RaycastHit hit, laserLength, laserCollisionLayers))
            {
                pointlaserHit?.Invoke(hit.point);
            }
        }

        public void MakeRayFromCamera(Vector2 screenPosition)
        {
            cursorRay = Camera.main.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(cursorRay, out RaycastHit hit, laserLength, laserCollisionLayers))
            {
                hasHit = true;
                if (hit.collider.gameObject.TryGetComponent(out IOnLaserPointCursorHit hitEvent)) hitEvent.OnLaserPointCursorHit();
                pointlaserHit?.Invoke(hit.point);
                transformLaserHit?.Invoke(hit.collider.transform);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (objectiveCamera == null) objectiveCamera = Camera.main;
            Gizmos.DrawRay(objectiveCamera.transform.position, cursorRay.direction * laserLength);
        }



    }

    public interface IOnLaserPointCursorHit
    {
        public void OnLaserPointCursorHit();
    }
}