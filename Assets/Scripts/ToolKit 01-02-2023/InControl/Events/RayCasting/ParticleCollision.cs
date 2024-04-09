using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{

    public class ParticleCollision : MonoBehaviour
    {
        [SerializeField] ParticleSystem particleSystemUsed;
        [SerializeField] ParticleSystemTriggerEventType particleEventType;
        List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
        ParticleSystem.ColliderData collided;

        private void OnParticleTrigger()
        {
            int number = particleSystemUsed.GetTriggerParticles(particleEventType, particles, out collided);

            for (int i = 0; i < number; i++)
            {
                int collidedNum = collided.GetColliderCount(i);
                for (int i2 = 0; i2 < collidedNum; i2++)
                {
                    Component collidedObject = collided.GetCollider(i, i2);
                    print("Collided with " + collidedObject.name);
                    if (collidedObject.TryGetComponent(out ISignalReceiver signalReceiver))
                    {
                        signalReceiver.ReceiveSignal();
                    }
                }
            }

        }

    }
}