using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightsState
{
    Off = 0,
    Emergency,
    Normal
};

public class EmergencyLights : MonoBehaviour {

    [SerializeField]
    private LightsState lightState;

    public LightsState GetLightState()
    {
        return lightState;
    }

    public void SetLightState(LightsState lightState)
    {
        this.lightState = lightState;
    }
}
