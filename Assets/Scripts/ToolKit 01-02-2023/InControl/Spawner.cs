using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace InControl
{
    public class Spawner : ToolsParent
    {
        [Space(5)]

        public ItemsToSpawn itemsToSpawn;
        [Serializable]
        public struct ItemsToSpawn
        {
            public bool isRandom;
            public ItemToSpawn[] items;
            [HideInInspector] public int nextIndex;
        }

        [Serializable]
        public struct ItemToSpawn
        {
            public GameObject GObject;
            public int chance;
        }

        [Space(5)]

        public SpawnSettings spawnRateSettings;
        [Serializable]
        public struct SpawnSettings
        {
            public bool loop;
            public int maxNumberToSpawn;
            [HideInInspector]public int leftToSpawn;
            public float minTime;
            public float maxTime;
        }

        [Space(5)]

        public AreaSettings areaSettings;
        [Serializable]
        public struct AreaSettings
        {
            public bool keepZInZero;
            public Vector3 cubeSize;
            public float sphereRadius;
            public Shape shape;
            public enum Shape
            {
                Cube,
                Sphere
            }
        }
        [Space(5)]

        public RotationSettings rotationSettings;
        [Serializable]
        public struct RotationSettings
        {
            public float angleVariation;
            public bool Z,X,Y;
            public bool useSpawnerAngle;
        }

        public Events events;
        [Serializable]
        public struct Events
        {
            public UnityEvent OnSpawn;
        }
        protected override void _OnRunAction()
        {
            spawnRateSettings.leftToSpawn = spawnRateSettings.maxNumberToSpawn;
            StartCoroutine(Secuence());
        }

        IEnumerator Secuence()
        {
            Spawn();
            spawnRateSettings.leftToSpawn--;
            yield return new WaitForSeconds(UnityEngine.Random.Range(spawnRateSettings.minTime, spawnRateSettings.maxTime));
            if (spawnRateSettings.loop || spawnRateSettings.leftToSpawn > 0 && spawnRateSettings.minTime != 0 && spawnRateSettings.maxTime != 0) StartCoroutine(Secuence());
        }
        

        public void Spawn()
        {
            int index2Spawn = 0;
            if (itemsToSpawn.isRandom)
            {
                int totalChance = 0;
                foreach (var item in itemsToSpawn.items)
                {
                    totalChance += item.chance;
                }
                int rollNumber = UnityEngine.Random.Range(0, totalChance);
                int chanceCount = 0;
                for (int i = 0; i < itemsToSpawn.items.Length; i++)
                {
                    chanceCount += itemsToSpawn.items[i].chance;
                    if (chanceCount >= rollNumber)
                    {
                        index2Spawn = i;
                        break;
                    }
                }
            }
            else
            {
                index2Spawn = itemsToSpawn.nextIndex;
                itemsToSpawn.nextIndex++;
                if (itemsToSpawn.nextIndex >= itemsToSpawn.items.Length) itemsToSpawn.nextIndex = 0;
            }

            Quaternion spawnRotation = GetRandomRotation(GetObject(index2Spawn).transform);

            Instantiate(GetObject(index2Spawn), GetRandomPositionInArea(), spawnRotation);
            events.OnSpawn?.Invoke();
        }

        private GameObject GetObject(int index)
        {
            return itemsToSpawn.items[index].GObject;
        }

        protected Quaternion GetRandomRotation(Transform prefab)
        {
            Quaternion rotation = Quaternion.identity;
           
            if (rotationSettings.useSpawnerAngle)
            {
                rotation = GetRotationVariation() * transform.rotation;
            }
            else
            {
                rotation = prefab.rotation;
            }
            return rotation;
        }

        protected Quaternion GetRotationVariation()
        {

            int X = Convert.ToInt32(rotationSettings.X);
            int Y = Convert.ToInt32(rotationSettings.Y);
            int Z = Convert.ToInt32(rotationSettings.Z);
            Vector3 selectedAngles = new Vector3(X, Y, Z);

            Vector3 minVar = -selectedAngles * rotationSettings.angleVariation/2;
            Vector3 maxVar = selectedAngles * rotationSettings.angleVariation/2;
            return Quaternion.Euler(VectorMath.RandomV3(minVar, maxVar));
        }

        public Vector3 GetRandomPositionInArea()
        {
            Vector3 finalPosition = transform.position;

            switch (areaSettings.shape)
            {
                case AreaSettings.Shape.Cube:

                    finalPosition += VectorMath.RandomV3(-areaSettings.cubeSize / 2, areaSettings.cubeSize / 2);

                    break;
                case AreaSettings.Shape.Sphere:

                    finalPosition += UnityEngine.Random.insideUnitSphere * areaSettings.sphereRadius;

                    break;
                default:
                    break;
            }

            if (areaSettings.keepZInZero) finalPosition = (Vector2)finalPosition;
            return finalPosition;
        }

        private void OnDrawGizmos()
        {
            switch (areaSettings.shape)
            {
                case AreaSettings.Shape.Cube:
                    Gizmos.DrawWireCube(transform.position, areaSettings.cubeSize);
                    break;
                case AreaSettings.Shape.Sphere:
                    Gizmos.DrawWireSphere(transform.position, areaSettings.sphereRadius);
                    break;
                default:
                    break;
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}