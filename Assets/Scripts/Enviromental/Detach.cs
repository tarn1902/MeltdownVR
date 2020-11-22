/*----------------------------------------
File Name: Detach.cs
Purpose: Makes objects able to seperate
Author: Tarn Cooper
Modified: 16 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Makes objects able to seperate
/// </summary>
public class Detach : MonoBehaviour
{
    public bool isDestructible = false;

    /// <summary>
    /// Updates each frame
    /// </summary>
    void Update()
    {
        //Checks if joint was removed
        if (GetComponent<Joint>() == null)
        {
            Destroy(GetComponent<DoorToggle>());
            Destroy(GetComponent<DrawInteraction>());
            gameObject.AddComponent<Throwable>();
            GetComponent<Rigidbody>().useGravity = true;
            //Checks if should add destructible script
            if(isDestructible)
            {
                gameObject.AddComponent<Destructible>();
            }
            Destroy(this);
        }

    }
}
