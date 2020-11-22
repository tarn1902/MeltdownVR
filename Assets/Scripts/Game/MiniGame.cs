/*----------------------------------------
File Name: MiniGame.cs
Purpose: Basic abstract class for minigames
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections;
using UnityEngine;

/// <summary>
/// Basic abstract class for minigames
/// </summary>
public abstract class MiniGame : MonoBehaviour
{
    public GameController controller = null;
    public float nextSequenceWaitTime;
    public float totalTime = 0;

    public AnimationCurve[] difficultyCurve;
    public int difficultyIndex = 0;

    public float maxStartSequenceWaitTime = 0f;

    protected bool isStarted = false;

    /// <summary>
    /// virtual script that triggers when starts
    /// </summary>
    protected virtual void Start()
    {
        controller.onStart += RunStartSequence;
        nextSequenceWaitTime = difficultyCurve[difficultyIndex].Evaluate(totalTime);
    }

    /// <summary>
    /// virtual script that triggers each update
    /// </summary>
    protected virtual void Update()
    {
        //Check if game is running and started
        if (controller.isRunning && isStarted)
        {
            nextSequenceWaitTime = difficultyCurve[difficultyIndex].Evaluate(totalTime);
            totalTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// Fails mini game
    /// </summary>
    protected void FailMiniGame()
    {
        controller.Fail();
    }

    /// <summary>
    /// Starts game start sequence
    /// </summary>
    private void RunStartSequence()
    {
        StartCoroutine("StartSequence");
    }

    /// <summary>
    /// Coroutine that waits
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(Random.Range(0, maxStartSequenceWaitTime));
        isStarted = true;
        StartCoroutine("GameSequence");
    }

    /// <summary>
    /// A game sequence that must be overriden by class
    /// </summary>
    /// <returns>IEnumerator<returns>
    protected abstract IEnumerator GameSequence();

    /// <summary>
    /// A fail check that must be overriden by class
    /// </summary>
    protected abstract void CheckFailedGame();
}
