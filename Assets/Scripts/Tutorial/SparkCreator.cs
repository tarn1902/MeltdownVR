using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkCreator : MonoBehaviour
{
    public ParticleSystem[] sparks;

    public ButtonGame2 button;

    public bool startCoroutine;

    private void Update()
    {
        if (button.IsOnFire && !startCoroutine)
        {
            StartCoroutine("CreateSparks");
            startCoroutine = true;
        }
        else if (!button.IsOnFire && startCoroutine)
        {
            startCoroutine = false;
        }
    }

    public IEnumerator CreateSparks()
    {
        while (button.IsOnFire)
        {
            sparks[Random.Range(0, sparks.Length)].Play();
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }
}
