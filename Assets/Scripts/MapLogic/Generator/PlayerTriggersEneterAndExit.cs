using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggersEneterAndExit : MonoBehaviour {

    #region Items From Usable Item
    [SerializeField]
    private Generator generator;
    #endregion

    #region System Methods
    void Start()
    {
        generator = GameObject.Find("UsableItems").GetComponent<Generator>();
    }
    #endregion

    #region Triggers
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GeneratorPlace")
        {
            generator.SetUseGenerator(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "GeneratorPlace")
        {
            generator.SetUseGenerator(false);
        }
    }
    #endregion
}