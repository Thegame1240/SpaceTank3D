﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inertia : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.angularVelocity = rb.inertiaTensor;
    }
}
