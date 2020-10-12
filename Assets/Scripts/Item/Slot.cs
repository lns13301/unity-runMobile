using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Slot : MonoBehaviour, IPointerUpHandler
{
    public int slotNumber;
    public Item item;
    public Image itemIcon;
    public Text itemCount;
    public ItemMenuSet itemMenuSet;

    private void Start()
    {
        itemMenuSet = GameObject.Find("Canvas").GetComponent<ItemMenuSet>();
    }

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
        itemMenuOnOff();
        itemMenuSet.isPlayerInventory = true;
    }

    public void itemMenuOnOff()
    {
        try
        {
            if (itemMenuSet.itemMenuSet.activeSelf || item == null || item.count < 1)
            {
                itemMenuSet.itemMenuSet.SetActive(false);
            }
            else
            {
                itemMenuSet.item = item;
                itemMenuSet.slotNumber = slotNumber;

                itemMenuSet.buttonsOnOff();
                itemMenuSet.setButton(item, new Vector2(transform.position.x + 290, transform.position.y - 340));
            }
        }
        catch (NullReferenceException)
        {
            Debug.Log("빈 아이템 슬롯");
        }
    }
}
