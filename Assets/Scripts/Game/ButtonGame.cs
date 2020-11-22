/*----------------------------------------
File Name: ButtonGame.cs
Purpose: Handles the button mini-game
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the button mini-game
/// </summary>
public class ButtonGame : MiniGame
{
    public List<Color> possibleColors;
    Color selectedLightColor;
    Color currentLightColor;
    public int lightCount = 0;
    public GameObject indentifer = null;
    bool buttonPressed = false;
    public float blinkLightSpeed = 0.1f;
    LightToggle toggle = null;
    public int blinkTime = 3;

    public AudioClip[] audioClips;
    public FireManager fireSource;
    AudioSource audioSource;
    bool isBlinking = false;
    private Color lastLightColour;

    /// <summary>
    /// Overrriden method for when scripts starts
    /// </summary>
    protected override void Start()
    {
        base.Start();
        selectedLightColor = possibleColors[Random.Range(0, possibleColors.Count)];
        possibleColors.Remove(selectedLightColor);
        lastLightColour = selectedLightColor;
        indentifer.GetComponent<MeshRenderer>().material = new Material(indentifer.GetComponent<MeshRenderer>().material.shader);
        indentifer.GetComponent<MeshRenderer>().material.color = selectedLightColor;
        lightCount = possibleColors.Count;
        toggle = GetComponentInChildren<LightToggle>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Triggered when button is pressed
    /// </summary>
    public void ButtonPressed()
    {
        PlaySFX(0);
        //Test if game started yet
        if (isStarted)
        {
            //Tests if on fire
            if (IsOnFire())
            {
                PlayFireSFX(2);
            }
            //Tests if button is not pressed yet
            if (!buttonPressed)
            {
                buttonPressed = true;
                StartCoroutine("Blink");
                CheckFailedGame();
            }
        }
    }

    /// <summary>
    /// Checks if game has been failed
    /// </summary>
    protected override void CheckFailedGame()
    {
        //Tests if is on fire
        if (IsOnFire())
        {
            FailMiniGame();
            PlayFireSFX(3);
        }
        //Tests if button is pressed on wrong colour
        else if (buttonPressed && currentLightColor != selectedLightColor)
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
        
        while (controller.isRunning)
        {
            isBlinking = false;
            currentLightColor = possibleColors[Random.Range(0, possibleColors.Count)];
            possibleColors.Add(lastLightColour);
            toggle.ChangeColor(currentLightColor);
            lastLightColour = currentLightColor;
            possibleColors.Remove(lastLightColour);
            yield return new WaitForSeconds(nextSequenceWaitTime);

        }
        toggle.TurnOff();
    }

    /// <summary>
    /// Tests if mini-game is triggering fire
    /// </summary>
    /// <returns>Returns if fire is active</returns>
    public bool IsOnFire()
    {
        return (currentLightColor == selectedLightColor && !buttonPressed && controller.isRunning);
    }

    //
    /// <summary>
    /// Blink event when player presses button
    /// </summary>
    /// <returns>IEnumerator</returns>
    IEnumerator Blink()
    {
        float currentTime = 0;
        isBlinking = true;
        while (controller.isRunning && currentTime <= blinkTime & isBlinking)
        {
            toggle.TurnOn();
            yield return new WaitForSeconds(blinkLightSpeed);
            toggle.TurnOff();
            yield return new WaitForSeconds(blinkLightSpeed);
            currentTime += blinkLightSpeed * 2;
        }
        buttonPressed = false;
        isBlinking = false;
    }
    
    /// <summary>
    /// Plays sfx from audio source
    /// </summary>
    /// <param name="index">Which source to play from array?</param>
    public void PlaySFX(int index)
    {
        audioSource.PlayOneShot(audioClips[index], 1f);
    }

    /// <summary>
    /// Plays sfx at fire position
    /// </summary>
    /// <param name="index">Which source to play from array?</param>
    public void PlayFireSFX(int index)
    {
        fireSource.PlayAtPosition(audioClips[index]);
    }
}
