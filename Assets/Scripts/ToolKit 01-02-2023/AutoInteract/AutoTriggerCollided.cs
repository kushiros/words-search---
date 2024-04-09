using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    /// <summary>
    /// Allows you to use an interface that will trigger when the object using it collides against an object with this script
    /// </summary>
    public class AutoTriggerCollided : MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ITriggerObjectOnTouch trigger))
            {
                trigger.TriggerOnCollideTrigger();
            }
        }

        private void OnTriggerStay(Collider collision)
        {
            if (collision.TryGetComponent(out ITriggerObjectOnTouch trigger))
            {
                trigger.TriggerOnCollideTrigger();
            }
        }
    }

    public interface ITriggerObjectOnTouch
    {
        public void TriggerOnCollideTrigger();
    }
}