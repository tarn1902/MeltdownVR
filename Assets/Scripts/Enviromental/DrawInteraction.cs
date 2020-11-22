/*----------------------------------------
File Name: DrawInteraction.cs
Purpose: Handles draw interactions
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Handles draw interactions
/// </summary>
public class DrawInteraction : LinearDrive
{
    public float endPoint = 1;
    /// <summary>
    /// New method when scripts starts
    /// </summary>
    new private void Start()
    {
        endPosition.position = startPosition.position + -startPosition.forward * endPoint;
        base.Start();
    }

    /// <summary>
    /// Updates each frame
    /// </summary>
    new private void Update()
    {
        linearMapping.value = CalculateLinearMapping(transform);
        base.Update();
    }

    /// <summary>
    /// Handles event when entering trigger
    /// </summary>
    /// <param name="other">What is other collider?</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Placeable")
            other.transform.parent = transform;
        else if (other.transform.parent.tag == "Placeable")
            other.transform.parent.parent = transform;
    }

    /// <summary>
    /// Handles event when exiting trigger
    /// </summary>
    /// <param name="other">What is other collider?</param>
    private void OnTriggerExit(Collider other)
    {
        //Checks if object can be placed
        if (other.tag == "Placeable")
            other.transform.parent = null;
        //Checks if parent is placeable
        else if (other.transform.parent.tag == "Placeable")
            other.transform.parent.parent = null;
    }
}


