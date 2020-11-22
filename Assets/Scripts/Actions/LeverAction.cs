/*----------------------------------------
File Name: LeverAction.cpp
Purpose: 
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.Events;

/// <summary>
/// Handles actions of wall lever
/// </summary>
public class LeverAction : CircularDrive
{
    public float maxAngleRange = 25.0f;
    public LeverGame2 game;
    Animation anim;
    Rigidbody rb;
    float lastAngle = 0;
    bool isReseting = false;
    public UnityEvent onReset;

    /// <summary>
    /// New method when scripts starts
    /// </summary>
    new private void Start()
    {
        base.Start();
        anim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Updates each frame
    /// </summary>
    private void Update()
    {
        //Tests if at max angle
        outAngle = Vector3.Angle(transform.parent.up, transform.up);
        if (Mathf.Abs(maxAngle - outAngle) <= maxAngleRange && !isReseting)
        {
            ResetLever();
        }
        //Tests if playing animation
        else if (!anim.isPlaying)
        {
            isReseting = false;
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
        //Tests if lever is grabbed
        if (grabbedWithType == GrabTypes.None && startingGrabType != GrabTypes.None)
        {
            rb.angularVelocity = Vector3.zero;
        }
        //Tests if lever is being let go
        else if (grabbedWithType != GrabTypes.None && isGrabEnding)
        {
            rb.angularVelocity = transform.right * (outAngle - lastAngle);
        }
        lastAngle = outAngle;
        base.HandHoverUpdate(hand);
    }

    /// <summary>
    /// Resets lever to default state and plays animation
    /// </summary>
    void ResetLever()
    {
        isReseting = true;
        linearMapping.value = 0;
        outAngle = 0;
        rb.angularVelocity = Vector3.zero;
        anim.Play();
        onReset.Invoke();
    }
}
