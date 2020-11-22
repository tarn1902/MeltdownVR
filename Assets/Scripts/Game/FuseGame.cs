/*----------------------------------------
File Name: FuseGame.cs
Purpose: Handles fuse mini-game
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/

using System.Collections;
using UnityEngine;

/// <summary>
/// Handles fuse mini-game
/// </summary>
public class FuseGame : MiniGame
{
    BrokenFuseControl[] brokenFuseCon;
    FuseAction[] fuseActions;
    private int index = 0;
    public Transform[] location;
    public bool fusePlaced = false;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    /// <summary>
    /// Overriden method for when scripts starts
    /// </summary>
    protected override void Start()
    {
        base.Start();
        fuseActions = GetComponentsInChildren<FuseAction>();
        brokenFuseCon = GetComponentsInChildren<BrokenFuseControl>();
        foreach (BrokenFuseControl con in brokenFuseCon)
        {
            con.gameObject.SetActive(false);
        }
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Checks if mini-game has failed
    /// </summary>
    protected override void CheckFailedGame()
    {
        //Check if fuse has been placed
        if (!fusePlaced)
            FailMiniGame();
    }

    /// <summary>
    /// Coroutine for game loop
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator GameSequence()
    {
        while (controller.isRunning)
        {
            index = Random.Range(0, fuseActions.Length);
            brokenFuseCon[index].gameObject.SetActive(true);
            PlaySFX(0);
            fuseActions[index].MoveToNewSpawn(location[Random.Range(0, location.Length)]);
            while(!fusePlaced)
            {
                yield return new WaitForSeconds(nextSequenceWaitTime);
                CheckFailedGame();
            }
            fusePlaced = false;
            yield return new WaitForSeconds(nextSequenceWaitTime);
        }
    }

    /// <summary>
    /// Plays sfx from audio source
    /// </summary>
    /// <param name="index">Which source to play from array?</param>
    public void PlaySFX(int index)
    {
        audioSource.PlayOneShot(audioClips[index], 1f);
    }
}
