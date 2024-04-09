using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InControl
{
    public class DestroyThis : ToolsParent
    {
        public bool destroyFromParent;
        public bool destroyImages;
        public bool destroyText;
        public bool destroyGameObjects = true;
        [Space(20)]
       // public float delayedStart;

        public string[] whiteList;

        public GameObject objectObjective;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _OnRunAction();
            }
        }

        //IEnumerator Start()
        //{
        //   yield return new WaitForSeconds(delayedStart);
        //    RunAction();
        //}

        protected override void _OnRunAction()
        {
            if (objectObjective == null) objectObjective = this.gameObject;
            if (destroyImages) DestroyComponents(GetComponentsToInteract<Image>());
            if (destroyText) DestroyComponents(GetComponentsToInteract<TextMesh>());

            if (destroyGameObjects) DestroyGameObject((destroyFromParent) ? objectObjective.transform.parent.gameObject : objectObjective.gameObject);
        }

        public T[] GetComponentsToInteract<T>() where T : Component
        {
            return SelectObjective().GetComponentsInChildren<T>(true);
        }

        public Transform SelectObjective()
        {
            if (objectObjective == null) objectObjective = this.transform.gameObject;
            return (destroyFromParent) ? objectObjective.transform.parent : objectObjective.transform;
        }

        public void DestroyGameObject(GameObject objective)
        {
           // Debug.LogError($"DESTRUCTION TO GameObject.{this.transform.parent.gameObject.name} YEAA ");
            Destroy(objective);
        }

        public void DestroyComponents<T>(T[] images) where T : Component
        {
            foreach (var item in images)
            {
                if (!ListContains(whiteList, new string[] { item.gameObject.name }))
                    Destroy(item);
            }
        }

        public bool ListContains(string[] a, string[] b)
        {
            foreach (var itema in a)
            {
                foreach (var itemb in b)
                {
                    if (string.Equals(itema, itemb)) return true;
                }
            }
            return false;
        }
    }
}