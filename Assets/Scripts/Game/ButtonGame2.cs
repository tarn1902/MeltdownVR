/*----------------------------------------
File Name: ButtonGame2.cs
Purpose: Second version of button mini-game handler
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Second version of button mini-game handler
/// </summary>
public class ButtonGame2 : MiniGame2
{
    public List<Color> possibleColors;
    public GameObject indentifer = null;

    public UnityEvent onFailPressEvent;
    public UnityEvent onCorrectPressEvent;
    public UnityEvent onWrongPressEvent;

    public int startingColour;

    Color selectedLightColor;
    Color currentLightColor;
    bool buttonPressed = false;
    LightToggle toggle = null;
    private Color lastLightColour;
    public int selectedNum = 0;
    public bool isRandom = false;
    public bool previousRandomState = false;
    public bool IsOnFire { get { return (currentLightColor == selectedLightColor && !buttonPressed && isRunning); } }

    /// <summary>
    /// Method for when scripts starts
    /// </summary>
    protected void Start()
    {
        if (isRandom)
        {
            possibleColors.Remove(selectedLightColor);
        }

        selectedLightColor = possibleColors[startingColour];
        lastLightColour = selectedLightColor;
        indentifer.GetComponent<MeshRenderer>().material = new Material(indentifer.GetComponent<MeshRenderer>().material.shader);
        indentifer.GetComponent<MeshRenderer>().material.color = selectedLightColor;
        toggle = GetComponentInChildren<LightToggle>();
        
        //Tests if game needs to auto start
        if (autoStart)
            StartGameSequence();
    }

    /// <summary>
    /// Called when button is pressed
    /// </summary>
    public void ButtonPressed()
    {
        //Checks if button is not pressed and game is running
        if (!buttonPressed && isRunning)
        {
            buttonPressed = !buttonPressed;
            toggle.BlinkAction();
            //Checks if displayed colour is diffrent from indicated
            if (currentLightColor != selectedLightColor)
                onWrongPressEvent.Invoke();
            else
                onCorrectPressEvent.Invoke();
        }

    }
    /// <summary>
    /// function for updates each frame
    /// </summary>
    private void Update()
    {
        //Tests if running
        if (isRunning)
        {
            //waits till time is finised
            if (timer.AddTime(Time.deltaTime))
            {
                //Tests if button is not pressed
                if (!buttonPressed)
                {
                    //Tests if light was on indicated colour
                    if (currentLightColor == selectedLightColor)
                        onFail.Invoke();
                }
                ResetSequence();
                ChangeColor();
            }
        }
    }

    /// <summary>
    /// Changes colour of light
    /// </summary>
    public void ChangeColor()
    {
        //Checks if random state changed
        if (previousRandomState != isRandom)
        {
            //Checks if now set to random
            if(isRandom)
            {
                possibleColors.Remove(selectedLightColor);
            }
            else
            {
                possibleColors.Add(lastLightColour);
            }
        }
        //Checks if random
        if (isRandom)
        {
            currentLightColor = possibleColors[Random.Range(0, possibleColors.Count)];
            possibleColors.Add(lastLightColour);
            toggle.ChangeColor(currentLightColor);
            possibleColors.Remove(currentLightColor);
            lastLightColour = currentLightColor;
        }
        else
        {
            //Checks if index is at end of array
            if (selectedNum >= possibleColors.Count - 1)
            {
                selectedNum = -1;
            }

            currentLightColor = possibleColors[++selectedNum];
            toggle.ChangeColor(currentLightColor);

        }
        previousRandomState = isRandom;
    }




    /// <summary>
    /// //Resets game to default values of sequence
    /// </summary>
    public override void ResetSequence()
    {
        buttonPressed = false;
        toggle.ForceStopBlinking();
        toggle.TurnOff();
        //ChangeColor();
    }

    /// <summary>
    /// Triggers random input
    /// </summary>
    public void Randomise()
    {
        isRandom = true;
    }
}
