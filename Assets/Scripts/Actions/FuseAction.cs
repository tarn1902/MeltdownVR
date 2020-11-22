/*----------------------------------------
File Name: FuseAction.cs
Purpose: Handles how working fuse works
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.Events;
/// <summary>
/// Handles how working fuse works
/// </summary>
public class FuseAction : Throwable
{
    bool isPlaceable = false;
    bool isGrabable = false;
    GameObject parent = null;
    bool isGame1 = true;
    FuseGame game = null;
    FuseGame2 game2 = null;
    Rigidbody rb = null;
    Transform spawnedLocation;
    public float maxRange = 10;
    public UnityEvent onReset;
    MeshRenderer highlight = null;

    /// <summary>
    /// Overrided function when script is created
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        parent = transform.parent.parent.gameObject;
        game = GetComponentInParent<FuseGame>();
        if (game == null)
        {
            game2 = GetComponentInParent<FuseGame2>();
            isGame1 = false;
        }
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Moves fuse to new location
    /// </summary>
    /// <param name="location">Where is fuse going?</param>
    public void MoveToNewSpawn(Transform location)
    {
        spawnedLocation = location;
        transform.position = location.position;
        transform.rotation = location.rotation;
        rb.constraints = RigidbodyConstraints.None;
        isGrabable = true;
    }

    /// <summary>
    /// Handles event when enters trigger
    /// </summary>
    /// <param name="other">What is other collider?</param>
    private void OnTriggerEnter(Collider other)
    {
        //Tests if collider is tagged "FuseGhost" and isGrabable object
        if (other.tag == "FuseGhost" && isGrabable)
        {
            isPlaceable = true;
            highlight = other.GetComponent<MeshRenderer>();
            highlight.enabled = true;
        }
    }

    /// <summary>
    /// Handles event when exits trigger
    /// </summary>
    /// <param name="other">What is other collider?</param>
    private void OnTriggerExit(Collider other)
    {
        //Tests if collider is tagged "FuseGhost"
        if (other.tag == "FuseGhost")
        {
            isPlaceable = false;
            highlight = other.GetComponent<MeshRenderer>();
            highlight.enabled = false;
        }
        
    }

    /// <summary>
    /// Handles event when interactible is detached from hand
    /// </summary>
    /// <param name="hand">What hand is grabbing it?</param>
    protected override void OnDetachedFromHand(Hand hand)
    {
        base.OnDetachedFromHand(hand);
        //Tests if in location that can reset it's state
        if (isPlaceable)
        {
            ResetFuse();
        }
    }

    /// <summary>
    /// Handles event when interactible is attached to hand
    /// </summary>
    /// <param name="hand">What hand is grabbing it?</param>
    protected override void OnAttachedToHand(Hand hand)
    {
        base.OnAttachedToHand(hand);
    }

    /// <summary>
    /// Handles event when hand is hovering over interactible
    /// </summary>
    /// <param name="hand">What hand is hovering over it?</param>
    protected override void HandHoverUpdate(Hand hand)
    {
        //Tests if is a object that can be grabbed
        if (isGrabable)
            base.HandHoverUpdate(hand);
    }

    /// <summary>
    /// Resets fuse to default state
    /// </summary>
    public void ResetFuse()
    {
        transform.parent = parent.transform;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        if (isGame1)
            game.fusePlaced = true;
        else
            game2.fusePlaced = true;
        isGrabable = false;
        isPlaceable = false;
        highlight.enabled = false;
        onReset.Invoke();
    }

    /// <summary>
    /// Updates each frame
    /// </summary>
    private void Update()
    {
        if (spawnedLocation != null && Vector3.Distance(transform.position, spawnedLocation.position) >= maxRange )
        {
            MoveToNewSpawn(spawnedLocation);
            rb.velocity = Vector3.zero;
        }
    }
}
