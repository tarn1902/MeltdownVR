/*----------------------------------------
File Name: SquishAction.cs
Purpose: Handles action of squishing object
Author: Tarn Cooper
Modified: 16 Novemver 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

/// <summary>
/// Handles action of squishing object
/// </summary>
public class SquishAction : MonoBehaviour
{
    public float maxRange = 10;
    public SteamVR_Action_Boolean squishAction;
    SquishyGame game = null;

    /// <summary>
    /// method when script starts
    /// </summary>
    private void Start()
    {
        game = GetComponentInParent<SquishyGame>();
    }

    /// <summary>
    /// Handles when a hand is over a interactible object
    /// </summary>
    /// <param name="hand">Which hand grabbed it?</param>
    protected void HandHoverUpdate(Hand hand)
    {
        //Tests if action is pressed on hand
        if (squishAction.GetStateDown(hand.handType))
        {
            game.SpinFan();
        }
    }
}
