/*----------------------------------------
File Name: FireManager.cs
Purpose: Handles when fire audio plays
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles when fire audio plays
/// </summary>
public class FireManager : MonoBehaviour
{
    public List<ButtonGame> buttons = new List<ButtonGame>();
    public AudioSource fireSource;
    public AudioSource fireSourceOneshot;
    bool fireOn = false;

    /// <summary>
    /// function for updates each frame
    /// </summary>
    private void Update()
    {
        foreach (ButtonGame button in buttons)
        {
            //Test if button is triggering fire
            if (button.IsOnFire())
            {
                //Test if already playing sound
                if (!fireSource.isPlaying)
                    fireSource.Play();
                fireOn = true;
                break;
            }
        }

        //Tests fire is on and playing
        if (!fireOn && fireSource.isPlaying)
            fireSource.Stop();
        
        fireOn = false;

    }

    /// <summary>
    /// Plays sound at same position as fire
    /// </summary>
    /// <param name="clip">What clip is played?</param>
    public void PlayAtPosition(AudioClip clip)
    {
        fireSourceOneshot.PlayOneShot(clip, 1.0f);
    }
}
