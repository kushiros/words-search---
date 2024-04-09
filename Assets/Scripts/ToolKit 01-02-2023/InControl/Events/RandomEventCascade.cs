using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class RandomEventCascade : ToolsParent
    {
        public List<ToolsParent> objectives;

        [BoundedCurve(1,0,0,1,1)]public AnimationCurve probabilityCurve;

        List<ToolsParent> activationCascade;

        public float minTimeBetweenEvents;
        public float maxTimeBetweenEvents;

        public int maxNumberExecuted = 999;


        private void Start()
        {
            ResetActivationCascade();
        }

        protected override void _OnRunAction()
        {
            StartCoroutine(RunEvents());
        }

        public void ResetActivationCascade()
        {
            activationCascade = objectives;
        }

        protected IEnumerator RunEvents()
        {
            for (int i = 0; activationCascade.Count != 0 && i < maxNumberExecuted; i++)
            {
                ActivateRandom();
                yield return new WaitForSeconds(Random.Range(minTimeBetweenEvents,maxTimeBetweenEvents));
            }
        }
        
        public void ActivateRandom()
        {
            float[] probabilityArray = new float[activationCascade.Count];

            float curveSampleSize = 1 / Mathf.Max(probabilityArray.Length - 1f, 1f);

            print("CurveSampleSize: " + curveSampleSize + "  " + (1 / Mathf.Max(probabilityArray.Length - 1, 1)));

            float totalValue = 0;

            for (int i = 0; i < probabilityArray.Length; i++)
            {
                float sampleValue = probabilityCurve.Evaluate(curveSampleSize * i);
                print("SAMPLE VALUE " + sampleValue + " at pos: " + i);
                probabilityArray[i] = sampleValue;
                totalValue += sampleValue;
            }

            float numberRoll = Random.Range(0, totalValue);

           // print("Roll: " + numberRoll + "  TotalValue: " + totalValue);

            float rollCount = 0;

            for (int i = 0; i < probabilityArray.Length; i++)
            {
                rollCount += probabilityArray[i];
                if (rollCount >= numberRoll)
                {
                    ActivateAtIndex(i);
                    break ;
                }
            }

           // print("RollCount " + rollCount);

        }

        public void ActivateAtIndex(int index)
        {
            activationCascade[index]._RunAction();
            activationCascade.RemoveAt(index);
        }

    }
}