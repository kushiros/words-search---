using UnityEngine;

public class EnlargeHeightDetection : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cell")) 
        {
            Debug.Log("enlargeHeightRenderer ha pasado por encima de una cell nueva");
        }
    }
}