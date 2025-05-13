using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingManager : MonoBehaviour
{
    public Slider bgmSlider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        bgmSlider.value = savedVolume;

        if (AudioManager.instance != null)
        {
            AudioManager.instance.AudioSource.volume = savedVolume;
        }

        bgmSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float value)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.AudioSource.volume = value;
            PlayerPrefs.SetFloat("BGMVolume", value);
        }
    }
}
