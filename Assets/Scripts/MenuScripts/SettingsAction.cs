using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsAction : MonoBehaviour {

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
    private Transform cameraSettingsPosition;

    [SerializeField]
    private Transform cameraCurrentPosition;

    [SerializeField]
    private Transform cameraMenuPosition;
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
        ChangeBoolPosition();
        CheckChanged();
    }
    #endregion

    //Change and check position of Camera
    #region Change position and Roatatione
    private void ChangePosition()
    {
        if (changePosition)
        {
            cameraCurrentPosition.position = Vector3.MoveTowards(cameraCurrentPosition.position, cameraSettingsPosition.position, step * Time.deltaTime);
        }
    }

    private void ChangeRoatione()
    {
        if (changePosition)
        {
            cameraCurrentPosition.rotation = Quaternion.RotateTowards(cameraCurrentPosition.rotation, cameraSettingsPosition.rotation, stepRoatione * Time.deltaTime);
        }
    }

    private void CheckChanged()
    {
        bool check = (cameraCurrentPosition.rotation == cameraSettingsPosition.rotation);
        if (check)
        {
            changePosition = false;
        }
    }

    private void ChangeBoolPosition()
    {
        bool checkNamecheckNameButtonAndPosition = (buttonAction.GetButtonName() == "SettingsPosition") && (cameraCurrentPosition.rotation == cameraMenuPosition.rotation);
        if (checkNamecheckNameButtonAndPosition)
        {
            changePosition = true;
        }
        buttonAction.SetButtonName("");
    }
    #endregion
}
