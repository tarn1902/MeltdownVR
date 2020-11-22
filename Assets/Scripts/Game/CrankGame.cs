/*----------------------------------------
File Name: CrankGame.cpp
Purpose: New version of WheelGame
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

public class CrankGame : MiniGame2
{

    public int turnAmount = 10;

    WheelAction action = null;
    LightToggle toggle = null;

    bool isActive = false;

    /// <summary>
    /// Method for when scripts starts
    /// </summary>
    protected void Start()
    {
        action = GetComponentInChildren<WheelAction>();
        toggle = GetComponentInChildren<LightToggle>();
        //Tests if game needs to start automatically
        if (autoStart)
            StartGameSequence();
    }

    /// <summary>
    /// function for updates each frame
    /// </summary>
    private void Update()
    {
        //Tests if running
        if (isRunning)
        {
            //Waits for timer to finish
            if (timer.AddTime(Time.deltaTime))
            {
                isActive = !isActive;
                //Tests if mini-game is running
                if (isActive)
                {
                    ResetSequence();
                }
                else
                {
                    //Tests if turned enough
                    if (action.turnCount < turnAmount)
                    {
                        toggle.ForceStopBlinking();
                        onFail.Invoke();
                    }
                    
                }
            }
            //Tests if turned enough
            if (action.turnCount >= turnAmount)
            {
                toggle.ForceStopBlinking();
            }
        }
    }

    /// <summary>
    /// Resets sequence to default state
    /// </summary>
    public override void ResetSequence()
    {
        action.totalDif = 0;
        action.turnCount = 0;
        toggle.BlinkAction();
    }
}
