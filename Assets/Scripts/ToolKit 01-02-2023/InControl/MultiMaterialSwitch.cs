using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class MultiMaterialSwitch : MonoBehaviour
    {

        [SerializeField] private List<Renderer> objectiveRenderers;



        //[SerializeField] public List<Material> materialList;
        //public int currentMaterial = 0;

        //private void Start()
        //{
        //   // if (materialList.Count > 0)
        //   // {
        //   // }
        //}

        //public void NextMaterial()
        //{
        //   // currentMaterial++;

        //    SetMaterialWithIndex(currentMaterial + 1);
        //}

        //public void SetMaterialWithIndex(int materialIndex)
        //{
        //    if (materialIndex >= materialList.Count)
        //    {
        //        materialIndex = 0;
        //    }

        //    foreach (var item in objectiveRenderers)
        //    {
        //        item.material = materialList[materialIndex];
        //    }
        //}

        public void SetNewMaterial(Material newMaterial)
        {
            foreach (var item in objectiveRenderers)
            {
                item.material = newMaterial;
            }
        }

        public void SetNewColor(Color newColor)
        {
            foreach (var item in objectiveRenderers)
            {
                item.material.color = newColor;
            }
        }
    }
}