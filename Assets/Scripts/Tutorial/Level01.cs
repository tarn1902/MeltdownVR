using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Level01 : NarrativeManager
{
    private bool button1Pressed;
    private bool button2Pressed;

    private bool shouldCount = false;

    public enum Section
    {
        Single,
        Double,
        Timed,
        Survival,
        Final
    }

    private Section section;

    public float timer = 0;

    public ParticleSystem[] fail1;
    public ParticleSystem[] fail2;
    public ParticleSystem[] fail3;
    public ParticleSystem[] fail4;

    public ButtonGame2 button1;
    public ButtonGame2 button2;


    new public void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<SteamVR_LoadLevel>().Trigger();
        }

        if (section == Section.Survival)
        {
            if (timer >= 30f)
            {
                ContinueGame(Section.Final);
            }
            else if (shouldCount)
            {
                timer += Time.deltaTime;
            }
        }
    }

    public void Continue()
    {
        switch (section)
        {
            case Section.Single:
                ContinueGame(Section.Double);
                break;

            case Section.Double:
                if (button1Pressed && button2Pressed)
                {
                    ContinueGame(Section.Timed);
                }
                break;

            case Section.Timed:
                if (button1Pressed && button2Pressed)
                {
                    ContinueGame(Section.Survival);
                }
                break;
        }
    }

    new public void Fail()
    {
        base.Fail();

        switch (currentFails)
        {
            case 1:
                foreach (ParticleSystem particle in fail1)
                {
                    var main = particle.main;
                    main.loop = true;
                    particle.Play();
                }
                break;
            case 2:
                foreach (ParticleSystem particle in fail2)
                {
                    var main = particle.main;
                    main.loop = true;
                    particle.Play();
                }
                break;
            case 3:
                foreach (ParticleSystem particle in fail3)
                {
                    var main = particle.main;
                    main.loop = true;
                    particle.Play();
                }
                break;
            case 4:
                foreach (ParticleSystem particle in fail4)
                {
                    var main = particle.main;
                    main.loop = true;
                    particle.Play();
                }
                break;
        }
    }

    public void CycleFail()
    {
        if (section == Section.Timed || section == Section.Survival)
        {
            Fail();
        }
    }

    public void Button1Pressed()
    {
        if (section != Section.Survival)
        {
            button1Pressed = true;
            button1.StopGameSequence();
            Continue();
        }
    }

    public void Button2Pressed()
    {
        if (section != Section.Survival)
        {
            button2Pressed = true;
            button2.StopGameSequence();
            Continue();
        }
    }

    public void ContinueGame(Section currentSection)
    {
        PlayTimeline();
        section = currentSection;
        button1Pressed = false;
        button2Pressed = false;
    }

    public void StartCounting()
    {
        shouldCount = true;
    }
}
