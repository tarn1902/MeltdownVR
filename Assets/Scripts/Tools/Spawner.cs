/*----------------------------------------
File Name: Spawner.cs
Purpose: Spawns in mini games
Author: Tarn Cooper
Modified: 17 November 2020
------------------------------------------
Copyright 2020 Tarn Cooper.
-----------------------------------*/
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns in mini games
/// </summary>
public class Spawner : MonoBehaviour
{
    public MiniGameScriptable[] miniGames;
    public LayerSettings[] layers = new LayerSettings[3];
    public Transform[] fuseLocations;
    public FireManager fire;
    const int LAYERSIZE = 3;
    public List<GameObject> games;

    /// <summary>
    /// Auto sets up for layer
    /// </summary>
    [System.Serializable]
    public struct LayerSettings
    {
        [HideInInspector]
        public string name;
        public Transform[] spawnLocations;
        public LayerDiff[] diffs;

        /// <summary>
        ///  sets up layer settings
        /// </summary>
        /// <param name="diff">What is the layer name</param>
        public LayerSettings(Layer diff)
        {
            name = diff.ToString();
            diffs = new LayerDiff[LAYERSIZE];
            for (int i = 0; i < diffs.Length; i++)
            {
                diffs[i] = new LayerDiff((Difficulty)i);
            }
            spawnLocations = null;
        }
    }

    /// <summary>
    /// Trigger when data is changed in editor
    /// </summary>
    private void OnValidate()
    {
        //Checks if layer size is correct
        if (layers.Length != LAYERSIZE)
        {
            layers = new LayerSettings[3];
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new LayerSettings((Layer)i); 
            }
        }

    }

    /// <summary>
    /// Holds difficulty in layer
    /// </summary>
    [System.Serializable]
    public struct LayerDiff
    {
        [HideInInspector]
        public string name;
        /// <summary>
        /// Costructs difficulty of layer
        /// </summary>
        /// <param name="diff"></param>
        public LayerDiff(Difficulty diff)
        {
            name = diff.ToString();
            spawnRange = Vector2.zero;
        }
        public Vector2 spawnRange;
    }

    int difficulty;

    /// <summary>
    /// Method for when scripts starts
    /// </summary>
    void Start()
    {
        difficulty = PlayerPrefs.GetInt("Difficulty");
        CreateGame();
    }

    /// <summary>
    /// function for updates each frame
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CreateGame();
        }
    }

    /// <summary>
    /// Creates all mini games in game
    /// </summary>
    private void CreateGame()
    {
        //checks if games already exist in list
        if (games.Count != 0)
        {
            foreach (GameObject game in games)
            {
                Destroy(game);
            }
            games.Clear();
        }
        for(int i = 0; i < layers.Length; i++)
        {
            CreateMinigames(i, layers[i].diffs[difficulty].spawnRange);
        }
    }

    /// <summary>
    /// Creates mini game
    /// </summary>
    /// <param name="spawnLayerIndex">What layer is it on?</param>
    /// <param name="spawnRange">How many could possibly spawn?</param>
    private void CreateMinigames(int spawnLayerIndex, Vector2 spawnRange)
    {
        int spawnAmount = (int)Random.Range(spawnRange.x, spawnRange.y + 1);

        List<Transform> potentialLocations = new List<Transform>();
        foreach (Transform spawn in layers[spawnLayerIndex].spawnLocations)
        {
            potentialLocations.Add(spawn);
        }
        for (int i = 0; i < miniGames.Length; i++)
        {
            miniGames[i].currentTotal = miniGames[i].maxTotal;
            miniGames[i].miniGameDiffs[difficulty].currentChance = miniGames[i].miniGameDiffs[difficulty].totalSpawnChance;
        }
        int spawnedNum = 0;
        while (spawnedNum++ != spawnAmount)
        {
            //Checks if any locations are left
            if (potentialLocations.Count == 0)
                break;

            int randomLocation = Random.Range(0, potentialLocations.Count);
            MiniGameScriptable randomMiniGame = MiniGameSelector(spawnLayerIndex);
            //Checks if mini game was selected
            if (randomMiniGame == null)
            {
                Debug.Log("Skip Layer");
                break;
            }
            GameObject pref = Instantiate(randomMiniGame.prefab, potentialLocations[randomLocation].position, potentialLocations[randomLocation].rotation);
            pref.GetComponent<MiniGame>().difficultyIndex = difficulty;
            IsFuseGame(pref.GetComponent<FuseGame>());
            IsButtonGame(pref.GetComponent<ButtonGame>());
            pref.GetComponent<MiniGame>().controller = GetComponent<GameController>();
            potentialLocations.RemoveAt(randomLocation);
            //Checks if game can be spawned again
            if (randomMiniGame.currentTotal == 0)
                randomMiniGame.miniGameDiffs[difficulty].currentChance = 0;
            games.Add(pref);
        }
        potentialLocations.Clear();
    }

    MiniGameScriptable MiniGameSelector(int layer)
    {
        int spawnChanceTotal = 0;
        foreach (MiniGameScriptable values in miniGames)
            //Checks if game can be spawned on layer
            if (values.layer == (Layer)layer)
                spawnChanceTotal += values.miniGameDiffs[difficulty].currentChance;
        int selected = Random.Range(0, spawnChanceTotal);
        int currentChance = 0;
        for (int i = 0; i < miniGames.Length; i++)
        {
            //Checks if game can be spawned on layer (May be removed)
            if (miniGames[i].layer == (Layer)layer)
            {
                //Checks if random number is in range of mini-game chance
                if (selected >= currentChance && selected < miniGames[i].miniGameDiffs[difficulty].currentChance + currentChance)
                {
                    miniGames[i].miniGameDiffs[difficulty].currentChance -= miniGames[i].miniGameDiffs[difficulty].chanceDecreaseRate;
                    miniGames[i].currentTotal--;
                    return miniGames[i];
                }
                //Adds it's chance for next mini-game
                else
                    currentChance = miniGames[i].miniGameDiffs[difficulty].currentChance + currentChance;
            }
        }
        
        return null;
    }

    /// <summary>
    /// Checks if is fuse game (need to find better way)
    /// </summary>
    /// <param name="game">What is the game that needs to be tested?</param>
    void IsFuseGame(FuseGame game)
    {
        //Check if is a fuse game
        if (game != null)
        {
            game.location = fuseLocations;
        }
    }

    /// <summary>
    /// Checks if is button game (need to find better way)
    /// </summary>
    /// <param name="game">What is the game that needs to be tested?</param>
    void IsButtonGame(ButtonGame game)
    {
        //checks if is a button game
        if (game != null)
        {
            game.fireSource = fire;
            fire.buttons.Add(game);
        }
    }
}
