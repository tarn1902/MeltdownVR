/*----------------------------------------
File Name: WireCutGame.cs
Purpose: Handles game for cutting the wire
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections;
using UnityEngine;

/// <summary>
/// Handles game for cutting the wire
/// </summary>
public class WireCutGame : MiniGame
{
    private CutAction[] wires;
    private int selectedWire;
    public MeshRenderer indicator;
    public DoorToggle door;
    public bool wasCut = true;

    private AudioSource audioSource;
    public AudioClip[] audioClips;
    public AudioSource robot;

    bool change = true;
    /// <summary>
    /// Overriden method for when scripts starts
    /// </summary>
    protected override void Start()
    {
        base.Start();
        wires = GetComponentsInChildren<CutAction>();
        foreach (CutAction wire in wires)
        {
            wire.ResetWire();
        }
        door = GetComponentInChildren<DoorToggle>();
        indicator.GetComponent<MeshRenderer>().material = new Material(indicator.GetComponent<MeshRenderer>().material.shader);
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Coroutine for game loop
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator GameSequence()
    {
        while (controller.isRunning)
        {
            //Check current state of sequence
            if (change)
                ResetGame();
            //Check if wire was not cut
            else if (!wasCut)
                CheckFailedGame();
            wasCut = false;
            change = !change;
            yield return new WaitForSeconds(nextSequenceWaitTime);
            
        }
    }

    /// <summary>
    /// Checks if mini-game has failed
    /// </summary>
    protected override void CheckFailedGame()
    {
        FailMiniGame();
        PlayRobotSFX(3);
        door.CloseDoor();
    }

    /// <summary>
    /// Resets game to default state
    /// </summary>
    void ResetGame()
    {
        foreach (CutAction wire in wires)
            wire.ResetWire();
        selectedWire = Random.Range(0, wires.Length);
        indicator.GetComponent<MeshRenderer>().material.color = wires[selectedWire].wireColor;
        door.OpenDoor();
        wasCut = false;
        PlaySFX(0);
        PlayRobotSFX(2);

    }

    /// <summary>
    /// Triggered when wire is cut
    /// </summary>
    /// <param name="wire">Which wire was cut?</param>
    public void WireIsCut(CutAction wire)
    {
        //Check if wire cut is not the selected wire
        if (wire != wires[selectedWire])
        {
            CheckFailedGame();
        }
        else
        {
            PlayRobotSFX(4);
            door.CloseDoor();
        }
        wasCut = true;
    }

    /// <summary>
    /// Plays sfx from audio source
    /// </summary>
    /// <param name="index">Which source to play from array?</param>
    public void PlaySFX(int index)
    {
        audioSource.PlayOneShot(audioClips[index], 1f);
    }

    /// <summary>
    /// Plays sfx from robots audio source
    /// </summary>
    /// <param name="index">Which source to play from array?</param>
    public void PlayRobotSFX(int index)
    {
        robot.PlayOneShot(audioClips[index], 1f);
    }
}
