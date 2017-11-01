using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class ItemUse : MonoBehaviour, IPointerDownHandler
{
    //Item which could be helpfull to use an item
    #region Item Class
    [Header("Instance of item class")]
    [SerializeField]
    private Item item;

    [SerializeField]
    private ItemsPickUp itemsPickUp;
    #endregion

    //Start Method
    #region Unity System Methods
    // Use this for initialization
    void Start () {
        item = GetComponent<Item>();
        itemsPickUp = GameObject.Find("Player").GetComponent<ItemsPickUp>();
	}
    #endregion

    //Interaction with mouse
    #region On Pointer Down
    public void OnPointerDown(PointerEventData eventData)
    {
        if (item.CheckItemImage())
        {
            Debug.Log(this.gameObject.name + " Is Clicked Item");
            item.RemoveItem();
            itemsPickUp.SubtractCurrentNumberOfItem();
        }
        else
        {
            Debug.Log(this.gameObject.name + " There is no item");
        }
    }
    #endregion
}
