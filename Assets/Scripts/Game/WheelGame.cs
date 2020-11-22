/*----------------------------------------
File Name: WheelGame.cs
Purpose: Handles wheel mini game
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections;
using UnityEngine;

/// <summary>
/// Handles wheel mini game
/// </summary>
public class WheelGame : MiniGame
{
    int turnCount = 0;
    public int turnAmount = 10;
    WheelAction action = null;
    LightToggle toggle = null;
    public float blinkTime = 1;
    bool isBlinking = false;
    public float maxAngleChange = 3.0f;
    float outAnglePrevious = 0;
    AudioSource audioSource;
    public AudioSource audioSourceOneshot;
    public AudioClip ding;
    bool change = true;
    float changeDif = 0;

    /// <summary>
    /// Overriden method for when scripts starts
    /// </summary>
    protected override void Start()
    {
        base.Start();
        action = GetComponentInChildren<WheelAction>();
        toggle = GetComponentInChildren<LightToggle>();
        outAnglePrevious = action.outAngle;
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// new method for when scripts updates
    /// </summary>
    new private void Update()
    {
        changeDif = action.outAngle - outAnglePrevious;
        //Checks if wheel is moving
        if (Mathf.Abs(changeDif) > maxAngleChange)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        //Checks if wheel has stopped
        else if (Mathf.Abs(changeDif) <= maxAngleChange)
        {
            audioSource.Stop();
        }
        outAnglePrevious = action.outAngle;
        base.Update();
    }

    /// <summary>
    /// Checks if mini-game has failed
    /// </summary>
    protected override void CheckFailedGame()
    {
        //Check if wheel was turned a certain amount
        if (turnCount < turnAmount)
        {
            FailMiniGame();
        }
        turnCount = 0;
        audioSourceOneshot.pitch = 1.0f;
        isBlinking = false;
    }

    /// <summary>
    /// Coroutine for game loop
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator GameSequence()
    {
        StartRotationListener();
        while (controller.isRunning)
        {
            //Check what state game is in
            if(change)
                StartBlink();
            else
                CheckFailedGame();
            change = !change;
            yield return new WaitForSeconds(nextSequenceWaitTime);
        }
    }

    /// <summary>
    /// Starts blink sequence
    /// </summary>
    void StartBlink()
    {
        isBlinking = true;
        StartCoroutine("Blink");
    }

    /// <summary>
    /// Blinks till wheel has turned a certain amount or game fails
    /// </summary>
    /// <returns>IEnumerator</returns>
    IEnumerator Blink()
    {
        while (isBlinking)
        {
            //Check if turned a cetain amount of times
            if (turnCount >= turnAmount)
                break;
            toggle.TurnOff();
            yield return new WaitForSeconds(blinkTime);
            toggle.TurnOn();
            yield return new WaitForSeconds(blinkTime);
        }
        toggle.TurnOff();

    }

    //starts the listener for rotation of wheel
    void StartRotationListener()
    {
        StartCoroutine("RotationListener");
    }

    /// <summary>
    /// listens for rotation of wheel
    /// </summary>
    /// <returns>IEnumerator</returns>
    IEnumerator RotationListener()
    {
        float totalDif = 0;
        while (controller.isRunning)
        {
            //Check if changed or is in correct state
            if (changeDif <= 0 || change)
            {
                yield return null;
                continue;
            }
            totalDif += changeDif; 
            //Check if rotated full 360 and can keep adding turn amount
            if (totalDif >= 360 && (turnCount < turnAmount))
            {
                turnCount++;
                totalDif = 0;
                audioSourceOneshot.PlayOneShot(ding, 1f);
                audioSourceOneshot.pitch += 0.05f;
            }
            yield return null;
        }

        //Check if game is not running
        if (!controller.isRunning)
        {
            audioSource.Stop();
        }
    }

}
