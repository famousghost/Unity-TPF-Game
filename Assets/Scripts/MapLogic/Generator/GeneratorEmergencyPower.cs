using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEmergencyPower : MonoBehaviour {

    [SerializeField]
    private Rigidbody generator;

    [SerializeField]
    private SwitchActive switchAcitve;

    [SerializeField]
    private EmergencyLights emergencyLights;

    [SerializeField]
    private PlayerControler playerControler;

	// Use this for initialization
	void Start () {
        generator = this.GetComponent<Rigidbody>();
        switchAcitve = GameObject.FindGameObjectWithTag("GeneratorPlace").GetComponent<SwitchActive>();
        playerControler = GameObject.Find("Player").GetComponent<PlayerControler>();
        emergencyLights = GameObject.FindGameObjectWithTag("GeneratorPlace").GetComponent<EmergencyLights>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GeneratorPlace")
        {
            playerControler.SetGameObj(null);
            switchAcitve.PlayPowerOnSound();
            Destroy(generator);
            emergencyLights.SetLightState(LightsState.Emergency);
        }
    }
}
