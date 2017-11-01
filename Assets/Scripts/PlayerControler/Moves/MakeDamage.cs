using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class MakeDamage : MonoBehaviour {

    [SerializeField]
    private float damage;

    [SerializeField]
    private TakeDamage takeDamage;

	// Use this for initialization
	void Start () {
        damage = 2.0f;
        takeDamage = GameObject.Find("Player").GetComponent<TakeDamage>();
	}
	
	// Update is called once per frame
	void Update () {
        Atack();
    }

    private void Atack()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            takeDamage.SubstractHP(damage);
        }
    }
}
