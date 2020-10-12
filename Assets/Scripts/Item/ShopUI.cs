using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public EntityInventory entityInventory;
    public PlayerInventory playerInventory;

    public GameObject shopSet;
    public GameObject shopNotice;
    public GameObject shopInformation;

    public ShopSlot[] slotsBuy;
    public ShopSlot[] slotsSell;
    public Transform slotHolderBuy;
    public Transform slotHolderSell;

    public GameObject buyUI;
    public GameObject sellUI;

    public Text money;

    void Start()
    {
        entityInventory = EntityInventory.instance;
        playerInventory = PlayerInventory.instance;

        slotHolderBuy = shopSet.transform.Find("Buy").GetChild(0).GetChild(0);
        slotHolderSell = shopSet.transform.Find("Sell").GetChild(0).GetChild(0);

        buyUI = shopSet.transform.Find("Buy").gameObject;
        sellUI = shopSet.transform.Find("Sell").gameObject;

        slotsBuy = slotHolderBuy.GetComponentsInChildren<ShopSlot>();
        slotsSell = slotHolderSell.GetComponentsInChildren<ShopSlot>();

        entityInventory.onSlotCountChange += slotChange;
        entityInventory.onChangeItem += redrawSlotUI;

        playerInventory.onSlotCountChange += slotChange;
        playerInventory.onChangeItem += redrawSlotUI;

        shopSet.SetActive(false);
        shopInformation.SetActive(false);

        shopNotice.gameObject.SetActive(false);
    }

    private void slotChange(int val)
    {
        //buy
        for (int i = 1; i < slotsBuy.Length; i++)
        {
            slotsBuy[i].slotNumber = i;

            if (i < entityInventory.slotCount)
            {
                slotsBuy[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                slotsBuy[i].GetComponent<Button>().interactable = false;
            }
        }
        //sell
        for (int i = 1; i < slotsSell.Length; i++)
        {
            slotsSell[i].slotNumber = i;

            if (i < playerInventory.slotCount)
            {
                slotsSell[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                slotsSell[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            uiOnOff();
        }
    }

    public void uiOnOff()
    {
        money.text = "" + GameManager.instance.playerData.money;

        if (shopSet.activeSelf)
        {
            shopSet.SetActive(false);
            shopInformation.SetActive(false);
            shopNotice.SetActive(false);

            return;
        }
        else
        {
            if (GetComponent<StatUI>().statSet.activeSelf)
            {
                GetComponent<StatUI>().statSet.SetActive(false);
            }
            if (GetComponent<EquipmentUI>().equipmentSet.activeSelf)
            {
                GetComponent<EquipmentUI>().equipmentSet.SetActive(false);
            }

            shopSet.SetActive(true);
            redrawSlotUI();

            buyUI.SetActive(false);
            sellUI.SetActive(false);

            shopNotice.SetActive(true);
        }
    }

    public void addSlot()
    {
        entityInventory.slotCount++;
    }

    void redrawSlotUI()
    {
        //buy
        for (int i = 0; i < slotsBuy.Length; i++)
        {
            slotsBuy[i].removeSlotUI();
        }
        for (int i = 0; i < entityInventory.items.Count; i++)
        {
            slotsBuy[i].item = entityInventory.items[i];
            slotsBuy[i].updateSlotUI();
        }

        //sell
        for (int i = 0; i < playerInventory.slotCount; i++)
        {
            slotsSell[i].removeSlotUI();
        }
        for (int i = 0; i < playerInventory.items.Count; i++)
        {
            slotsSell[i].item = playerInventory.items[i];
            slotsSell[i].updateSlotUI();
        }
    }

    public void testSetItemPowerUp()
    {
        entityInventory.items[0].power = 999;
    }

    public void openBuyUI()
    {
        buyUI.SetActive(true);
        sellUI.SetActive(false);
        shopNotice.gameObject.SetActive(false);

        GetComponent<ShopInformation>().buttonPurchase.SetActive(true);
        GetComponent<ShopInformation>().buttonSell.SetActive(false);
    }

    public void openSellUI()
    {
        buyUI.SetActive(false);
        sellUI.SetActive(true);
        shopNotice.gameObject.SetActive(false);

        GetComponent<ShopInformation>().buttonPurchase.SetActive(false);
        GetComponent<ShopInformation>().buttonSell.SetActive(true);
    }

    public void back()
    {
        buyUI.SetActive(false);
        sellUI.SetActive(false);
        shopNotice.gameObject.SetActive(true);
        ShopInformation.instance.offInformation();

        GetComponent<ShopInformation>().buttonPurchase.SetActive(false);
        GetComponent<ShopInformation>().buttonSell.SetActive(false);
    }

    public void shopNoticeOff()
    {
        shopNotice.gameObject.SetActive(false);
        shopSet.SetActive(false);
    }
}
