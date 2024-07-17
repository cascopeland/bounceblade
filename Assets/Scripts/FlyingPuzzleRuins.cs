using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPuzzleRuins : MonoBehaviour
{
    public static FlyingPuzzleRuins instance;

    private int activatedRuins = 0;
    private int solution = 1;
    public Light pointLight;

    void Awake()
    {
        instance = this;
    }

    public void CheckSolution()
    {
        activatedRuins++;

        if (activatedRuins == solution)
        {
            FadeInPointLight();
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
