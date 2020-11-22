/*----------------------------------------
File Name: SetCenterOfMass.cpp
Purpose: Sets center of mass of rigidbody
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

/// <summary>
/// Sets center of mass of rigidbody
/// </summary>
public class SetCenterOfMass : MonoBehaviour
{
    public Vector3 com;
    public Rigidbody rb;

    /// <summary>
    /// Method for when scripts starts
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
    }
}
