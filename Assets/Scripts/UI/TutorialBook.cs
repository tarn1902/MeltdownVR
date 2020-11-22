using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBook : MonoBehaviour
{
    private int currentIndex = 0;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject[] tutorialPlates;

    void Start()
    {
        Display(0);
    }

    public void Display(int indexChange)
    {
        currentIndex += indexChange;
        currentIndex = Mathf.Clamp(currentIndex, 0, tutorialPlates.Length - 1);

        for (int i = 0; i < tutorialPlates.Length; i++)
        {
            if (i == currentIndex)
            {
                tutorialPlates[i].SetActive(true);
            }
            else
            {
                tutorialPlates[i].SetActive(false);
            }
        }

        if (currentIndex == 0)
        {
            leftButton.SetActive(false);
        }
        else
        {
            leftButton.SetActive(true);
        }

        if (currentIndex == tutorialPlates.Length - 1)
        {
            rightButton.SetActive(false);
        }
        else
        {
            rightButton.SetActive(true);
        }
    }
}
