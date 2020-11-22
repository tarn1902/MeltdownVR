/*----------------------------------------
File Name: PulleyAction.cpp
Purpose: Handles pulley device action
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Handles pulley device action
/// </summary>
public class PulleyAction : LinearDrive
{
    public bool isReseting = false;
    public PulleyGame game;
    public float returnSpeed = 10;

    /// <summary>
    /// New method when scripts starts
    /// </summary>
    new protected void Start()
    {
        base.Start();
        game = GetComponentInParent<PulleyGame>();
    }

    /// <summary>
    /// new function for updates each frame
    /// </summary>
    new private void Update()
    {
        //Tests if returning to original position
        if(isReseting)
        {
            transform.position = Vector3.Lerp(startPosition.position, endPosition.position, linearMapping.value);
            linearMapping.value -= Time.deltaTime * returnSpeed;
            //Tests if returned to position
            if (linearMapping.value <= 0)
            {
                isReseting = false;
                linearMapping.value = 0;
            }
        }
        base.Update();
    }


    /// <summary>
    /// Handles event when interactble is let go
    /// </summary>
    /// <param name="hand">What hand was used?</param>
    new protected virtual void OnDetachedFromHand(Hand hand)
    {
        base.OnDetachedFromHand(hand);
        isReseting = true;
    }
}
