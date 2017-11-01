using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage: MonoBehaviour {

    [SerializeField]
    private float hp;

    [SerializeField]
    private const float MAXHP = 100.0f;

	// Use this for initialization
	void Start () {
        hp = MAXHP;
	}

    public void SubstractHP(float damage)
    {
        hp -= damage;
    }
}
