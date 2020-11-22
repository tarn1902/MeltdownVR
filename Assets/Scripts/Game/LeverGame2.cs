/*----------------------------------------
File Name: LeverGame2.cpp
Purpose: Version 2 of lever game
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

/// <summary>
/// Version 2 of lever game
/// </summary>
public class LeverGame2 : MiniGame2
{
    // Start is called before the first frame update
    CountdownTimerDisplay display;

    /// <summary>
    /// Method for when scripts starts
    /// </summary>
    protected void Start()
    {
        display = GetComponentInChildren<CountdownTimerDisplay>();
        display.DisplayTime(timer.currentTime);
        //Check if auto starts game
        if (autoStart)
            StartGameSequence();
    }

    /// <summary>
    /// function for updates each frame
    /// </summary>
    private void Update()
    {
        //Check if is running game
        if (isRunning)
        {
            //Waits till time is finished
            if (timer.AddTime(Time.deltaTime))
            {
                onFail.Invoke();
                ResetSequence();
            }
            display.DisplayTime(timer.currentTime);
        }
    }

    /// <summary>
    /// Resets sequence to default state
    /// </summary>
    public override void ResetSequence()
    {
        timer.ResetTimer();
    }
}
