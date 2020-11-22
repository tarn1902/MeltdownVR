/*----------------------------------------
File Name: FailLightDisplay.cs
Purpose: displays the amount of fails
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections;
using UnityEngine;

/// <summary>
/// displays the amount of fails
/// </summary>
public class FailLightDisplay : MonoBehaviour
{
    public float gameOverBlinkSpeed = 1;
    public LightToggle[] FailLights;
    public GameController controller;
    /// <summary>
    /// New method when scripts starts
    /// </summary>
    void Start()
    {
        FailLights = GetComponentsInChildren<LightToggle>();
    }

    /// <summary>
    /// Changes amount of fail lights that are on
    /// </summary>
    public void DisplayFailCount()
    {
        //Tests if is player lost
        if (controller.IsGameOver())
        {
            StartCoroutine("Blink");
            GetComponent<AudioSource>().Play();
        }
        else
        {
            FailLights[controller.currentFails].TurnOn();
            FailLights[controller.currentFails].GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// Blink event when player lost game
    /// </summary>
    /// <returns>Coroutine</returns>
    IEnumerator Blink()
    {
        //Todo: Add lose sequence
        while (true)
        {
            //Turns off all lights
            foreach (LightToggle light in FailLights)
            {
                light.TurnOff();
            }
            yield return new WaitForSeconds(gameOverBlinkSpeed);
            //Turns on all lights
            foreach (LightToggle light in FailLights)
            {
                light.TurnOn();
            }
            yield return new WaitForSeconds(gameOverBlinkSpeed);
        }
    }

}
