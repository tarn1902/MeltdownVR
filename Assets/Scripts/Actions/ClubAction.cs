/*----------------------------------------
File Name: ClubAction.cs
Purpose: Handles how club lever works
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using Valve.VR.InteractionSystem;
/// <summary>
/// Handles how club lever works
/// </summary>
public class ClubAction : CircularDrive
{
    public ClubGame clubGame;
    public float centerSections;
    public float sectionCount = 4;
    public float sectionRange = 0;
    public Section curretSection;
    public ushort hapticFeedbackTime = 10000;
    public GameObject indicator;
    public GameObject bolt;
    public float maxOffset;
    float offset = 0;
    Vector3 currentOffsetedRotation;

    /// <summary>
    /// New method when scripts starts
    /// </summary>
    new private void Start()
    {
        base.Start();
        centerSections = 1.0f / (sectionCount - 1);
        sectionRange = (centerSections) / 2;
        minMaxAngularThreshold = 100.0f;
    }

    /// <summary>
    /// Handles when a hand is over a interactible object
    /// </summary>
    /// <param name="hand">hand (Hand): Which hand grabbed it?</param>
    new protected void HandHoverUpdate(Hand hand)
    {
        bool isGrabEnding = hand.IsGrabbingWithType(grabbedWithType) == false;
        if (grabbedWithType != GrabTypes.None && isGrabEnding)
        {
            outAngle = Mathf.Lerp(minAngle, maxAngle, centerSections * (int)curretSection);
            linearMapping.value = Mathf.InverseLerp(0, 1, centerSections * (int)curretSection);
            UpdateAll();
        }
        base.HandHoverUpdate(hand);
        for (int i = 0; i < sectionCount; i++)
        {
            if (linearMapping.value <= centerSections * i + sectionRange && linearMapping.value >= centerSections * i - sectionRange)
            {
                if (curretSection != (Section)i)
                {
                    hand.TriggerHapticPulse(hapticFeedbackTime);
                    clubGame.PlaySFX(0);
                }
                curretSection = (Section)i;
            }
        }
        
    }

    /// <summary>
    /// Updates each frame
    /// </summary>
    private void Update()
    {
        offset = Mathf.Lerp(-maxOffset, maxOffset, linearMapping.value);
        currentOffsetedRotation = transform.rotation.eulerAngles;
        currentOffsetedRotation.x += offset;
        indicator.transform.rotation = Quaternion.Euler(currentOffsetedRotation);
        bolt.transform.rotation = transform.rotation;
    }

    /// <summary>
    /// Each section of club lever
    /// </summary>
    public enum Section
    { 
        S1,
        S2,
        S3,
        S4,
        S5,
        S6,
        S7,
        S8,
    }


}
