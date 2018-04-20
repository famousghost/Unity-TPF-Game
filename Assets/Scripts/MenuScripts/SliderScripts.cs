using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderScripts : MonoBehaviour {

    #region PlayerPrefs
    [SerializeField]
    private float playerVolume;
    [SerializeField]
    private float playerBrightness;
    #endregion

    #region Sliders
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Slider brightnessSlider;
    #endregion

    #region Audio Mixer
    public AudioMixer mainMixer;
    #endregion

    #region Light
    public Light mainLight;
    #endregion

    #region Volume Slider Function
    public void setVolume (float volume)
    {
        mainMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("playerVolume", volume);
    }
    #endregion

    #region Brightness Slider Function
    public void setBrightness(float brightness)
    {
        mainLight.intensity = brightness + (float)0.5;
        PlayerPrefs.SetFloat("playerBrightness", brightness);
    }
    #endregion
    
    #region On Startup
    private void Start()
    {
        playerVolume = PlayerPrefs.GetFloat("playerVolume");
        playerBrightness = PlayerPrefs.GetFloat("playerBrightness");
        brightnessSlider.value = playerBrightness;
        volumeSlider.value = playerVolume;
    }
    #endregion
}
