using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle2Manager : MonoBehaviour
{
    public static Puzzle2Manager instance;

    private int activatedRuins = 0;
    private int solution = 3;
    public Light pointLight;
    public GameObject pickUp;
    public GameObject skeletons;


    void Awake()
    {
        instance = this;
        skeletons.SetActive(false);
    }


    public void CheckSolution()
    {
        activatedRuins++;

        if (activatedRuins == solution)
        {
            pickUp.SetActive(true);
            FadeInPointLight();
        }

        if (activatedRuins == 2)
        {
            skeletons.SetActive(true);
        }
    }

    private void FadeInPointLight()
    {
        StartCoroutine(FadeInLight());
    }

    private IEnumerator FadeInLight()
    {
        float duration = 4f;
        float elapsedTime = 0f;
        float startIntensity = pointLight.intensity;
        float targetIntensity = 15f;

        pointLight.gameObject.SetActive(true);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            pointLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
            yield return null;
        }

        pointLight.intensity = targetIntensity;
    }
}
