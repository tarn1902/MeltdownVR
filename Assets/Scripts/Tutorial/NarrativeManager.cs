using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Valve.VR;

public class NarrativeManager : MonoBehaviour
{
    private int maxFails = 3;
    public int currentFails = 0;

    public LightToggle[] failLights;

    public PlayableDirector playableDirector;
    public PlayableAsset loseTimeline;

    public SteamVR_Action_Boolean menuAction;
    public GameObject pauseMenu;

    private void Start()
    {
        maxFails = 3;
        currentFails = 0;
    }

    public void Update()
    {
        if (menuAction.GetState(SteamVR_Input_Sources.LeftHand) || menuAction.GetState(SteamVR_Input_Sources.RightHand))
        {
            GetMenu();
        }
    }

    public void PlayTimeline()
    {
        playableDirector.Play();
    }

    public void PauseTimeline()
    {
        playableDirector.Pause();
    }

    public void Fail()
    {
        if (currentFails >= maxFails)
        {
            playableDirector.Stop();
            playableDirector.playableAsset = loseTimeline;
            playableDirector.Play();
        }
        else
        {
            failLights[currentFails].TurnOn();
            currentFails++;
        }
    }

    public void GetMenu()
    {
        pauseMenu.SetActive(true);
    }
}
