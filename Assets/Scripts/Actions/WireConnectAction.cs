/*----------------------------------------
File Name: WireConnectAction.cpp
Purpose: Handles acttion of connection wires
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Handles acttion of connection wires
/// </summary>
public class WireConnectAction : Throwable
{
    bool isPlaceable = false;
    public Transform startPoint = null;
    public Transform startWireConnectPoint = null;

    Rigidbody rb;
    LineRenderer line;
    GameObject wireConnectPoint;

    /// <summary>
    /// overrides method when script is created
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        line = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
        transform.position = startWireConnectPoint.position;
        line.SetPosition(0, startPoint.position);
        line.SetPosition(1, transform.position);
    }

    /// <summary>
    /// Handles event when interactble is let go
    /// </summary>
    /// <param name="hand">What hand was used?</param>
    protected override void OnDetachedFromHand(Hand hand)
    {
        base.OnDetachedFromHand(hand);
        if (isPlaceable)
            AttachWire();
    }

    /// <summary>
    /// Handles event when interactble is grabbed
    /// </summary>
    /// <param name="hand">What hand was used?</param>
    protected override void HandAttachedUpdate(Hand hand)
    {
        base.HandAttachedUpdate(hand);
        line.SetPosition(0, startPoint.position);
        line.SetPosition(1, transform.position);
    }

    /// <summary>
    /// Handes entering trigger events
    /// </summary>
    /// <param name="other">What is the other collider?</param>
    private void OnTriggerEnter(Collider other)
    {
        //Tests if wire is near connect point
        if (other.tag == "WireConnectPoint")
        {
            isPlaceable = true;
            wireConnectPoint = other.gameObject;
        }
    }

    /// <summary>
    /// Handes exiting trigger events
    /// </summary>
    /// <param name="other">What is the other collider?</param>
    private void OnTriggerExit(Collider other)
    {
        isPlaceable = false;
        wireConnectPoint = null;
    }

    /// <summary>
    /// Attached wire to end
    /// </summary>
    void AttachWire()
    {
        transform.position = wireConnectPoint.transform.position;
        transform.rotation = wireConnectPoint.transform.rotation;
    }
}
