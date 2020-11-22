using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class InGameMenu : MonoBehaviour
{
    public GameObject inGameMenu;

    void Start()
    {
        HideMenu();
    }

    public void ShowMenu()
    {
        inGameMenu.SetActive(true);
    }

    public void HideMenu()
    {
        inGameMenu.SetActive(false);
    }

    public void QuitToMenu()
    {
        GetComponent<SteamVR_LoadLevel>().Trigger();
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
