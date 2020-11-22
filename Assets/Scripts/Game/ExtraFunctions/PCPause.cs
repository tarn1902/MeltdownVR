using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCPause : MonoBehaviour
{
    public InGameMenu inGameMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inGameMenu.ShowMenu();
        }
    }
}
