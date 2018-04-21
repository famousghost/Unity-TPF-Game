using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayNote : MonoBehaviour {

    #region TextToDisplay
    [SerializeField]
    private string noteText;
    #endregion

    #region Camera
    [SerializeField]
    private Camera playerView;
    #endregion

    #region Canvas
    [SerializeField]
    private Canvas noteCanvas;
    #endregion

    #region Class
    [SerializeField]
    private PlayerControler playerController;

    [SerializeField]
    private ItemsPickUp itemsPickUp;

    [SerializeField]
    private ItemDisplay itemDisplay;

    [SerializeField]
    private Note note;
    #endregion

    #region Bool
    [SerializeField]
    private bool noteActive = false;
    #endregion

    #region Const
    [SerializeField]
    private const float MAXDISANCEOFRAY = 2.3f;
    #endregion


    #region Texture2d
    [SerializeField]
    private Texture2D pickUpNote;
    #endregion

    // Use this for initialization
    void Start () {
        playerView = GetComponentInChildren<Camera>();
        itemsPickUp = GameObject.Find("Player").GetComponent<ItemsPickUp>();
        playerController = GameObject.Find("Player").GetComponent<PlayerControler>();
        itemDisplay = GameObject.Find("Player").GetComponentInChildren<ItemDisplay>();
    }
	
	// Update is called once per frame
	void Update () {
        if(noteActive)
        {
            noteCanvas.GetComponent<Canvas>().enabled = true;
            playerController.enabled = false;
            itemsPickUp.enabled = false;
            itemDisplay.enabled = false;
        }
        else
        {
            noteCanvas.GetComponent<Canvas>().enabled = false;
            playerController.enabled = true;
            itemsPickUp.enabled = true;
            itemDisplay.enabled = true;
        }
	}

    void FixedUpdate()
    {
        RayCast();
    }

    #region GUI
    void OnGUI()
    {
        Ray ray = new Ray(playerView.transform.position, playerView.transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MAXDISANCEOFRAY))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Notes"))
            {
                if(!noteActive)
                    GUI.DrawTexture(new Rect((Screen.width / 2) - 25, (Screen.height / 2) - 25, 50,50), pickUpNote);
            }
        }
    }
    #endregion

    void RayCast()
    {
        Ray ray = new Ray(playerView.transform.position, playerView.transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MAXDISANCEOFRAY))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Notes"))
            {
                note = hit.collider.GetComponent<Note>();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    noteCanvas.GetComponentInChildren<Text>().text = note.GetTextOfNote();
                    noteActive = !noteActive;
                    noteCanvas.GetComponent<Canvas>().enabled = false;
                    playerController.enabled = true;
                    itemsPickUp.enabled = true;
                    itemDisplay.enabled = true;
                }
            }
            else
            {
                noteActive = false;
            }
        }
    }

    public void SetNoteActive(bool noteActive)
    {
        this.noteActive = noteActive;
    }
}
