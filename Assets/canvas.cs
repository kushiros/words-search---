using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class canvas : MonoBehaviour
{

    private void Start()
    {
        // Start is called before the first frame update
        Canvas tempCanvas = gameObject.GetComponent<Canvas>();
        // Set the overrideSorting property to true
        tempCanvas.overrideSorting = true;
    }
}
