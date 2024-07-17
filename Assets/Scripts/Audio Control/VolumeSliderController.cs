using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    public static Slider slider;
    public static float savedVolume;
    // Start is called before the first frame update
    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
        savedVolume = gameManager.savedVolume;
        slider = gameObject.GetComponent<Slider>();
        slider.value = gameManager.savedVolume = AudioListener.volume;
    }

    public void UpdateVolume()
    {
        AudioListener.volume = slider.value;
        if (AudioListener.volume != 0)
        {
            gameManager.savedVolume = slider.value;
        }
    }
}
