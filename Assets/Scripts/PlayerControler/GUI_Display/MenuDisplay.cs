using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDisplay : MonoBehaviour {

    #region MenuCanvas
    [SerializeField]
    private GameObject MenuCanvas;
    #endregion

    #region Disabling stuff so that cursor is visible and player can't move
    [SerializeField]
    private PlayerControler playerControler;

    [SerializeField]
    private ItemDisplay itemDisplay;

    [SerializeField]
    private ItemsPickUp itemsPickUp;

    [SerializeField]
    private DisplayNote displayNote;
    #endregion

    #region Active Bool
    private bool Active = false;
    #endregion

    #region Start (Canvas Off)
    // Use this for initialization
    void Start () {
        displayNote = GetComponent<DisplayNote>();
        MenuCanvas.SetActive(false);
    }
    #endregion

    #region Update
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Active == false)
            {
                menuDislpayerTrue();
            }
            else
            {
                menuDislpayerFalse();
            }
        }
	}
    #endregion

    #region Option Activating Function
    void menuDislpayerTrue()
    {
        Active = true;
        MenuCanvas.SetActive(true);
        displayNote.SetNoteActive(false);
        playerControler.enabled = false;
        itemsPickUp.enabled = false;
        itemDisplay.enabled = false;
        displayNote.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void menuDislpayerFalse()
    {
        Active = false;
        MenuCanvas.SetActive(false);
        playerControler.enabled = true;
        itemsPickUp.enabled = true;
        itemDisplay.enabled = true;
        displayNote.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion
}
