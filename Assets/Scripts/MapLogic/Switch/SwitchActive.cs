using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActive : MonoBehaviour {

    [SerializeField]
    private AudioSource audioSoruce;

    [SerializeField]
    private EmergencyLights emergencyLight;

    [SerializeField]
    private AudioClip switchClip;

	// Use this for initialization
	void Start () {
        switchClip = Resources.Load("Switch", typeof(AudioClip)) as AudioClip;
        audioSoruce = GetComponent<AudioSource>();
        emergencyLight = GameObject.FindGameObjectWithTag("GeneratorPlace").GetComponent<EmergencyLights>();
        audioSoruce.clip = switchClip;
	}

    public void PlayPowerOnSound()
    {
        audioSoruce.Play();
    }
}
