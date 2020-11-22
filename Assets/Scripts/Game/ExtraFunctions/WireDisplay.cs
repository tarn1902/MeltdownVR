/*----------------------------------------
File Name: WireDisplay.cpp
Purpose: Displays wire as line renderer
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

/// <summary>
/// Displays wire as line renderer
/// </summary>
public class WireDisplay : MonoBehaviour
{
    public LineRenderer line;
    public GameObject startPoint;
    public GameObject endPoint;
    /// <summary>
    /// Method for when scripts starts
    /// </summary>
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// function for updates each frame
    /// </summary>
    void Update()
    {
        line.SetPosition(0, startPoint.transform.position);
        line.SetPosition(1, endPoint.transform.position);
    }
}
