/*----------------------------------------
File Name: ClubGame.cpp
Purpose: Handles how the club game functions
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections;
using UnityEngine;

/// <summary>
/// Handles how the club game functions
/// </summary>
public class ClubGame : MiniGame
{
    ClubAction action;
    int activeSection;
    LightToggle[] lights;
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    /// <summary>
    /// Overriden Method for when scripts starts
    /// </summary>
    protected override void Start()
    {
        base.Start();
        action = GetComponentInChildren<ClubAction>();
        lights = GetComponentsInChildren<LightToggle>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Checks if mini-game has failed
    /// </summary>
    protected override void CheckFailedGame()
    {
        //Checks if current section is in correct section
        if ((int)action.curretSection != activeSection)
        {
            FailMiniGame();
        }
    }

    /// <summary>
    /// Coroutine for game loop
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator GameSequence()
    {
        bool change = true; ;
        while (controller.isRunning)
        {
            //Checks if need to change state
            if (change)
            {
                activeSection = Random.Range(0, 3);
                lights[activeSection].TurnOn();
                PlaySFX(1);
            }
            else
            {
                CheckFailedGame();
                lights[activeSection].TurnOff();
            }
            change = !change;
            yield return new WaitForSeconds(nextSequenceWaitTime);

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
