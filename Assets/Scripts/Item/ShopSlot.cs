using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerUpHandler
{
    public int slotNumber;
    public Item item;
    public Image itemIcon;
    public Text itemCount;

    public void updateSlotUI()
    {
        itemIcon.color = new Color(1, 1, 1, 1);
        itemIcon.sprite = item.sprite;
        itemCount.text = "" + item.count;

        if (item.count < 2)
        {
            itemCount.text = "";
        }
        itemIcon.gameObject.SetActive(true);
        itemCount.gameObject.SetActive(true);
    }

    public void removeSlotUI()
    {
        itemIcon.color = new Color(1, 1, 1, 0);
        item = null;
        itemIcon.gameObject.SetActive(false);
        itemCount.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ShopInformation.instance.setItem(item, slotNumber);
        ShopInformation.instance.showInformation();
        TouchPad.instance.touchPanelNumberString = "0";
        TouchPad.instance.showPanelNumber();
        TouchPad.instance.addOne();
    }
}
