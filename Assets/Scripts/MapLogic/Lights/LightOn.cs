using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOn : MonoBehaviour {

    [SerializeField]
    private Light corridorLight;

    [SerializeField]
    private EmergencyLights emergencyLights;

	// Use this for initialization
	void Start () {
        corridorLight = GetComponent<Light>();
        emergencyLights = GameObject.FindGameObjectWithTag("GeneratorPlace").GetComponent<EmergencyLights>();
	}
	
	// Update is called once per frame
	void Update () {
        if (emergencyLights.GetLightState() == LightsState.Emergency)
        {
            corridorLight.color = Color.red;
            corridorLight.enabled = true;
        }
	}
}
