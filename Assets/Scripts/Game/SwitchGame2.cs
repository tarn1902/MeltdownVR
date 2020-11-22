/*----------------------------------------
File Name: SwitchGame2.cpp
Purpose: 2nd version of switch game handler
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 2nd version of switch game handler
/// </summary>
public class SwitchGame2 : MiniGame2
{
    LightToggle[] toggles = null;
    int selectedToggle = 0;
    int currentToggle = 0;
    public UnityEvent onSuccess;
    public UnityEvent onEventStart;
    public UnityEvent onToggle;
    bool isReseting = false;

    /// <summary>
    /// Method for when scripts starts
    /// </summary>
    protected void Start()
    {
        toggles = GetComponentsInChildren<LightToggle>();
        selectedToggle = currentToggle;
    }

    /// <summary>
    /// Toggles light position
    /// </summary>
    /// <param name="num"></param>
    public void ToggleLight(int num)
    {
        if (num == selectedToggle)
            toggles[selectedToggle].TurnOff();
        else
            toggles[selectedToggle].TurnOn();
        currentToggle = num;
    }

    /// <summary>
    /// function for updates each frame
    /// </summary>
    private void Update()
    {
        //Check if is running and timer is finished
        if (isRunning && timer.AddTime(Time.deltaTime))
        {
            //Check if light is on
            if (toggles[selectedToggle].isOn)
            {
                onFail.Invoke();
                toggles[selectedToggle].BlinkAction();
                isReseting = true;
            }
            else
                onSuccess.Invoke();
        }
        //Check if light is blinking and needs to reset
        else if (!toggles[selectedToggle].isActivated && isReseting)
        {
            ResetSequence();
            onEventStart.Invoke();
        }
    }
    /// <summary>
    /// Resets sequence to default state
    /// </summary>
    public override void ResetSequence()
    {
        //Checks if in position one
        if (selectedToggle == 0)
        {
            toggles[selectedToggle].TurnOff();
            //Checks if position matches
            if (selectedToggle == currentToggle)
                toggles[selectedToggle + 1].TurnOn();
            selectedToggle += 1;
        }
        //Checks if in position two
        else if (selectedToggle == 1)
        {
            toggles[selectedToggle].TurnOff();
            //Checks if position matches
            if (selectedToggle == currentToggle)
                toggles[selectedToggle-1].TurnOn();
            selectedToggle -= 1;
        }
        isReseting = false;
    }
}
