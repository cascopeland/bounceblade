using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchesMoatPuzzleManager : MonoBehaviour
{
    public static WitchesMoatPuzzleManager instance;

    private int activatedRuins = 0;
    private int solution = 6;
    public Light pointLight;
    public GameObject pickUp;
    public GameObject bridge;

    void Awake()
    {
        instance = this;
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
            bridge.GetComponent<Animator>().SetInteger("BridgeRaised", 1);
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
