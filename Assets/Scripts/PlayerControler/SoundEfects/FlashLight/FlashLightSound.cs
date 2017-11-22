using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightSound : MonoBehaviour {

    [SerializeField]
    protected AudioClip flashLightOn;

    [SerializeField]
    protected AudioClip flashLightOff;

    [SerializeField]
    protected AudioSource flashLightSource;

    public void PlayFlashLightOn()
    {
        flashLightSource.clip = flashLightOn;
        flashLightSource.Play();
    }

    public void PlayFlashLightOff()
    {
        flashLightSource.clip = flashLightOff;
        flashLightSource.Play();
    }
}
