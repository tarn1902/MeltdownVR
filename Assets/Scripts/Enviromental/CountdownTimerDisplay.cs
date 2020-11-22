/*----------------------------------------
File Name: CountdownTimerDisplay.cs
Purpose: Displays time
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using TMPro;

/// <summary>
/// Displays time
/// </summary>
public class CountdownTimerDisplay : MonoBehaviour
{
    TMP_Text textUI;
    public bool isGameDisplayer = true;

    /// <summary>
    /// Function when script is created
    /// </summary>
    private void Awake()
    {
        textUI = GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Display time as digital to UI
    /// </summary>
    /// <param name="totalSeconds">How many seconds?</param>
    public void DisplayClock(float totalSeconds)
    {
        textUI.text = ConvertToClock(totalSeconds);
    }

    /// <summary>
    /// Display time as seconds to UI
    /// </summary>
    /// <param name="totalSeconds">How many seconds?</param>
    public void DisplayTime(float totalSeconds)
    {
        float seconds = Mathf.RoundToInt(totalSeconds);
        textUI.text = seconds.ToString("0");
    }

    /// <summary>
    /// Convert second to digital time
    /// </summary>
    /// <param name="currentTime">How many seconds?</param>
    string ConvertToClock(float totalSeconds)
    {
        float minutes = Mathf.Floor(totalSeconds / 60);
        float seconds = Mathf.RoundToInt(totalSeconds % 60);
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
