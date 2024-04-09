using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class DontCollideWith : MonoBehaviour
    {
        public Collider objectiveCollider;
        public List<Collider> collidersToNOtCollide;

        // Update is called once per frame
        void Start()
        {
            foreach (var item in collidersToNOtCollide)
            {
                Physics.IgnoreCollision(objectiveCollider, item);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (collidersToNOtCollide.Count <= 0)
            {
                this.transform.GetComponentsInChildren<Collider>(true, collidersToNOtCollide);
            }
        }
    }
}