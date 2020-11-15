using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;



public class ShopScrollElement : MonoBehaviour
{
    //public Button buttonComponent;
    
    public Text     name_text;
    public Text     desc_text;
    public Image    icon_image;
    public Image    pay_type_image;
    public Text     pay_value_text;
    
    public Button   get_button;


    private ShopScrollItem item;
    private ShopScrollList scrollList;

    // Use this for initialization
    void Start()
    {
        //buttonComponent.onClick.AddListener(HandleClick);
        //get_button.onClick.AddListener(onClick_get_button);
    }

    public void Setup(ShopScrollItem currentItem, ShopScrollList currentScrollList)
    {
        item = currentItem;
        scrollList = currentScrollList;

        name_text.text = item.name;
        desc_text.text = item.desc;

        //CGame.Instance.IconImage_set(icon_image, item.shop_index); //kdw
        TableInfo_shop table = CGameTable.Instance.Get_TableInfo_shop(item.shop_index);
        icon_image.sprite = Resources.Load<Sprite>(table.icon) as Sprite;

        //if (item.pay_type == "cash") CGame.Instance.IconImage_set(pay_type_image, (int)eItemCode.cash);
        //if (item.pay_type == "coin") CGame.Instance.IconImage_set(pay_type_image, (int)eItemCode.coin);
        pay_value_text.text = "" + item.pay_value;


        //int index = item.item_index;
        
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