/*----------------------------------------
File Name: MiniGame2.cs
Purpose: 2nd version of abstract minigame
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 2nd version of abstract minigame
/// </summary>
public abstract class MiniGame2 : MonoBehaviour
{
    public UnityEvent onFail;
    public float waitTime = 10;
    public bool autoStart = false;
    public bool isRunning = true;
    /// <summary>
    /// Basic struct for handling time
    /// </summary>
    public struct Timer
    {
        /// <summary>
        /// Timer constructer
        /// </summary>
        /// <param name="isActive">Is it on?</param>
        /// <param name="waitTime">How long is it waiting?</param>
        public Timer(bool isActive, float waitTime)
        {
            currentTime = waitTime;
            IsActive = isActive;
            this.waitTime = waitTime;
        }

        /// <summary>
        /// Adds time and checks if is finsihed
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        public bool AddTime(float deltaTime)
        {
            //Checks if is on
            if (IsActive)
            {
                currentTime -= deltaTime;
                //Checks if timer is finished
                if (currentTime <= 0)
                {
                    currentTime += waitTime;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Turns timer on
        /// </summary>
        public void StartTimer()
        {
            IsActive = true;
        }

        /// <summary>
        /// Turns timer off
        /// </summary>
        public void StopTimer()
        {
            IsActive = false;
            currentTime = waitTime;
        }

        /// <summary>
        /// Temporary pause of timer
        /// </summary>
        public void PauseTimer()
        {
            IsActive = false;
        }

        /// <summary>
        /// Resets timer to original wait time
        /// </summary>
        public void ResetTimer()
        {
            currentTime = waitTime;
        }

        public float currentTime;
        float waitTime;
        public bool IsActive { get; private set; }
    }

    protected Timer timer;

    /// <summary>
    /// Abstract command that resets the sequence
    /// </summary>
    abstract public void ResetSequence();

    /// <summary>
    /// Function when script is created
    /// </summary>
    private void Awake()
    {
        timer = new Timer(false, waitTime);
    }

    /// <summary>
    /// Starts Game sequence using set wait time
    /// </summary>
    public void StartGameSequence()
    {
        isRunning = true;
        timer.StartTimer();
        ResetSequence();
    }

    /// <summary>
    /// Finishes game sequence
    /// </summary>
    public void StopGameSequence()
    {
        isRunning = false;
        timer.StopTimer();
        ResetSequence();
    }

}
