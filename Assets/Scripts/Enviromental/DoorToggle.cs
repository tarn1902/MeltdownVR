/*----------------------------------------
File Name: DoorToggle.cs
Purpose: handles door state
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// handles door state
/// </summary>
public class DoorToggle : CircularDrive
{
    public bool isAutomatic = false;
    public Animation anim;
    Rigidbody rb;
    float lastAngle = 0;

    /// <summary>
    /// New method when scripts starts
    /// </summary>
    new private void Start()
    {
        rb = GetComponent<Rigidbody>();
        base.Start();
    }

    /// <summary>
    /// Updates each frame
    /// </summary>
    private void Update()
    {
        outAngle = Vector3.Angle(transform.parent.forward, transform.forward);
    }

    /// <summary>
    /// Trigger opening of door
    /// </summary>
    public void OpenDoor()
    {
        Open();
    }

    /// <summary>
    /// Triggers closing of door
    /// </summary>
    public void CloseDoor()
    {
        Close();
    }

    /// <summary>
    /// Handles when a hand started hovering over interactible object
    /// </summary>
    /// <param name="hand">Which hand grabbed it?</param>
    new protected void OnHandHoverBegin(Hand hand)
    {
        //Check if can open it by hand
        if (!isAutomatic)
            base.OnHandHoverBegin(hand);
    }

    /// <summary>
    /// Handles when a hand is hovering over a interactible object
    /// </summary>
    /// <param name="hand">Which hand grabbed it?</param>
    new protected void HandHoverUpdate(Hand hand)
    {
        //Check if can open it by hand
        if (!isAutomatic)
        {
            GrabTypes startingGrabType = hand.GetGrabStarting();
            bool isGrabEnding = hand.IsGrabbingWithType(grabbedWithType) == false;
            //Checks if grabbed
            if (grabbedWithType == GrabTypes.None && startingGrabType != GrabTypes.None)
            {
                rb.angularVelocity = Vector3.zero;
            }
            //Checks if let go
            else if (grabbedWithType != GrabTypes.None && isGrabEnding)
            {
                rb.angularVelocity = transform.up * (outAngle - lastAngle);
            }
            lastAngle = outAngle;
            base.HandHoverUpdate(hand);
        }
    }

    /// <summary>
    /// Handles when a hand stops hovering over a interactible object
    /// </summary>
    /// <param name="hand">Which hand grabbed it?</param>
    new protected void OnHandHoverEnd(Hand hand)
    {
        //Check if can open it by hand
        if (!isAutomatic)
            base.OnHandHoverEnd(hand);
    }

    /// <summary>
    /// Default open state and animation
    /// </summary>
    void Open()
    {
        linearMapping.value = 1;
        outAngle = maxAngle;
        anim.Play("OpenDoor");
    }

    /// <summary>
    /// Default close state and animation
    /// </summary>
    void Close()
    {
        linearMapping.value = 0;
        outAngle = minAngle;
        anim.Play("CloseDoor");
    }

}
