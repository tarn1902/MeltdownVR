/*----------------------------------------
File Name: SquishyGame.cpp
Purpose: Handles squishy interaction
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections;
using UnityEngine;
/// <summary>
/// Handles squishy interaction
/// </summary>
public class SquishyGame : MiniGame
{
    public FanRotation fan;
    AudioSource audioSource;
    public AudioClip[] audioClips;
    float deltaTime = 0;
    public float batteryIncreaseRate = 0.1f;
    public float batteryDecreaseRate = 0.1f;
    public float batteryLevel = 1;
    public float maxBatteryLevel = 1;
    bool sequenceStarted = false;

    /// <summary>
    /// Overriden method for when scripts starts
    /// </summary>
    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Checks if mini-game has failed
    /// </summary>
    protected override void CheckFailedGame()
    {
        if (batteryLevel <= 0)
        {
            FailMiniGame();
            batteryLevel = maxBatteryLevel;
        }
    }

    /// <summary>
    /// new method for when scripts updates
    /// </summary>
    new private void Update()
    {
        base.Update();
        if (sequenceStarted)
        {
            //Check if battery is at zero
            if (batteryLevel <= 0)
                batteryLevel = 0;
            else
                batteryLevel -= batteryDecreaseRate * deltaTime * Time.deltaTime;
        }

    }

    /// <summary>
    /// Coroutine for game loop
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator GameSequence()
    {
        sequenceStarted = true;
        while (controller.isRunning)
        {
            deltaTime = nextSequenceWaitTime / maxBatteryLevel;
            CheckFailedGame();
            yield return new WaitForSeconds(deltaTime);
        }
    }

    /// <summary>
    /// Plays sfx from audio source
    /// </summary>
    /// <param name="index">Which source to play from array?</param>
    public void PlaySFX (int index)
    {
        audioSource.PlayOneShot(audioClips[index], 0.5f);
    }

    /// <summary>
    /// Increase battery
    /// </summary>
    public void IncreaseBattery()
    {
        batteryLevel += batteryIncreaseRate;
        batteryLevel = Mathf.Clamp(batteryLevel, 0, maxBatteryLevel);
    }

    /// <summary>
    /// Triggered to spin the fan
    /// </summary>
    public void SpinFan()
    {
        IncreaseBattery();
        PlaySFX(0);
    }
}
