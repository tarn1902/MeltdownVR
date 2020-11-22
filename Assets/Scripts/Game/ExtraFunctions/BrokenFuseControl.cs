/*----------------------------------------
File Name: BrokenFuseControl.cs
Purpose: Controls overall state of broken fuses
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

/// <summary>
/// Controls overall state of broken fuses
/// </summary>
public class BrokenFuseControl : MonoBehaviour
{
    BrokenFuseAction[] actions;
    public bool isBlocked = true;

    /// <summary>
    /// Function when script is created
    /// </summary>
    private void Awake()
    {
        actions = GetComponentsInChildren<BrokenFuseAction>();

    }

    /// <summary>
    /// Triggered when component is turn on
    /// </summary>
    void OnEnable()
    {
        foreach (BrokenFuseAction action in actions)
        {
            action.transform.localPosition = Vector3.zero;
            action.transform.localRotation = Quaternion.identity;
            action.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// function for updates each frame
    /// </summary>
    void Update()
    {
        //Checks if broken piece is still in slot
        if (actions[0].isActiveAndEnabled || actions[1].isActiveAndEnabled)
        {
            isBlocked = true;
        }
        else
        {
            isBlocked = false;
        }
            
    }
}
