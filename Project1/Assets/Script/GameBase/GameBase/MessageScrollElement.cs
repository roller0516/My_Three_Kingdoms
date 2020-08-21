using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;



public class MessageScrollElement : MonoBehaviour
{

    //public Button buttonComponent;

    public Text message_text;
    public Text time_text;
    public Image icon_image;
    public Button get_button;


    private MessageScrollItem item;
    private MessageScrollList scrollList;

    // Use this for initialization
    void Start()
    {
        //buttonComponent.onClick.AddListener(HandleClick);
        //get_button.onClick.AddListener(onClick_get_button);
    }

    public void Setup(MessageScrollItem currentItem, MessageScrollList currentScrollList)
    {
        item = currentItem;
        scrollList = currentScrollList;

        message_text.text = item.message;
        time_text.text = "" + item.time;
        get_button.onClick.AddListener(onClick_get_button);

        
        //int index = item.item_index;
        //CGame.Instance.IconImage_set( icon_image, index); //kdw
    }

    public void HandleClick()
    {
        print("click");
        //scrollList.TryTransferItemToOtherShop(item);
    }

    public void onClick_get_button()
    {
        print("click get " + item.uid);
        
        scrollList.OnEventCallback(item.uid, "");

    }
}