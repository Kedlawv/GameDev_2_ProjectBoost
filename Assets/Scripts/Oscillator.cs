using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Oscillator : MonoBehaviour

{
    Vector3 startingPosition;
    float movementFactor;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = this.transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } // defend agains division by 0,
                                                 // use epsilon instead of ==0 because this is two floats
       
        float cycles = Time.time / period; // continually growing over time

        const float tau = Mathf.PI * 2; // constant value of 6.283 
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2; // recalculated to go from 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
