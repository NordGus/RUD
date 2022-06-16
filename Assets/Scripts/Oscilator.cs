using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) return;

        const float tau = Mathf.PI * 2;                // constant value of 6.283
        float cycles = Time.time / period;             // continually growing overtime
        float rawSinWave = Mathf.Sin(tau * cycles);    // going from -1 to 1
        float movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 making it cleaner

        Vector3 offset = movementVector * movementFactor;

        transform.position = startingPosition + offset;
    }
}
