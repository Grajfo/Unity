﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accel_movement : MonoBehaviour
{
    public float smooth = 0.4f;
    public float newRotation;
    public float sensitivity = 6;
    private Vector3 currentAcceleration, initialAcceleration;
    void Start()
    {
        initialAcceleration = Input.acceleration;
        currentAcceleration = Vector3.zero;
    }

    void Update()
    {
        //pre-processing

        currentAcceleration = Vector3.Lerp(currentAcceleration, Input.acceleration - initialAcceleration, Time.deltaTime / smooth);

        newRotation = Mathf.Clamp(currentAcceleration.x * sensitivity, -1, 1);
        transform.Rotate(0, 0, -newRotation);
    }
}

