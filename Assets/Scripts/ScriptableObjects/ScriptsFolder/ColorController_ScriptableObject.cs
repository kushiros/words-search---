using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class ColorController_ScriptableObject : ScriptableObject
    
{
    [SerializeField] private Material[] materials = new Material[4];
    // Start is called before the first frame update

    public event Action<Material> OnChangeMaterialEvent;

    public Material changeColor() {
        Material _materialToReturn = GetRandomColor(GetRandomInt());
        OnChangeMaterialEvent?.Invoke(_materialToReturn);
        return _materialToReturn;

    }

    private int GetRandomInt()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 4);
        return randomNumber;
    }
    private Material GetRandomColor(int i)
    {
        return materials[i];
    }



}
