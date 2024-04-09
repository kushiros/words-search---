using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeLettersFall : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(LettersFall());
        }
    }

    IEnumerator LettersFall()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));

        rb.isKinematic = false;

        rb.AddTorque(new Vector3(0f, 0f, Random.Range(0f, 50f)));
    }
}
