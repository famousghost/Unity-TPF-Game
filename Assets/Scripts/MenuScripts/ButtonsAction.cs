using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsAction : MonoBehaviour
{
    [SerializeField]
    private bool isHover = false;

    private int textWritingIterator = 0;

    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private Slider loadingSlider;

    [SerializeField]
    private AsyncOperation async;

    [SerializeField]
    private Text loadingDone;

    [SerializeField]
    private string textToLoadingScreen;

    [SerializeField]
    private Text loadingText;

    [SerializeField]
    private Image loadingScreen;

    [SerializeField]
    private bool startClicked = false;

    [SerializeField]
    private bool gameStarted = false;

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
        loadingText = GameObject.Find("LoadingScreen").GetComponentInChildren<Text>();
        loadingScreen = GameObject.Find("Loading").GetComponentInChildren<Image>();
        loadingDone = GameObject.Find("LoadingDone").GetComponent<Text>();
        loadingSlider = loadingScreen.GetComponentInChildren<Slider>();
        backgroundImage = GameObject.Find("Background").GetComponent<Image>();
        fillImage = GameObject.Find("Fill").GetComponent<Image>();
        loadingSlider.enabled = false;
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
        if (startClicked && !gameStarted)
        {
            cameraMenuPosition.rotation = Quaternion.RotateTowards(cameraMenuPosition.rotation, cameraNewGamePostion.rotation, step * 10 * Time.deltaTime);
            cameraMenuPosition.position = Vector3.MoveTowards(cameraMenuPosition.position, cameraNewGamePostion.position, step * Time.deltaTime);
            bool checkCameraPosition = (cameraMenuPosition.rotation == cameraNewGamePostion.rotation);
            if (checkCameraPosition)
            {
                gameStarted = true;
                LoadGame(1);
            }
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

    public void LoadGame(int lvl)
    {
        StartCoroutine(LoadingScreenScene(lvl));
    }

    IEnumerator LoadingScreenScene(int lvl)
    {
        loadingText.text = textToLoadingScreen;
        loadingSlider.enabled = true;
        loadingScreen.GetComponent<Image>().enabled = true;
        async = SceneManager.LoadSceneAsync(lvl);
        async.allowSceneActivation = false;
        backgroundImage.GetComponent<Image>().enabled = true;
        fillImage.GetComponent<Image>().enabled = true;

        while (async.isDone == false)
        {
            loadingText.GetComponent<Text>().enabled = true;
            loadingSlider.value = async.progress;
            if (async.progress == 0.9f)
            {
                loadingDone.GetComponent<Text>().enabled = true;
                if (Input.anyKey)
                    async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
