
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToYForTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveLocalY(this.gameObject, -200, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
