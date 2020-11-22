/*----------------------------------------
File Name: DialGame2.cs
Purpose: Handles version 2 of dial game
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.Events;

public class DialGame2 : MiniGame2
{
    public GameObject dial;
    public GameObject indicator;
    public float diffRange = 10;
    private DialAction action;
    bool isFailing = false;
    public UnityEvent onSafe;

    public float failBufferTime = 5;
    public int degreeEachSecond = 1;

    /// <summary>
    /// Checks if is within safe zone
    /// </summary>
    bool IsInZone { get { return Mathf.Abs(action.outAngle - indicator.transform.localRotation.eulerAngles.y) <= diffRange; } }

    /// <summary>
    /// Method for when scripts starts
    /// </summary>
    protected void Start()
    {
        action = GetComponentInChildren<DialAction>();
    }

    /// <summary>
    /// function for updates each frame
    /// </summary>
    private void Update()
    { 
        //check if is game is running
        if (isRunning)
        {
            //Check if is failing
            if (isFailing)
            {
                //Check if is in zone
                if (IsInZone)
                {
                    ResetSequence();
                    onSafe.Invoke();
                }
                else
                {
                    //Check if waited a amount of time
                    if (timer.AddTime(Time.deltaTime))
                        onFail.Invoke();
                }
            }
            //Check if not in zone
            else if (!IsInZone)
                isFailing = true;

            indicator.transform.Rotate(Vector3.up, degreeEachSecond * Time.deltaTime);
        }

    }

    /// <summary>
    /// Resets sequence to default state
    /// </summary>
    public override void ResetSequence()
    {
        isFailing = false;
        timer.ResetTimer();
    }
}
