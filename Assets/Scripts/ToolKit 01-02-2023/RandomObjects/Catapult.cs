using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using InControl;

namespace RandomObjects
{
    public class Catapult : MonoBehaviour
    {
        public Transform loadedObject;
        [HideInInspector] public Rigidbody objectRB;
        [Header("Settings")]
        public string tagLaunchObjects = "Launcheable";
        public float timeTillLaunch;
        public float launchForce;
        public float torque;
        public Transform catapultPivot;
        public UnityEvent OnLoaded;
        public UnityEvent OnLaunch;

        [Header("Loading animation")]
        public float loadAnimTime;
        public LeanTweenType easeType;
        public AnimationCurve moveCurveY;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(tagLaunchObjects) && (loadedObject == null || other.gameObject != loadedObject.gameObject))
            {
                print($"found that {other.gameObject.name} is a loadable object");
                objectRB = other.GetComponent<Rigidbody>();
                loadedObject = other.transform;
                objectRB.isKinematic = true;
                leanTween();
            }
        }

        public void leanTween()
        {
            //float initialHeight = loadedObject.position.y;
            //LeanTween.value(loadedObject.gameObject, (value) =>
            //{
            //    float height = Mathf.Lerp(initialHeight, catapultPivot.position.y,value) + moveCurveY.Evaluate(value);
            //    loadedObject.position = loadedObject.position.Y(height);
            //}, 0, 1, loadAnimTime).setEase(easeType);
            LeanTween.moveY(loadedObject.gameObject, catapultPivot.position.y, loadAnimTime).setEase(moveCurveY).setOnComplete(() =>
                {
                    loadedObject.parent = catapultPivot;
                    OnLoaded?.Invoke();
                    StartCoroutine(Launch());
                }
                );
            LeanTween.moveZ(loadedObject.gameObject, catapultPivot.position.z, loadAnimTime).setEase(easeType);
            LeanTween.moveX(loadedObject.gameObject, catapultPivot.position.x, loadAnimTime).setEase(easeType);
            LeanTween.rotate(loadedObject.gameObject, catapultPivot.rotation.eulerAngles, loadAnimTime).setEase(easeType);
        }

        public IEnumerator Launch()
        {

            yield return new WaitForSeconds(timeTillLaunch);
            loadedObject.parent = null;
            objectRB.isKinematic = false;
            //objectRB.AddForce(catapultPivot.up * launchForce, ForceMode.VelocityChange);
            objectRB.velocity = catapultPivot.up * launchForce;
            objectRB.AddTorque(Vector3.right * torque, ForceMode.VelocityChange);
            OnLaunch?.Invoke();

        }
    }
}