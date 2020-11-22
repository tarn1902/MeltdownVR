/*----------------------------------------
File Name: LightToggle.cs
Purpose: Toggles light state
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections;
using UnityEngine;

/// <summary>
/// Toggles light state
/// </summary>
public class LightToggle : MonoBehaviour
{
    public Color onColor;
    public float onIntensity = 4;
    public Color defaultColor;
    public Light comLight = null;
    public MeshRenderer comRenderer = null;
    public bool isOn = false;
    Material copiedMat = null;
    public float blinkTime = 0;
    public float blinkSpeed = 0;
    public bool isActivated = false;

    /// <summary>
    /// Function when script is created
    /// </summary>
    public void Awake()
    {
        copiedMat = new Material(comRenderer.material.shader);
        copiedMat.CopyPropertiesFromMaterial(comRenderer.material);
        comRenderer.material = copiedMat;
        //Tests if should start on
        if (!isOn)
            TurnOff();
        else
            TurnOn();
    }

    /// <summary>
    /// Turns on light
    /// </summary>
    public void TurnOn()
    {
        copiedMat.SetColor("_EmissionColor", onColor);
        comLight.intensity = onIntensity;
        comLight.color = onColor;
        isOn = true;
    }

    /// <summary>
    /// Turns off light
    /// </summary>
    public void TurnOff()
    {
        copiedMat.SetColor("_EmissionColor", defaultColor);
        comLight.intensity = 0;
        comLight.color = defaultColor;
        isOn = false;
    }


    /// <summary>
    /// Turns on light and changes color
    /// </summary>
    /// <param name="newColor">What is the new color?</param>
    public void ChangeColor(Color newColor)
    {
        copiedMat.SetColor("_EmissionColor", newColor);
        onColor = newColor;
        comLight.color = onColor;
        TurnOn();
    }

    /// <summary>
    /// Blinks the light
    /// </summary>
    public void BlinkAction()
    {
        if (!isActivated)
        {
            isActivated = true;
            StartCoroutine("Blink");
        }
    }

    /// <summary>
    /// Blink event when player lost game
    /// </summary>
    /// <returns>Coroutine</returns>
    IEnumerator Blink()
    {
        float currentTime = 0;
        while (currentTime <= blinkTime)
        {
            TurnOff();
            yield return new WaitForSeconds(blinkSpeed);
            TurnOn();
            yield return new WaitForSeconds(blinkSpeed);
            currentTime += blinkSpeed * 2;
        }
        TurnOff();
        isActivated = false;
    }

    /// <summary>
    /// Forces blinking to stop
    /// </summary>
    public void ForceStopBlinking()
    {
        isActivated = false;
        StopCoroutine("Blink");
        TurnOff();
    }
}
