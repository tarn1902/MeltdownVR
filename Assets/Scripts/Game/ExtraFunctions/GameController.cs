/*----------------------------------------
File Name: GameController.cs
Purpose: Controls overall flow of game
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections;
using UnityEngine;
using Valve.VR;
using UnityEngine.Playables;

/// <summary>
/// Controls overall flow of game
/// </summary>
public class GameController : MonoBehaviour
{
    [Range (0.0f, 10.0f)]
    public float globalDifficulty = 0;
    public float globalDifficultyRateOfChange = 0;
    public float gameEndMaxTime = 60;
    public float gameStartMaxTime = 5;
    public int maxFails = 3;
    public float failCooldown = 1;
    public CountdownTimerDisplay gameTimeDisplay = null;
    public FailLightDisplay failLightDisplay = null;

    public delegate void StartAll();
    public event StartAll onStart;
    
    public float countdownSpeed = 1;
    bool isFailWaiting = false;

    public float CurrentTime { get; private set; }
    public int currentFails = 0;

    public bool isRunning = false;
    public bool isDifficultyIncreasing = false;
    public float difficultyIncreaseSpeed = 0.1f;

    public SteamVR_Action_Boolean menuAction;
    public GameObject pauseMenu;
    SteamVR_LoadLevel loader = null;

    private PlayableDirector playableDirector;

    public PlayableAsset loseTimeline;
    public PlayableAsset winTimeline;
    private bool gameOver = false;

    /// <summary>
    /// Method for when scripts starts
    /// </summary>
    void Start()
    {
        loader = GetComponent<SteamVR_LoadLevel>();
        playableDirector = GetComponent<PlayableDirector>();
        gameOver = false;
        StartCoroutine("StartCountDown");
    }

    /// <summary>
    /// function for updates each frame
    /// </summary>
    private void Update()
    {
        //test if game is running
        if (isRunning)
        {
            //Test if difficulty increases
            if (isDifficultyIncreasing)
            {
                //Test if past max difficulty of 10
                if (globalDifficulty < 10)
                {
                    globalDifficulty += Time.deltaTime * difficultyIncreaseSpeed;
                }
                else
                {
                    globalDifficulty = 10;
                    isDifficultyIncreasing = false;
                }
            }
            
        }
        gameTimeDisplay.DisplayClock(CurrentTime);
        //Tests if inputs are pressed. Causes error if not attached to Steam VR
        if (menuAction.GetState(SteamVR_Input_Sources.LeftHand) || menuAction.GetState(SteamVR_Input_Sources.RightHand))
        {
            GetMenu();
        }
    }

    /// <summary>
    /// Trigger for when minig games fail the game
    /// </summary>
    public void Fail()
    {
        //Tests if have certain amount of fails
        if (currentFails >= maxFails)
        {
            GameOverLose();
        }
        //Tests if waiting to fail again (used to prevent multiple fails at once
        if (!isFailWaiting)
        {
            isFailWaiting = true;
            StartCoroutine("FailWait");
            failLightDisplay.DisplayFailCount();
            currentFails++;
        }
        
            
    }

    /// <summary>
    /// Starts the game
    /// </summary>
    void GameStart()
    {
        isRunning = true;
        StartCoroutine("EndCountDown");
        if (onStart != null)
        {
            onStart.Invoke();
        }
        
    }
    /// <summary>
    /// Triggers if game has ended in loss
    /// </summary>
    private void GameOverLose() 
    {
        if (!gameOver)
        {
            isRunning = false;
            gameOver = true;
            playableDirector.playableAsset = loseTimeline;
            playableDirector.Play();
        }
    }

    /// <summary>
    /// Triggers if game has ended in win
    /// </summary>
    private void GameOverWin()
    {
        if (!gameOver)
        {
            isRunning = false;
            gameOver = true;
            playableDirector.playableAsset = winTimeline;
            playableDirector.Play();
        }
    }

    /// <summary>
    /// Loads end game scene
    /// </summary>
    /// <param name="result">What was the resulting end?</param>
    public void LoadPostScene(int result)
    {
        PlayerPrefs.SetInt("LastResult", result);
        loader.Trigger();
    }

    /// <summary>
    /// Countsdown to start game
    /// </summary>
    /// <returns></returns>
    IEnumerator StartCountDown()
    {
        CurrentTime = gameStartMaxTime;
        while (true)
        {
            yield return new WaitForSeconds(countdownSpeed);
            CurrentTime -= countdownSpeed;
            //Tests if time for countdown is over
            if (CurrentTime < 0) break;
        }
        GameStart();
    }

    /// <summary>
    /// Used delay multiple fails
    /// </summary>
    /// <returns>Coroutine</returns>
    IEnumerator FailWait()
    {
        int currentTime = 0;
        while (currentTime < failCooldown)
        {
            yield return new WaitForSeconds(1);
            currentTime++;
        }
        isFailWaiting = false;
    }

    /// <summary>
    /// Countsdown to end game
    /// </summary>
    /// <returns></returns>
    IEnumerator EndCountDown()
    {
        CurrentTime = gameEndMaxTime;
        while (isRunning)
        {
            yield return new WaitForSeconds(countdownSpeed);
            CurrentTime -= countdownSpeed;
            //Tests if time for countdown is over
            if (CurrentTime <= 0)
            {
                GameOverWin();
                break;
            }
        }
    }

    /// <summary>
    /// Tests if game over
    /// </summary>
    /// <returns>Return the state of game</returns>
    public bool IsGameOver()
    {
        return currentFails >= maxFails;
    }

    /// <summary>
    /// fuction to set menu to active
    /// </summary>
    public void GetMenu()
    {
        pauseMenu.SetActive(true);
    }
}
