using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenColseSound : MonoBehaviour
{

    [SerializeField]
    private DoorIsOpenedAndClosed doorOpenedAndClosed;

    [SerializeField]
    private AudioSource sourceOfDoorSounds;

    [SerializeField]
    private EmergencyLights emergencyLights;

    [SerializeField]
    private AudioClip doorOpenSound;

    [SerializeField]
    private AudioClip doorCloseSound;


    // Use this for initialization
    void Awake()
    {
        doorOpenedAndClosed = GetComponent<DoorIsOpenedAndClosed>();
        emergencyLights = GameObject.FindGameObjectWithTag("GeneratorPlace").GetComponent<EmergencyLights>();
        sourceOfDoorSounds = GetComponent<AudioSource>();
        doorOpenSound = Resources.Load("Switches/ClickOn", typeof(AudioClip)) as AudioClip;
        doorCloseSound = Resources.Load("Switches/ClickOn", typeof(AudioClip)) as AudioClip;
    }

    void Update()
    {
        if(emergencyLights.GetLightState() != LightsState.Off)
        {
            doorOpenSound = Resources.Load("GameSounds/DoorSounds/openDoor", typeof(AudioClip)) as AudioClip;
            doorCloseSound = Resources.Load("GameSounds/DoorSounds/openDoor", typeof(AudioClip)) as AudioClip;
        }
    }

    private void PlayOpenDoorSound()
    {
        if (doorOpenSound != null)
            sourceOfDoorSounds.clip = doorOpenSound;
    }

    private void PlayCloseDoorSound()
    {
        if (doorCloseSound != null)
            sourceOfDoorSounds.clip = doorCloseSound;
    }

    public void PlayClip()
    {
        if(doorOpenedAndClosed.GetDoorIsOpened())
        {
            PlayOpenDoorSound();
        }
        else
        {
            PlayCloseDoorSound();
        }
        sourceOfDoorSounds.Play();
    }
}
