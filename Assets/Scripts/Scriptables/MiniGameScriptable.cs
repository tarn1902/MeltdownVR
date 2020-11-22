/*----------------------------------------
File Name: MiniGameScriptables.cs
Purpose: List of scriptable objects used
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

/// <summary>
/// Storage for mini-game data
/// </summary>
[CreateAssetMenu(fileName = "MiniGame", menuName = "ScriptableObjects/New Mini Game", order = 1)]
public class MiniGameScriptable : ScriptableObject
{
    /// <summary>
    /// Creates three difficulties when created
    /// </summary>
    public MiniGameScriptable()
    {
        for(int i = 0; i < 3; i++)
        {
            miniGameDiffs[i] = new MiniGameDiff((Difficulty)i);
        }
    }
    public GameObject prefab;
    public Layer layer;
    public MiniGameDiff[] miniGameDiffs = new MiniGameDiff[3];
    public int maxTotal = 0;

    public int currentTotal = 0;

    /// <summary>
    /// Triggers when data is cahnged
    /// </summary>
    private void OnValidate()
    {
        //Checks if difficulty count is still 3
        if (miniGameDiffs.Length != 3)
        {
            miniGameDiffs = new MiniGameDiff[3];
            for (int i = 0; i < 3; i++)
            {
                miniGameDiffs[i] = new MiniGameDiff((Difficulty)i);
            }
        }
    }
}

/// <summary>
/// Holds data for mini game difficulties
/// </summary>
[System.Serializable]
public struct MiniGameDiff
{
    [HideInInspector]
    public string name;
    /// <summary>
    /// Sets default data of difficulty
    /// </summary>
    /// <param name="diff">What is the diffulty?</param>
    public MiniGameDiff(Difficulty diff)
    {
        name = diff.ToString();
        totalSpawnChance = 0;
        chanceDecreaseRate = 0;
        currentChance = 0;
    }
    public int totalSpawnChance;
    public int chanceDecreaseRate;
    [HideInInspector]
    public int currentChance;
}




