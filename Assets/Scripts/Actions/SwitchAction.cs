/*----------------------------------------
File Name: SwitchAction.cs
Purpose: Handles action of switch
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.Events;

/// <summary>
/// Handles action of switch
/// </summary>
public class SwitchAction : CircularDrive
{
    public SwitchGame game;
    float localMap = 0;
    public UnityEvent onToggleA;
    public UnityEvent onToggleB;

    /// <summary>
    /// New method when scripts starts
    /// </summary>
    new private void Start()
    {
        base.Start();
        minMaxAngularThreshold = 100.0f;
    }

    /// <summary>
    /// new Handler when a hand is over a interactible object
    /// </summary>
    /// <param name="hand">Which hand grabbed it?</param>
    new protected void HandHoverUpdate(Hand hand)
    {
        bool isGrabEnding = hand.IsGrabbingWithType(grabbedWithType) == false;
        //Tests if letting go of interactible
 
        if (grabbedWithType != GrabTypes.None && isGrabEnding)
        {
            SnapSwitch();
        }
        base.HandHoverUpdate(hand);
    }

    /// <summary>
    /// new Handler when a hand is moving away from interactible object
    /// </summary>
    /// <param name="hand">Which hand grabbed it?</param>
    new protected void OnHandHoverEnd(Hand hand)
    {
        //Tests if letting go of interactible when going out of range
        if (hand.IsGrabbingWithType(grabbedWithType))
        {
            SnapSwitch();
        }
        base.OnHandHoverEnd(hand);
    }

    /// <summary>
    /// Snaps switch to closest side if in range
    /// </summary>
    void SnapSwitch()
    {
        if (Mathf.Abs(localMap - linearMapping.value) <= 0.075)
            return;
            
        localMap = linearMapping.value;
        if (linearMapping.value <= 0.35)
        {
            outAngle = minAngle;
            linearMapping.value = 0;
            onToggleA.Invoke();
        }
        if (linearMapping.value >= 0.65)
        {
            outAngle = maxAngle;
            linearMapping.value = 1;
            onToggleB.Invoke();
        }
        UpdateAll();
        
    }
        
}
