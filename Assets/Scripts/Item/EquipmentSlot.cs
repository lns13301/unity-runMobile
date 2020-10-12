using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IPointerUpHandler
{
    public Item item;
    public Image itemIcon;
    public Text grade;
    public Text reinforce;
    public EquipmentType slotType;

    public void updateSlotUI()
    {
        if (item != null && item.itemName.Length > 0)
        {
            itemIcon.color = new Color(1, 1, 1, 1);
            itemIcon.sprite = item.sprite;
            reinforce.text = "+ " + item.reinforce;
            grade.text = "" + item.rating;
        }
        else
        {
            itemIcon.sprite = null;
            reinforce.text = "";
            grade.text = "";
        }

        if (item != null && item.reinforce < 1)
        {
            reinforce.text = "";
        }

        itemIcon.gameObject.SetActive(true);
        reinforce.gameObject.SetActive(true);
    }

    public void removeSlotUI()
    {
        if (item != null && item.itemName.Length > 0)
        {
            itemIcon.color = new Color(1, 1, 1, 0);
        }

        item = null;
        itemIcon.gameObject.SetActive(false);
        reinforce.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerEquipment.instance.removeItem(slotType);
    }
}
