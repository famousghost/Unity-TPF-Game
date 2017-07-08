using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAction : MonoBehaviour {
    //Main class button action
    #region Class Variables
    [Header("Class variables")]
    [SerializeField]
    private ButtonsAction buttonAction;
    #endregion

    //Position of Camera main menu position, settings position, and current position
    #region Transform Variables
    [Header("Transform variables")]
    [SerializeField]
    private Transform cameraMenuPosition;

    [SerializeField]
    private Transform cameraCurrentPosition;

    [SerializeField]
    private Transform cameraSettingsPosition;
    #endregion

    //Check position of camera if is changed changePosition = true
    #region Bool Variables
    [Header("Bool Varibales")]
    [SerializeField]
    private bool changePosition = false;
    #endregion

    //Step to move and roatate camera
    #region Float Variables
    [Header("Float Varibales")]
    [SerializeField]
    private float step = 2.0f;

    [SerializeField]
    private float stepRoatione = 70.0f;
    #endregion

    //Update and Start Method
    #region Unity System Methods
    void Start()
    {
        buttonAction = GetComponent<ButtonsAction>();
        cameraCurrentPosition = GameObject.Find("Main Camera").GetComponent<Transform>();
        cameraMenuPosition = GameObject.Find("CameraMenuPosition").GetComponent<Transform>();
        cameraSettingsPosition = GameObject.Find("CameraSettingsPostion").GetComponent<Transform>();
    }

	// Update is called once per frame
	void Update () {
        if (changePosition)
        {
            ChangePosition();
            ChangeRoatione();
        }
        CheckChanged();
        ChangeBoolPosition();
    }
    #endregion

    //Change and check position of Camera
    #region Change Position Methods
    private void ChangePosition()
    {
        if (changePosition)
        {
            cameraCurrentPosition.position = Vector3.MoveTowards(cameraCurrentPosition.position, cameraMenuPosition.position, step * Time.deltaTime);
        }
    }

    private void ChangeRoatione()
    {
        if (changePosition)
        {
            cameraCurrentPosition.rotation = Quaternion.RotateTowards(cameraCurrentPosition.rotation, cameraMenuPosition.rotation, stepRoatione * Time.deltaTime);
        }
    }

    private void CheckChanged()
    {
        bool check = (cameraCurrentPosition.rotation == cameraMenuPosition.rotation);
        if (check)
        {
            changePosition = false;
        }
    }


    private void ChangeBoolPosition()
    {
        bool checkNameButtonAndPosition = (buttonAction.GetButtonName() == "BackPosition") && (cameraCurrentPosition.rotation == cameraSettingsPosition.rotation);
        if (checkNameButtonAndPosition)
        {
            changePosition = true;
        }
        buttonAction.SetButtonName("");
    }
    #endregion
}
