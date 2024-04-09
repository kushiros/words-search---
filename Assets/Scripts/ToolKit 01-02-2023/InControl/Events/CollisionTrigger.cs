using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace InControl
{
    /// <summary>
    /// Works with 3D and 2D collisions and triggers
    /// </summary>
    public class CollisionTrigger : MonoBehaviour
    {
        [Header("Works with 3D and 2D collisions and triggers")]
        [Space(20)]
        public UnityEvent OnObjectEnter;
        // public UnityEvent OnObjectStay;
        public UnityEvent OnObjectExit;

        public ObjectiveSettings objectiveSettings;
        [Serializable]
        public struct ObjectiveSettings
        {
            public string[] useTags;
            //public LayerMask useLayer;
        }

       public bool detectCollision = true;

        public void _SetDetectCollisions(bool set)
        {
            detectCollision = set;
        }

        public void OnCollisionEnterEvent(GameObject collided)
        {
            if (detectCollision && checkObjectCollided(collided)) OnObjectEnter?.Invoke();
        }

        public void OnCollisionExitEvent(GameObject collided)
        {
            if (detectCollision && checkObjectCollided(collided)) OnObjectExit?.Invoke();
        }

        public bool checkObjectCollided(GameObject _object)
        {
            bool isValid = false;
            if (objectiveSettings.useTags.Length <= 0) return isValid = true;

            foreach (var tag in objectiveSettings.useTags)
            {
                if (_object.CompareTag(tag))
                {
                    isValid = true;
                    break;
                }
            }
            print(isValid);

            return isValid;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnterEvent(collision.gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterEvent(collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnCollisionEnterEvent(collision.gameObject);
        }

        private void OnTriggerEnter(Collider collision)
        {
            OnCollisionEnterEvent(collision.gameObject);
        }

        private void OnCollisionExit(Collision collision)
        {
            OnCollisionExitEvent(collision.gameObject);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            OnCollisionExitEvent(collision.gameObject);
        }

        private void OnTriggerExit(Collider collision)
        {
            OnCollisionExitEvent(collision.gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            OnCollisionExitEvent(collision.gameObject);
        }

    }
}