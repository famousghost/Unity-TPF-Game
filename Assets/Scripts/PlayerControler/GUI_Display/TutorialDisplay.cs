using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisplay : MonoBehaviour {

    [SerializeField]
    private string tutorialText;

    private void OnGUI()
    {
        //tutorialText = GUI.TextField(new Rect(50, 50, 500, 500), stringToEdit, 25);
    }

    // Use this for initialization
    void Start () {
        OnGUI();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
