/*----------------------------------------
File Name: DialGame.cpp
Purpose: Handles how old dial system works
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections;
using UnityEngine;

/// <summary>
/// Handles how old dial system works
/// </summary>
public class DialGame : MiniGame
{
    public GameObject dial;
    public GameObject indicator;
    public float diffRange = 10;
    private DialAction action;
    public float failWaitTime = 5;
    public float failStartWaitTime = 5;
    private bool isfailWaiting = false;
    bool isFailStarted = false;
    bool isFailStart = true;
    float fullRotation = 360;

    public AudioClip[] audioClips;
    public AudioSource beeAudioSource;

    /// <summary>
    /// Checks if in safe zone
    /// </summary>
    bool IsInZone { get { return Mathf.Abs(action.outAngle - indicator.transform.localRotation.eulerAngles.y) <= diffRange; } }

    /// <summary>
    /// Overriden method for when scripts starts
    /// </summary>
    protected override void Start()
    {
        base.Start();
        action = GetComponentInChildren<DialAction>();
    }

    /// <summary>
    /// Coroutine for game loop
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator GameSequence()
    {
        //Checks if game is running
        while (controller.isRunning)
        {
            //Checks if out of zone
            if (!IsInZone)
            {
                //Checks if can start fail sequence
                if (isFailStart)
                    StartCoroutine("FailStartBuffer");
                //Checks if fail sequence is no longer waiting and has started
                else if (!isfailWaiting && isFailStarted)
                    CheckFailedGame();
            }
            //Checks if can start fail sequence
            else if (!isFailStart)
            {
                isFailStart = true;
                isFailStarted = false;
                isfailWaiting = false;
            }
            indicator.transform.Rotate(Vector3.up, 1);
            yield return new WaitForSeconds(nextSequenceWaitTime / fullRotation);

        }

    }

    /// <summary>
    /// Checks if mini-game has failed
    /// </summary>
    protected override void CheckFailedGame()
    {
        FailMiniGame();
        isfailWaiting = true;
        StartCoroutine("FailBuffer");
        PlayBeeSFX(0);
    }

    /// <summary>
    /// Delay till next fail can happen
    /// </summary>
    /// <returns>IEnumerator</returns>
    IEnumerator FailBuffer()
    {
        float currentTime = 0;
        while (isfailWaiting)
        {
            if (currentTime++ >= failWaitTime || IsInZone)
            {
                isfailWaiting = false;
                beeAudioSource.Stop();
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }

    /// <summary>
    /// Buffer before fail can start
    /// </summary>
    /// <returns>IEnumerator</returns>
    IEnumerator FailStartBuffer()
    {
        isFailStart = false;
        float currentTime = 0;
        while (!isFailStarted)
        {
            if (currentTime++ >= failStartWaitTime)
            {
                isFailStarted = true;
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }

    /// <summary>
    /// Plays sfx from bee audio source
    /// </summary>
    /// <param name="index">Which source to play from array?</param>
    public void PlayBeeSFX(int index)
    {
        beeAudioSource.PlayOneShot(audioClips[index], 1f);
    }
}
