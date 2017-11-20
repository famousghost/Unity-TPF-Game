using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEmergencyPower : MonoBehaviour {

    [SerializeField]
    private Rigidbody generator;

    [SerializeField]
    private EmergencyLights emergencyLights;

    [SerializeField]
    private PlayerControler playerControler;

	// Use this for initialization
	void Start () {
        generator = this.GetComponent<Rigidbody>();
        playerControler = GameObject.Find("Player").GetComponent<PlayerControler>();
        emergencyLights = GameObject.FindGameObjectWithTag("GeneratorPlace").GetComponent<EmergencyLights>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GeneratorPlace")
        {
            playerControler.SetGameObj(null);
            Destroy(generator);
            emergencyLights.SetLightState(LightsState.Emergency);
        }
    }
}
