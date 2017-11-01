using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureDoorOpen : MonoBehaviour {

    [SerializeField]
    private Animator doorAnimator;

    [SerializeField]
    private Camera mainCamera;

    #region Textures
    [Header("Texture2d Variables")]
    [SerializeField]
    private Texture2D openDoorImage;
    #endregion

    [SerializeField]
    private bool isOpen = false;

    [SerializeField]
    private const float MAXDISTANCE = 1.4f;

    // Use this for initialization
    void Start () {
        mainCamera = GetComponent<Camera>();
        //openDoorImage = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Resources/UseImages/DoorOpen.png", typeof(Texture2D));
    }
	
	// Update is called once per frame
	void Update () {
        RayToDoor();
        
    }

    private void RayToDoor()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * 30, Color.blue);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MAXDISTANCE))
        {

             if (hit.collider.gameObject.layer == LayerMask.NameToLayer("futureDoor"))
             {
                doorAnimator = hit.collider.gameObject.GetComponent<Animator>();
                  if (Input.GetKeyDown(KeyCode.Mouse0))
                  {
                      isOpen = !isOpen;
                      doorAnimator.SetBool("IsOpen", isOpen);
                }
             }
        }
    }

    void OnGUI()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * 30, Color.blue);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MAXDISTANCE))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("futureDoor"))
            {
                GUI.DrawTexture(new Rect(Screen.width / 2 - 10.0f, Screen.height / 2 - 10, 50, 50), openDoorImage);
            }
        }
    }
}
