using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InControl
{
    public class RagdollManager : MonoBehaviour
    {
        [SerializeField] bool startActive;
        [SerializeField] List<Rigidbody> ragdollParts;
        [SerializeField] List<Collider> ragdollColliders;
        [SerializeField] bool deactivateColliders = false;

        bool isActive = false;

        public UnityEvent onSetActive;
        public UnityEvent onSetInactive;

        private void Start()
        {
            FetchColliders();

            if (startActive) ActivateRagdoll();
            else DeactivateRagdoll();

        }

        public void ActivateRagdoll()
        {
            foreach (var item in ragdollParts)
            {
                item.isKinematic = false;
            }

            if (deactivateColliders)
                foreach (var item in ragdollColliders)
                {
                    item.enabled = true;
                }
            if(!isActive) onSetActive?.Invoke();
            isActive = true;
        }

        public void DeactivateRagdoll()
        {
            if (deactivateColliders) foreach (var item in ragdollColliders)
                {
                    item.enabled = false;

                }
            foreach (var item in ragdollParts)
            {
                item.isKinematic = true;
            }

            if(isActive) onSetInactive?.Invoke();
            isActive = false;
        }

        protected void FetchColliders()
        {
            foreach (var item in ragdollParts)
            {
                if (item.TryGetComponent(out Collider collider)) ragdollColliders.Add(collider);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (ragdollParts.Count <= 0)
            {
                this.transform.GetComponentsInChildren<Rigidbody>(true, ragdollParts);
            }
        }
    }
}