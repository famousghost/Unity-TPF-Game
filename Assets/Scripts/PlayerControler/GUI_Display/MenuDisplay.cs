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

<<<<<<< HEAD
=======
    [SerializeField]
    private DisplayNote displayNote;
>>>>>>> dd15de67da3581d7b2366198ae7faee105c944b1
    #endregion

    #region Active Bool
    private bool Active = false;
    #endregion

    #region Start (Canvas Off)
    // Use this for initialization
    void Start () {
<<<<<<< HEAD
=======
        displayNote = GetComponent<DisplayNote>();
>>>>>>> dd15de67da3581d7b2366198ae7faee105c944b1
        MenuCanvas.SetActive(false);
    }
    #endregion

    #region Update
    // Update is called once per frame
    void Update () {
<<<<<<< HEAD
		if(Input.GetKeyDown(KeyCode.Escape) && Active == false)
        {
            menuDislpayerFalse();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && Active == true)
        {
            menuDislpayerTrue();
=======
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
>>>>>>> dd15de67da3581d7b2366198ae7faee105c944b1
        }
	}
    #endregion

    #region Option Activating Function
<<<<<<< HEAD
    void menuDislpayerFalse()
    {
        Active = true;
        MenuCanvas.SetActive(true);
        playerControler.enabled = false;
        itemsPickUp.enabled = false;
        itemDisplay.enabled = false;
=======
    void menuDislpayerTrue()
    {
        Active = true;
        MenuCanvas.SetActive(true);
        displayNote.SetNoteActive(false);
        playerControler.enabled = false;
        itemsPickUp.enabled = false;
        itemDisplay.enabled = false;
        displayNote.enabled = false;
>>>>>>> dd15de67da3581d7b2366198ae7faee105c944b1
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

<<<<<<< HEAD
    void menuDislpayerTrue()
=======
    void menuDislpayerFalse()
>>>>>>> dd15de67da3581d7b2366198ae7faee105c944b1
    {
        Active = false;
        MenuCanvas.SetActive(false);
        playerControler.enabled = true;
        itemsPickUp.enabled = true;
        itemDisplay.enabled = true;
<<<<<<< HEAD
=======
        displayNote.enabled = true;
>>>>>>> dd15de67da3581d7b2366198ae7faee105c944b1
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion
}
