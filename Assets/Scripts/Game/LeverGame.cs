/*----------------------------------------
File Name: LeverGame.cpp
Purpose: Handles lever game
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections;
using UnityEngine;

/// <summary>
/// Handles lever game
/// </summary>
public class LeverGame : MiniGame
{
    // Start is called before the first frame update
    public float leverEndTime = 30;
    public float countdownSpeed = 1;
    float currentTime = 0;
    CountdownTimerDisplay display;

    private AudioSource audiosource;
    public AudioSource bears;
    public AudioClip[] audioClips;

    /// <summary>
    /// Overriden method for when scripts starts
    /// </summary>
    protected override void Start()
    {
        base.Start();
        currentTime = leverEndTime;
        display = GetComponentInChildren<CountdownTimerDisplay>();
        display.DisplayTime(currentTime);
        audiosource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Checks if mini-game has failed
    /// </summary>
    protected override void CheckFailedGame()
    {
        //Checks if current time is zero
        if (currentTime <= 0)
        {
            FailMiniGame();
            TimeResetFail();
            PlayBearSFX(2);
        }
    }

    /// <summary>
    /// Coroutine for game loop
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator GameSequence()
    {
        currentTime = leverEndTime;
        while (controller.isRunning)
        {
            currentTime -= 1;
            display.DisplayTime(currentTime);
            CheckFailedGame();
            yield return new WaitForSeconds(nextSequenceWaitTime / leverEndTime);
            
        }
    }

    /// <summary>
    /// Resets time to max amount on fail
    /// </summary>
    public void TimeResetFail()
    {
        currentTime = leverEndTime;
    }

    /// <summary>
    /// Resets time to max amount on success
    /// </summary>
    public void TimeResetSuccess()
    {
        currentTime = leverEndTime;
        display.DisplayTime(currentTime);
        PlaySFX(1);
    }


    /// <summary>
    /// Plays sfx from audio source
    /// </summary>
    /// <param name="index">Which source to play from array?</param>
    public void PlaySFX(int index)
    {
        audiosource.PlayOneShot(audioClips[index], 1f);
    }


    /// <summary>
    /// Plays sfx from bear audio source
    /// </summary>
    /// <param name="index">Which source to play from array?</param>
    public void PlayBearSFX(int index)
    {
        bears.PlayOneShot(audioClips[index], 0.3f);
    }
}
