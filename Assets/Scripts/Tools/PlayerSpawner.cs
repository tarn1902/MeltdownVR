using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    void Awake()
    {
        if (Player.instance == null)
        {
            Instantiate(playerPrefab);
        }
    }
}
