using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class MuteBtnController : MonoBehaviour
{
    public static bool isMuted = false;
    Button button;
    public Sprite muteSprite;
    public Sprite volumeSprite;
    // Start is called before the first frame update
    void Awake()
    {
        button = gameObject.GetComponent<Button>();
    }

    public void ToggleMute()
    {
        if (isMuted)
        {
            button.image.sprite = volumeSprite;
            AudioListener.volume = VolumeSliderController.slider.value = VolumeSliderController.savedVolume;
        }
        else
        {
            button.image.sprite = muteSprite;
            AudioListener.volume = 0;
            VolumeSliderController.slider.value = 0;
        }
        isMuted = !isMuted;
    }
}
