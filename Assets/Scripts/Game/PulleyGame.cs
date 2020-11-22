/*----------------------------------------
File Name: PulleyGame.cs
Purpose: Handles pulley game
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

/// <summary>
/// Handles pulley interaction
/// </summary>
public class PulleyGame : MiniGame
{
    public Slider gauge;
    LinearMapping map;
    [Range(0.0f, 1.0f)]
    public float pullAmount = 0.5f;
    public float steamTime = 5;
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    private bool playedAudio;

    int fullFill = 100;
    public float decreaseAmount = 2;
    public float increaseAmount = 2;
    bool isSteaming = false;
    float deltaTime = 1;
    bool sequenceStarted = false;
    bool isReleasing = false;

    /// <summary>
    /// Overriden method for when scripts starts
    /// </summary>
    protected override void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gauge.maxValue = fullFill;
        base.Start();
        map = GetComponentInChildren<LinearMapping>();
    }

    /// <summary>
    /// Overriden method for when scripts updates
    /// </summary>
    protected override void Update()
    {
        base.Update();
        //Checks if sequence started
        if (sequenceStarted)
        {
            //Checks if pull amount is enough or is reseting to positon
            if (map.value >= pullAmount || isReleasing)
            {
                gauge.value -= decreaseAmount * Time.deltaTime;
                //Checks if audio was played
                if (!playedAudio)
                {
                    PlaySFX(1);
                    playedAudio = true;
                    isSteaming = false;
                }
                //Checks if is reset to orignal position
                if (gauge.value <= 0)
                {
                    isReleasing = false;
                }
            }
            else
            {
                gauge.value += increaseAmount * deltaTime;
                playedAudio = false;
                //Plays audio when nearing max amount
                if (gauge.value >= (fullFill - (fullFill * steamTime) * increaseAmount) && !isSteaming)
                {
                    PlaySFX(0);
                    isSteaming = true;
                }
            }
        }
    }

    /// <summary>
    /// Checks if mini-game has failed
    /// </summary>
    protected override void CheckFailedGame()
    {
        if (gauge.value >= gauge.maxValue)
        {
            FailMiniGame();
            isSteaming =  false;
            isReleasing = true;
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
            deltaTime = nextSequenceWaitTime / fullFill;
            CheckFailedGame();
            yield return new WaitForSeconds(deltaTime);
        }
    }


    /// <summary>
    /// Plays sfx from audio source
    /// </summary>
    /// <param name="index">Which source to play from array?</param>
    public void PlaySFX(int index)
    {
        audioSource.PlayOneShot(audioClips[index], 1f);
    }
}
