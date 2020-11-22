using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LoadStartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SteamVR_LoadLevel>().Trigger();
    }
}
