using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR;

public class GameOverMenu : MonoBehaviour
{
    SteamVR_LoadLevel loader;
    public TextMeshProUGUI gameOverText;
    [TextArea]
    public string winMessage;
    [TextArea]
    public string loseMessage;
    [TextArea]
    public string levelMessage;

    private void Awake()
    {
        loader = GetComponent<SteamVR_LoadLevel>();

        if (PlayerPrefs.GetInt("Level") == 0)
        {
            if (PlayerPrefs.GetInt("LastResult") == 0)
            {
                gameOverText.text = loseMessage;
            }
            else
            {
                gameOverText.text = winMessage;
            }
        }
        else
        {
            gameOverText.text = levelMessage;
        }
    }


    public void Restart()
    {
        if (PlayerPrefs.GetInt("Level") == 0)
        {
            loader.levelName = "Master_Game_Scene";
        }
        else
        {
            loader.levelName = "Master_Level_1";
        }

        loader.Trigger();
    }

    public void LoadLevel(string level)
    {
        loader.levelName = level;
        loader.Trigger();
    }
}
