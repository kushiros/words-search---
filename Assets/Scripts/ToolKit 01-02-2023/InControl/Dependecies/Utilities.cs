using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace InControl
{

    [Serializable] public class FloatEvent : UnityEvent<float> { }
    [Serializable] public class ColorEvent : UnityEvent<Color> { }

    public static class VectorMath
    {
        public static Vector3 RandomV3(this Vector3 min, Vector3 max)
        {
            return new Vector3(
                UnityEngine.Random.Range(min.x, max.x),
                UnityEngine.Random.Range(min.y, max.y),
                UnityEngine.Random.Range(min.z, max.z));
        }

        public static List<GameObject> DetectObjects(Transform position, float radius, string tag)
        {
            Collider[] objectives = Physics.OverlapSphere(position.position, radius);

            List<GameObject> finalObjects = new List<GameObject>();

            foreach (var item in objectives)
            {
                if (item.gameObject.CompareTag(tag))
                {
                    finalObjects.Add(item.gameObject);
                }
            }
            return finalObjects;
        }

        public static Transform GetClosest(Transform pointA, List<Transform> objectives)
        {
            List<Vector3> objectivespositions = new List<Vector3>();

            foreach (var item in objectives)
            {
                objectivespositions.Add(item.position);
            }

            return objectives[GetClosestByIndex(pointA.position, objectivespositions)];
        }

        public static Vector3 GetClosest(Vector3 pointA, List<Vector3> objectives)
        {
            return objectives[GetClosestByIndex(pointA, objectives)];
        }

        public static int GetClosestByIndex(Vector3 pointA, List<Vector3> vectors)
        {
            float minDistance = float.MaxValue;
            int minDistanceIndex = 0;
            for (int i = 0; i < vectors.Count; i++)
            {
                float distance = Vector3.Distance(pointA, vectors[i]);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    minDistanceIndex = i;
                }
            }
            return minDistanceIndex;

        }

        public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
        {
            Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

            var mid = Vector3.Lerp(start, end, t);

            return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
        }

        public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
        {
            Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

            var mid = Vector2.Lerp(start, end, t);

            return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
        }

        public static Vector3 Y(this Vector3 vector, float Yvalue)
        {
           return vector = new Vector3(vector.x, Yvalue, vector.z);
        }

        public static Vector3 X(this Vector3 vector, float Xvalue)
        {
           return vector = new Vector3(Xvalue, vector.y, vector.z);
        }

        public static Vector3 Z(this Vector3 vector, float Zvalue)
        {
            return vector = new Vector3(vector.x, vector.y, Zvalue);
        }

    }
    public static class Utilities
    {

      

    }

    public static class ExGizmos
    {

        public static void DrawWireBox(Vector3 position, Quaternion rotation)
        {



           // Gizmos.DrawLine();
        }

    }

}
