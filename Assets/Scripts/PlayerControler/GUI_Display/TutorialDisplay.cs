﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisplay : MonoBehaviour {

    [SerializeField]
    private string tutorialText;

<<<<<<< HEAD
    [SerializeField]
    private bool isEntered = false;

    [SerializeField]
    private float timer = 0.4f;

    public void SetTimer(float timer)
    {
        this.timer = timer;
    }

    private void OnGUI()
    {
        if (timer > 0.0f)
        {
            GUI.skin.label.fontSize = 25;
            GUI.Label(new Rect(275, 150, 300, 300), tutorialText);
        }
    }

    public void SetTutorialText(string tutorialText)
    {
        this.tutorialText = tutorialText;
    }
	
    public void EnteredTrigger(bool isEntered)
    {
        this.isEntered = isEntered;
    }

    private void Update()
    {
        if(isEntered)
        {
            timer -= (0.1f * Time.deltaTime);
        }
        if(timer<=0.0f)
        {
            isEntered = false;
        }
    }
=======
    private void OnGUI()
    {
        //tutorialText = GUI.TextField(new Rect(50, 50, 500, 500), stringToEdit, 25);
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
>>>>>>> dd15de67da3581d7b2366198ae7faee105c944b1
}
