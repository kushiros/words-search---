using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InControl
{

    public class OnPhysicsEvent : ToolsParent
    {
        public Rigidbody objectiveRB;
        public bool active = true;

        public SpeedEvent[] speedEvents;
        [Serializable]
        public struct SpeedEvent
        {
            public float minSpeed;
            public float maxSpeed;
            public UnityEvent SpeedReached;
        }

        public CollisionEvent[] collisionEvents;
        [Serializable]
        public struct CollisionEvent
        {
            public float minRelativeSpeed;
            public float maxRelativeSpeed;
            public UnityEvent SpeedReached;
        }

        protected override void _OnRunAction()
        {
            SetActive(true);
        }

        public void SetActive(bool state)
        {
            active = state;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!enabled || !active) return;
            foreach (var item in collisionEvents)
            {
                if (collision.relativeVelocity.magnitude > item.minRelativeSpeed && collision.relativeVelocity.magnitude < item.maxRelativeSpeed)
                {
                    item.SpeedReached.Invoke();
                }
            }
        }

        private void Update()
        {
            if (active)
            {
                foreach (var speedEvent in speedEvents)
                {
                    if (speedEvent.minSpeed < objectiveRB.velocity.magnitude && speedEvent.maxSpeed > objectiveRB.velocity.magnitude)
                    {
                        speedEvent.SpeedReached.Invoke();
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (objectiveRB == null)
            {
                TryGetComponent(out objectiveRB);
            }
        }

       
    }
}
