using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class StartMenu : MonoBehaviour
{
    public GameObject[] menus;

    private SteamVR_LoadLevel loader;

    void Start()
    {
        loader = GetComponent<SteamVR_LoadLevel>();
        ChooseMenu("Start Menu");
    }

    public void ChooseMenu(string name)
    {
        foreach (GameObject menu in menus)
        {
            if (menu.name == name)
            {
                menu.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
            }
        }
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void LoadFreePlay(int difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
        PlayerPrefs.SetInt("Level", 0);
        loader.levelName = "Master_Game_Scene";
        loader.Trigger();
    }

    public void LoadLevel(string level)
    {
        PlayerPrefs.SetInt("Level", 1);
        loader.levelName = level;
        loader.Trigger();
    }
}
