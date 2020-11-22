/*----------------------------------------
File Name: CutAction.cs
Purpose: Handles cutting of wires
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper
-----------------------------------*/
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// Handles cutting of wires
/// </summary>
public class CutAction : MonoBehaviour
{
    private WireCutGame game;
    public SteamVR_Action_Boolean cutAction;
    public GameObject cutWire = null;
    public Color wireColor;
    private MeshRenderer meshRenderer;

    /// <summary>
    /// Method when script is created
    /// </summary>
    private void Awake()
    {
        game = GetComponentInParent<WireCutGame>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = wireColor;
    }

    /// <summary>
    /// Handles when a hand is over a interactible object
    /// </summary>
    /// <param name="hand">Which hand grabbed it?</param>
    protected void HandHoverUpdate(Hand hand)
    {
        //Checks if not cut and hand has attempted to cut
        if (cutAction.GetState(hand.handType) && !game.wasCut)
        {
            cutWire.SetActive(true);
            meshRenderer.enabled = false;
            game.WireIsCut(this);
            game.PlaySFX(1);
        }
    }

    /// <summary>
    /// Resets wire to default state
    /// </summary>
    public void ResetWire()
    {
        cutWire.SetActive(false);
        meshRenderer.enabled = true;
    }
}
