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
    private AudioClip doorOpenSound;

    [SerializeField]
    private AudioClip doorCloseSound;


    // Use this for initialization
    void Start()
    {
        doorOpenedAndClosed = GetComponent<DoorIsOpenedAndClosed>();
        sourceOfDoorSounds = GetComponent<AudioSource>();
        doorOpenSound = Resources.Load("DoorSounds/openDoor.wav",typeof(AudioClip)) as AudioClip;
        doorCloseSound = Resources.Load("DoorSounds/openDoor.wav", typeof(AudioClip)) as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {

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
