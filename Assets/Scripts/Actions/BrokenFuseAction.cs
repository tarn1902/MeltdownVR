/*----------------------------------------
File Name: BrokenFuseAction.cs
Purpose: Handles interaction when broken 
fuse is grabbed
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using Valve.VR.InteractionSystem;
/// <summary>
/// Handles interaction when broken
/// </summary>
public class BrokenFuseAction : Throwable
{
    public float maxCollisionSpeed = 10f;
    public float maxDistance = 10f;

    /// <summary>
    /// Handles when collision occurs
    /// </summary>
    /// <param name="collision">What collision occured?</param>
    private void OnCollisionEnter(Collision collision)
    {
        //Tests if moving fast enough
        if (rigidbody.velocity.magnitude >= maxCollisionSpeed)
        {
            ResetFuse();
        }
    }

    /// <summary>
    /// Updates each frame
    /// </summary>
    private void Update()
    {
        //Tests if far enough from parent
        if (transform.localPosition.magnitude >= maxDistance)
        {
            ResetFuse();
        }
    }

    /// <summary>
    /// Handles when grabbed by steamVR interaction
    /// </summary>
    /// <param name="hand">Which hand grabbed it?</param>
    protected override void OnAttachedToHand(Hand hand)
    {
        base.OnAttachedToHand(hand);
        //Removes constraints.
        rigidbody.constraints = RigidbodyConstraints.None;
    }

    /// <summary>
    /// Resets fuse location and state
    /// </summary>
    void ResetFuse()
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        gameObject.SetActive(false);
    }
}
