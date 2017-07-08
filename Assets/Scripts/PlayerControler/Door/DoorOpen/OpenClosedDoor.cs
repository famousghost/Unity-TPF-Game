using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClosedDoor : MonoBehaviour {

    #region Variables
    [Header("Camera")]
    [SerializeField]
    private Camera playerView;

    [Header("DoorIsOpenClass")]
    [SerializeField]
    private DoorIsOpenedAndClosed currentDoorTransform;

    [Header("Texture2d Variables")]
    [SerializeField]
    private Texture2D openDoorImage;

    [SerializeField]
    private const float MAXDISTANCEOFRAY = 1.4f;

    [Header("Key To Open")]
    [SerializeField]
    private KeyCode leftMouseKeyDown;
    #endregion

    #region Unity System Methods
    // Use this for initialization
    void Start () {
        playerView = GetComponentInChildren<Camera>();
        openDoorImage = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Resources/UseImages/ButtonClickHand.png", typeof(Texture2D));
        leftMouseKeyDown = KeyCode.Mouse0;
    }
	
	// Update is called once per frame
	void Update () {
        RayToDoor();
    }
    #endregion

    #region Ray to Doors
    private void RayToDoor()
    {
        Ray ray = new Ray(playerView.transform.position, playerView.transform.forward);
        RaycastHit hit;
        
        if(Physics.Raycast(ray,out hit,MAXDISTANCEOFRAY))
        {
            if (Input.GetKeyDown(leftMouseKeyDown))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Doors"))
                {
                    currentDoorTransform = hit.collider.gameObject.GetComponent<DoorIsOpenedAndClosed>();
                    currentDoorTransform.SetDoorIsOpened();
                    Debug.Log("udalo sie drzwi");
                }
            }
        }
    }
    #endregion

    #region GUI
    void OnGUI()
    {
        Ray ray = new Ray(playerView.transform.position, playerView.transform.forward);
        Debug.DrawRay(playerView.transform.position, playerView.transform.forward * 30, Color.blue);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MAXDISTANCEOFRAY))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Doors"))
            {
                GUI.DrawTexture(new Rect(Screen.width / 2 - 10.0f, Screen.height / 2 - 10, 30, 30), openDoorImage);
            }
        }
    }
    #endregion
}
