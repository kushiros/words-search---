using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace InControl
{
    public class PhysicsMove : MonoBehaviour
    {
        Rigidbody objectiveRB;
        Vector3 offSet;

        private Transform grapplePoint;
        [HideInInspector]
        public Transform GrapplePoint
        {
            get
            {
                if (grapplePoint == null)
                {
                    grapplePoint = new GameObject().transform;
                }
                return grapplePoint;
            }

            set
            {
                grapplePoint = value;

            }
        }

        [SerializeField] float force = 250;
        [SerializeField] float maxForce = 20;

        [Serializable]
        public class Vector3Event : UnityEvent<Vector3> { }
        [SerializeField] Vector3Event physicsPoint;

        private void Update()
        {
            MoveObjective();
        }

        public void GetObjective(Transform newObjective)
        {
            if (newObjective.TryGetComponent(out Rigidbody rb))
            {
                // offSet = -  newObjective.position + transform.position ;
                objectiveRB = rb;
                GrapplePoint.position = transform.position;
                GrapplePoint.parent = newObjective;

            }
        }

        public void MoveObjective()
        {
            if (objectiveRB == null) return;

            Vector3 finalDirection = -GrapplePoint.position + transform.position /*- objectiveRB.rotation * offSet*/;

            Vector3 finalSpeed = Vector3.ClampMagnitude(finalDirection * force, maxForce);

            //objectiveRB.AddForce(finalDirection * force);
            objectiveRB.angularVelocity = Vector3.zero;
            objectiveRB.velocity = finalSpeed;
            // objectiveRB.AddForceAtPosition(finalSpeed, GrapplePoint.position, ForceMode.VelocityChange);

            physicsPoint?.Invoke(GrapplePoint.position);
        }

        public void ReleaseObjective()
        {
            objectiveRB = null;
        }

    }
}