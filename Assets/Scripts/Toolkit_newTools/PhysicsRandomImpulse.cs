using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class PhysicsRandomImpulse : ToolsParent
    {
        public Rigidbody rb;
        public bool setKinematicOff = true;
        public float minUpValue = 0;
        public float maxUpValue = 5;
        public float maxSideValue = 4;
        public float maxTorqueValue = 20;

        protected override void _OnRunAction()
        {
            if(setKinematicOff) rb.isKinematic = false;
            Vector2 randomForce = new Vector2(Random.Range(-maxSideValue, maxSideValue), Random.Range(minUpValue, maxUpValue));
            rb.AddForce(randomForce, ForceMode.VelocityChange);
            rb.AddTorque(new Vector3(0, 0, Random.Range(-maxTorqueValue, maxTorqueValue)), ForceMode.VelocityChange);
        }
    }
}