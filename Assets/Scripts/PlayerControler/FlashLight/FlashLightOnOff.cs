using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightOnOff : FlashLightSound {

    [SerializeField]
    private Light flashLight;

    [SerializeField]
    private float batteryPowerLevel = 100.0f;

    private const float BATTERYLOWTIME = 420.0f;

    [SerializeField]
    private float timeToBatteryLow = BATTERYLOWTIME;

	// Use this for initialization
	void Start () {

        flashLightSource = GetComponent<AudioSource>();
        flashLightOn = Resources.Load("FlashLight/flashlighton", typeof(AudioClip)) as AudioClip;
        flashLightOff = Resources.Load("FlashLight/flashlightoff", typeof(AudioClip)) as AudioClip;
        flashLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        OnOffMethod();
    }

    private void OnOffMethod()
    {
        CheckBatterLow();
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (batteryPowerLevel > 0.0f)
            {
                if (flashLight.enabled)
                {
                    PlayFlashLightOff();
                    flashLight.enabled = false;
                }
                else
                {
                    PlayFlashLightOn();
                    flashLight.enabled = true;
                }
            }
            else
                flashLight.enabled = false;
        }
    }

    private void CheckBatterLow()
    {
        if (flashLight.enabled)
        {
            if (timeToBatteryLow <= 0.0f)
            {
                batteryPowerLevel -= 20.0f;
            }
            timeToBatteryLow -= Time.deltaTime * 1.0f;
        }
    }
}
