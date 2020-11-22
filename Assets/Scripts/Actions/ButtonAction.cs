/*----------------------------------------
File Name: ButtonAction.cs
Purpose: Handles button action.
Author: Tarn Cooper
Modified: 16 November 2020
Based On: SlideFactory's tutorial
Link: https://theslidefactory.com/vr-buttons-in-unity/
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Handles button action.
/// </summary>
public class ButtonAction : MonoBehaviour
{
    public float pressLength;
    public bool pressed;
    public UnityEvent downEvent;
    public UnityEvent upEvent;

    Vector3 startPos;

    /// <summary>
    /// When script starts
    /// </summary>
    void Start()
    {
        //Sets start position to it's current 
        startPos = transform.position;
    }

    /// <summary>
    /// Updates each frame
    /// </summary>
    void Update()
    {
        //When moved a specfic amount away from start
        float distance = Mathf.Abs(transform.position.y - startPos.y);
        if (distance >= pressLength)
        {
            //Only happens once return to orignal position
            if (!pressed)
            {
                pressed = true;
                downEvent.Invoke();
            }
        }
        else
        {
            pressed = false;
            upEvent.Invoke();

        }

        //Stops button from moving upwards
        if (transform.position.y > startPos.y)
        {
            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        }
    }

}
