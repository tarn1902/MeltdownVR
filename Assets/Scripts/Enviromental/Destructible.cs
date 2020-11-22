/*----------------------------------------
File Name: Destructible.cs
Purpose: Makes object destructible
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

/// <summary>
/// Makes object destructible
/// </summary>
public class Destructible : MonoBehaviour
{
    Rigidbody rb;
    public float maxForce = 10;
    public GameObject brokenObjects;

    /// <summary>
    /// New method when scripts starts
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Handles what happen when collides
    /// </summary>
    /// <param name="collision">What is the collision?</param>
    private void OnCollisionEnter(Collision collision)
    {
        //Checks if enough force is given in
        if (rb.velocity.magnitude >= maxForce)
        {
            brokenObjects.SetActive(true);
            brokenObjects.transform.parent = transform.parent;
            brokenObjects.transform.position = transform.position;
            brokenObjects.transform.rotation = transform.rotation;
            Destroy(gameObject);
        }
    }
}
