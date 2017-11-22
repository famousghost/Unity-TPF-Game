using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActive : MonoBehaviour {

    [SerializeField]
    private AudioSource audioSoruce;

    [SerializeField]
    private AudioClip switchClip;

	// Use this for initialization
	void Start () {
        switchClip = Resources.Load("Switches/Switch", typeof(AudioClip)) as AudioClip;
        audioSoruce = GetComponent<AudioSource>();
        audioSoruce.clip = switchClip;
	}

    public void PlayPowerOnSound()
    {
        audioSoruce.Play();
    }
}
