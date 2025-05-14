using UnityEngine;
using UnityEngine.UI;


public class SettingManager : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    void Start()
    {
        float savedBGM = PlayerPrefs.GetFloat("BGMVolume", 0.3f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.3f);

        bgmSlider.value = savedBGM;
        sfxSlider.value = savedSFX;

        if (AudioManager.instance != null)
        {
            AudioManager.instance.BGMSource.volume = savedBGM;
            AudioManager.instance.SFXSource.volume = savedSFX;
        }

        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    void OnBGMVolumeChanged(float value)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.BGMSource.volume = value;
            PlayerPrefs.SetFloat("BGMVolume", value);
        }
    }

    void OnSFXVolumeChanged(float value)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SFXSource.volume = value;
            PlayerPrefs.SetFloat("SFXVolume", value);
        }
    }
}
