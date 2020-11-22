/*----------------------------------------
File Name: WheelAction.cs
Purpose: Handles actions of wheel/crank
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.Events;

/// <summary>
/// Handles actions of wheel/crank
/// </summary>
public class WheelAction : CircularDrive
{
    Rigidbody rb;
    float lastAngle;
    public UnityEvent onTurning;
    public float totalDif = 0;
    float changeDif = 0;
    public float maxAngleChange = 3.0f;
    float outAnglePrevious = 0;
    public int turnCount = 0;
    float maxRotation = 360;
    public UnityEvent onFullTurn;

    /// <summary>
    /// New method when scripts starts
    /// </summary>
    new private void Start()
	{
        rb = GetComponent<Rigidbody>();
        base.Start();
        outAnglePrevious = outAngle;
    }

    /// <summary>
    /// function for updates each frame
    /// </summary>
    private void Update()
	{
        RotationCheck();
        //Tests if rotating
        if (Mathf.Abs(changeDif) > maxAngleChange)
            onTurning.Invoke();
    }

    /// <summary>
    /// Checks if wheel is rotating
    /// </summary>
    public void RotationCheck()
    {
        
        outAngle = transform.localRotation.eulerAngles.y;
        //Checks if rotating correct way
        if (outAngle - outAnglePrevious == Mathf.Abs(outAngle - outAnglePrevious))
        {
            totalDif += Mathf.Abs(outAngle - outAnglePrevious);
        }
        outAnglePrevious = outAngle;
        //Test if does full rotation
        if (totalDif >= maxRotation)
        {
            totalDif -= maxRotation;
            turnCount++;
            onFullTurn.Invoke();
        }
    }

    /// <summary>
    /// Handles when a hand is over a interactible object
    /// </summary>
    /// <param name="hand">Which hand grabbed it?</param>
    new protected void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabbingWithType(grabbedWithType) == false;
        //Checks if grabbing
        if (grabbedWithType == GrabTypes.None && startingGrabType != GrabTypes.None)
        {
            rb.angularVelocity = Vector3.zero;
        }
        //Checks if stops grabbing
        else if (grabbedWithType != GrabTypes.None && isGrabEnding)
        {
            rb.angularVelocity = transform.up * (outAngle - lastAngle);
        }
        lastAngle = outAngle;
        base.HandHoverUpdate(hand);
    }

}
