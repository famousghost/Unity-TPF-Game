using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsAction : MonoBehaviour
{

    [SerializeField]
    private bool chnagePosition = false;
    [SerializeField]
    private bool isHover = false;

    [SerializeField]
    private bool startClicked = false;

    [SerializeField]
    private bool buttonIsClicked = false;

    [SerializeField]
    private Transform cameraMenuPosition;

    [SerializeField]
    private Transform cameraNewGamePostion;

    [SerializeField]
    private string buttonName = "";

    [SerializeField]
    private Transform textHoverTransform;

    [SerializeField]
    private Transform textNormalTransform;

    [SerializeField]
    private Transform textCurrentTransform;


    [SerializeField]
    private float step = 2.0f;

    // Use this for initialization
    void Start()
    {
        cameraMenuPosition = GameObject.Find("Main Camera").GetComponent<Transform>();
        cameraNewGamePostion = GameObject.Find("CameraNewGamePosition").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTextPosition();
        CheckWhichButtonClicked();
    }

    void OnMouseEnter()
    {
        Debug.Log(this.gameObject.name);
        isHover = true;
    }

    void OnMouseExit()
    {
        Debug.Log("Cofam");
        isHover = false;
    }

    void OnMouseDown()
    {
        buttonName = this.gameObject.name;
        Debug.Log(buttonName);
    }

    private void ChangeTextPosition()
    {
        if (isHover == true)
        {
            textCurrentTransform.position = Vector3.MoveTowards(textCurrentTransform.position, textHoverTransform.position, step * Time.deltaTime);
        }
        else
        {
            textCurrentTransform.position = Vector3.MoveTowards(textCurrentTransform.position, textNormalTransform.position, step * Time.deltaTime);
        }
    }



    private void CheckWhichButtonClicked()
    {
        if(startClicked)
        {
            cameraMenuPosition.rotation = Quaternion.RotateTowards(cameraMenuPosition.rotation, cameraNewGamePostion.rotation, step*10 * Time.deltaTime);
            cameraMenuPosition.position = Vector3.MoveTowards(cameraMenuPosition.position, cameraNewGamePostion.position, step * Time.deltaTime);
            bool checkCameraPosition = (cameraMenuPosition.rotation == cameraNewGamePostion.rotation);
            if (checkCameraPosition)
                 SceneManager.LoadScene(1);
        }
        if (buttonName == "StartPosition" && buttonIsClicked == false)
        {
            startClicked = true;
            buttonIsClicked = true;
        }
        if (buttonName == "Exit" && buttonIsClicked == false)
        {
            Application.Quit();
        }

    }

    public string GetButtonName()
    {
        return this.buttonName;
    }

    public void SetButtonName(string buttonName)
    {
        this.buttonName = buttonName;
    }
}
