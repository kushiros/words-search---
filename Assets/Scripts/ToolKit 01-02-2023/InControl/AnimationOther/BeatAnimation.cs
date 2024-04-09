using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatAnimation : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] float growSize = 1.4f;
    [SerializeField] float minTimeBetweenGrow =0.01f;
    [SerializeField] float timeTillLastGrow = 0;
    [SerializeField] Transform objective;
    Vector3 startSize;
    [SerializeField] public UnityEvent onGrow;
    // Start is called before the first frame update
    void Start()
    {
        startSize = objective.localScale;
    }

    public void Grow()
    {
        if (timeTillLastGrow > 0) return;
        objective.localScale = startSize * growSize;
        timeTillLastGrow = minTimeBetweenGrow;
        onGrow?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        timeTillLastGrow -= Time.deltaTime;
        objective.localScale = Vector3.Lerp(objective.localScale, startSize, Time.deltaTime * speed);
    }
}
