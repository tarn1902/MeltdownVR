/*----------------------------------------
File Name: FanRotation.cpp
Purpose: Rotates fan based on speed
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

/// <summary>
/// Rotates fan based on speed
/// </summary>
public class FanRotation : MonoBehaviour
{
    SquishyGame squishyGame;
    public float maxFanSpeed = 10;
    /// <summary>
    /// Method for when scripts starts
    /// </summary>
    private void Start()
    {
        squishyGame = GetComponentInParent<SquishyGame>();
    }

    /// <summary>
    /// function for updates each frame
    /// </summary>
    private void Update()
    {
        transform.Rotate(transform.forward, maxFanSpeed * squishyGame.batteryLevel * Time.deltaTime, Space.World);
    }
}
