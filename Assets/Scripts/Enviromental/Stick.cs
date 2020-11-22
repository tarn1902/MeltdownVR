/*----------------------------------------
File Name: Stick.cpp
Purpose: Stick object into place
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

/// <summary>
/// Stick object into place
/// </summary>
public class Stick : MonoBehaviour
{
    Rigidbody rb;
    public bool isStickTemp = false;

    /// <summary>
    /// Method for when scripts starts
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Handles event when collision occurs
    /// </summary>
    /// <param name="collision">What was the collision?</param>
    private void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.parent = collision.transform;
    }

    /// <summary>
    /// Handles event when collision is resolved
    /// </summary>
    /// <param name="collision">What was the collision?</param>
    private void OnCollisionExit(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.None;
        transform.parent = null;
        //Is script temporary
        if (isStickTemp)
            Destroy(this);
    }
}
