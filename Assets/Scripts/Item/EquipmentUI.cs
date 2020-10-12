using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EquipmentUI : MonoBehaviour
{
    private PlayerEquipment playerEquipment;
    public GameObject equipmentSet;

    public EquipmentSlot[] slots;
    public Transform slotHolder;

    public Animator buttonMenuAnimator;

    void Start()
    {
        playerEquipment = PlayerEquipment.instance;
        slots = slotHolder.GetComponentsInChildren<EquipmentSlot>();
        playerEquipment.onChangeItem += redrawSlotUI;
        equipmentSet.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            uiOnOff();
        }
    }

    public void uiOnOff()
    {
        if (equipmentSet.activeSelf)
        {
            equipmentSet.SetActive(false);
        }
        else
        {
            if (GetComponent<InventoryUI>().inventorySet.activeSelf)
            {
                GetComponent<InventoryUI>().inventorySet.SetActive(false);
            }
            if (GetComponent<StatUI>().statSet.activeSelf)
            {
                GetComponent<StatUI>().statSet.SetActive(false);
            }
            equipmentSet.SetActive(true);
            buttonMenuAnimator.SetBool("isUIOn", true);
        }
    }

    void redrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].removeSlotUI();
        }
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].item = playerEquipment.items[i];
            slots[i].updateSlotUI();
            GameObject.Find("Canvas").GetComponent<StatUI>().isDataChanged = true;
        }
    }
}
