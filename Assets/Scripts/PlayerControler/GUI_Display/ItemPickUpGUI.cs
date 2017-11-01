using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class ItemPickUpGUI : MonoBehaviour {

    [SerializeField]
    private Camera playerView;

    [SerializeField]
    private Texture2D pickUpHand;

    [SerializeField]
    private const float MAXDISANCEOFRAY = 1.4f;

	// Use this for initialization
	void Start () {
        playerView = GetComponentInChildren<Camera>();
        pickUpHand = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Resources/UseImages/pickUpHand.png", typeof(Texture2D));
    }

    void OnGUI()
    {
        Ray ray = new Ray(playerView.transform.position, playerView.transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MAXDISANCEOFRAY))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                GUI.DrawTexture(new Rect((Screen.width / 2) - 25, (Screen.height / 2) - 25, 50, 50), pickUpHand);
            }
        }
            
    }
}
