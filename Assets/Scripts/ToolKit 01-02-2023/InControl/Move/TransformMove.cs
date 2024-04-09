using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class TransformMove : MonoBehaviour
    {
        public Transform transformToMove;

        public void MoveToV(Vector3 newPosition)
        {
            if (transformToMove != null) transformToMove.position = newPosition;
        }
    }
}