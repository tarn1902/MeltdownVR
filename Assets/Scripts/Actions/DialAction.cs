/*----------------------------------------
File Name: DialAction.cs
Purpose: Handles rotation of dial
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using Valve.VR.InteractionSystem;
using UnityEngine.Events;
/// <summary>
/// Handles rotation of dial
/// </summary>
public class DialAction : CircularDrive
{
    public UnityEvent onFullRotation;

    /// <summary>
    /// Updates each frame
    /// </summary>
    private void Update()
    {
        //Checks if at orignal place
        if (linearMapping.value == 0)
        {
            onFullRotation.Invoke();
        }
    }
}
