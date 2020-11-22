/*----------------------------------------
File Name: FireSFX.cs
Purpose: Handles when fire audio plays
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles when fire audio plays
/// </summary>
public class FireSFX : MonoBehaviour
{
    public List<ButtonGame2> buttons = new List<ButtonGame2>();
    public UnityEvent startFireEvent;
    public UnityEvent stopFireEvent;
    public AudioSource fireSourceOneshot;
    bool fireStarted = false;
    bool fireStopped = true;

    /// <summary>
    /// function for updates each frame
    /// </summary>
    private void Update()
    {
        fireStopped = true;
        foreach (ButtonGame2 button in buttons)
        {
            //Test if button is triggering fire
            if (button.IsOnFire && !fireStarted)
            {
                startFireEvent.Invoke();
                //if (!fireSource.isPlaying)
                //fireSource.Play();
                fireStarted = true;
                fireStopped = false;
                break;
            }
            //Test if already playing sound
            else if (button.IsOnFire)
            {
                fireStopped = false;
                break;
            }
        }
        //Tests fire is on and playing
        if (fireStopped)
        {
            fireStarted = false;
            stopFireEvent.Invoke();
        }
            
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
