/*----------------------------------------
File Name: SwitchGame.cpp
Purpose: Handles switch mini game
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections;
using UnityEngine;

/// <summary>
/// Handles switch mini game
/// </summary>
public class SwitchGame : MiniGame
{
    LightToggle[] toggles = null;
    int selectedToggle = 0;
    int currentToggle = 0;
    AudioSource audioSource;
    public AudioSource electricityAudioSource;
    public AudioClip[] audioClips;
    bool isFailed = false;
    public float blinkTime;
    public float blinkTotalTime;

    /// <summary>
    /// Overriden method for when scripts starts
    /// </summary>
    protected override void Start()
    {
        base.Start();
        toggles = GetComponentsInChildren<LightToggle>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Coroutine for game loop
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator GameSequence()
    {
        selectedToggle = currentToggle;
        while (controller.isRunning)
        {
            CheckFailedGame();
            //Check if failed
            if (isFailed)
            {
                toggles[selectedToggle].BlinkAction();
                yield return new WaitForSeconds(toggles[selectedToggle].blinkTime);
            }
            //Check if in first toggle position
            if (selectedToggle == 0)
            {
                toggles[0].TurnOff();
                //Check if same toggle position
                if (selectedToggle == currentToggle)
                    toggles[1].TurnOn();
                selectedToggle = 1;
            }
            //Check if in second toggle position
            else if (selectedToggle == 1)
            {
                toggles[1].TurnOff();
                //Check if same toggle position
                if (selectedToggle == currentToggle)
                    toggles[0].TurnOn();
                selectedToggle = 0;
            }
            PlaySFX(1);
            yield return new WaitForSeconds(nextSequenceWaitTime);
            
        }
        foreach (LightToggle toggle in toggles)
            toggle.TurnOff();
    }

    /// <summary>
    /// Checks if mini-game has failed
    /// </summary>
    protected override void CheckFailedGame()
    {
        //If light is on
        if (toggles[selectedToggle].isOn)
        {
            FailMiniGame();
            isFailed = true;
            //Audio here
        }
        else
        {
            //Audio here
        }
    }

    /// <summary>
    /// Toggles light position
    /// </summary>
    /// <param name="num">What light?</param>
    public void ToggleLight(int num)
    {
        if (isStarted)
        {
            if (num == selectedToggle)
                toggles[selectedToggle].TurnOff();
            else
                toggles[selectedToggle].TurnOn();
        }
        currentToggle = num;
        PlaySFX(0);

    }

    /// <summary>
    /// Plays sfx from audio source
    /// </summary>
    /// <param name="index">Which source to play from array?</param>
    public void PlaySFX(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }

    /// <summary>
    /// Plays sfx from electricity audio source
    /// </summary>
    /// <param name="index">Which source to play from array?</param>
    public void PlayElectricitySFX(int index)
    {
        electricityAudioSource.PlayOneShot(audioClips[index]);
    }
}
