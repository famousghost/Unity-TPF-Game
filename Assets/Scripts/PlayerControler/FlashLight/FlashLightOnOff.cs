using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightOnOff : MonoBehaviour {

    [SerializeField]
    private Light flashLight;

    [SerializeField]
    private KeysInput keysInput;

    [SerializeField]
    private float batteryPowerLevel = 100.0f;

    private const float BATTERYLOWTIME = 420.0f;

    [SerializeField]
    private float timeToBatteryLow = BATTERYLOWTIME;

	// Use this for initialization
	void Start () {
        flashLight = GetComponent<Light>();
        keysInput = GetComponentInParent<KeysInput>();
	}
	
	// Update is called once per frame
	void Update () {
        OnOffMethod();
    }

    private void OnOffMethod()
    {
        CheckBatterLow();
        if (batteryPowerLevel > 0.0f)
            flashLight.enabled = keysInput.GetFlashLightState();
        else
            flashLight.enabled = false;
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
