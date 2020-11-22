/*----------------------------------------
File Name: FuseGame2.cpp
Purpose: 2nd version of fuse game handler
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 2nd version of fuse game handler
/// </summary>
public class FuseGame2 : MiniGame2
{
    BrokenFuseControl[] brokenFuseCon;
    FuseAction[] fuseActions;
    private int index = 0;
    public Transform[] location;
    public bool fusePlaced = true;
    public UnityEvent onEventStart;
    public float eventDuration = 10;
    Timer eventTimer;

    /// <summary>
    /// Method for when scripts starts
    /// </summary>
    protected void Start()
    {
        fuseActions = GetComponentsInChildren<FuseAction>();
        brokenFuseCon = GetComponentsInChildren<BrokenFuseControl>();
        foreach (BrokenFuseControl con in brokenFuseCon)
            con.gameObject.SetActive(false);
        eventTimer = new Timer(true, eventDuration);
        if (autoStart)
        {
            StartGameSequence();
        }
    }

    /// <summary>
    /// function for updates each frame
    /// </summary>
    private void Update()
    {
        //Check if game is running
        if (isRunning)
        {
            //Check if fuse is placed and timer is finished
            if (fusePlaced && eventTimer.AddTime(Time.deltaTime))
            {
                onEventStart.Invoke();
                ResetSequence();
            }
            else
            {
                //Check if fuse is not placed and timer is finished
                if (!fusePlaced && timer.AddTime(Time.deltaTime))
                    onFail.Invoke();
            }
                
        }
    }

    /// <summary>
    /// Resets sequence to default state
    /// </summary>
    public override void ResetSequence()
    {
        fusePlaced = false;
        index = Random.Range(0, fuseActions.Length);
        brokenFuseCon[index].gameObject.SetActive(true);
        fuseActions[index].MoveToNewSpawn(location[Random.Range(0, location.Length)]);
        timer.ResetTimer();
    }
}
