using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicked : MonoBehaviour {

    [SerializeField]
    private GameObject thisButton;

    [SerializeField]
    private Transform buttonClicked;

    [SerializeField]
    private Transform currentButtonState;


    [SerializeField]
    private Transform buttonReleased;

    [SerializeField]
    private bool isClicked = false;

    [SerializeField]
    private float speedOfClicked = 0.2f;

	// Use this for initialization
	void Start () {
        thisButton = this.gameObject;
        buttonClicked = thisButton.transform.FindChild("ButtonClicked");
        buttonReleased = thisButton.transform.FindChild("ButtonReleased");
        currentButtonState = thisButton.transform.FindChild("Button");

    }

    void Update()
    {
        ChangeButtonState();
    }

    private void ChangeButtonState()
    {
        if(isClicked)
            thisButton.transform.FindChild("Button").position = Vector3.MoveTowards(thisButton.transform.FindChild("Button").position, buttonClicked.position, speedOfClicked * Time.deltaTime);
        else
            thisButton.transform.FindChild("Button").position = Vector3.MoveTowards(thisButton.transform.FindChild("Button").position, buttonReleased.position, speedOfClicked * Time.deltaTime);
        if(currentButtonState.position == buttonClicked.position)
        {
            isClicked = false;
        }
    }

    public void ButtonClick()
    {
        isClicked = true;
    }
}
