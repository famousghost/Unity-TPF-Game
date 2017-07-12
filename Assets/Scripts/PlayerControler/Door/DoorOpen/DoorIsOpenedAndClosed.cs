using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorIsOpenedAndClosed : MonoBehaviour {

    #region Variables
    [SerializeField]
    private GameObject thisDoor;

    [SerializeField]
    private Transform doorOpened;

    [SerializeField]
    private Transform doorClosed;

    [SerializeField]
    private bool doorIsOpened;

    [SerializeField]
    private float openingSpeed = 2.0f;
    #endregion

    #region Unity System Methods
    // Use this for initialization
    void Start () {
        doorIsOpened = false;
        thisDoor = this.gameObject;
        doorOpened = thisDoor.transform.FindChild("Position/opened").GetComponent<Transform>();
        doorClosed = thisDoor.transform.FindChild("Position/closed").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        ChangeDoorState();
	}
    #endregion

    #region Door Open and Close
    private void DoorOpen()
    {
        thisDoor.transform.FindChild("RoomDoor").position = Vector3.MoveTowards(thisDoor.transform.FindChild("RoomDoor").position, doorOpened.position, openingSpeed * Time.deltaTime);
    }

    private void DoorClose()
    {
        thisDoor.transform.FindChild("RoomDoor").position = Vector3.MoveTowards(thisDoor.transform.FindChild("RoomDoor").position, doorClosed.position, openingSpeed * Time.deltaTime);
    }

    private void ChangeDoorState()
    {
        if (doorIsOpened)
        {
            DoorOpen();
        }
        else
        {
            DoorClose();
        }
    }
    #endregion

    #region Setter
    public void SetDoorIsOpened()
    {
        this.doorIsOpened = !this.doorIsOpened;
    }
    #endregion

    #region Getter
    public bool GetDoorIsOpened()
    {
        return this.doorIsOpened;
    }
    #endregion


}
