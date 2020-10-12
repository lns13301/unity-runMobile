using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    public Item item;
    public int slotNumber;
    public Image itemIcon;
    public Text itemCount;

    public bool isSetSlot;

    // Start is called before the first frame update
    void Start()
    {
        itemIcon = transform.GetChild(0).GetComponent<Image>();
        itemCount = transform.GetChild(1).GetComponent<Text>();

        isSetSlot = false;
        setAlpha();
    }

    // Update is called once per frame
    void Update()
    {
        refreshData();
    }

    private void refreshData()
    {
        if (item == null || item.itemName.Length < 1)
        {
            return;
        }

        if (PlayerInventory.instance.findItemByCode(item.code) == null || PlayerInventory.instance.findItemByCode(item.code).itemName.Length < 1)
        {
            item = null;
            itemIcon.sprite = null;
            itemCount.text = "";

            isSetSlot = false;
            setAlpha();

            return;
        }

        item = PlayerInventory.instance.findItemByCode(item.code);
        itemIcon.color = new Color(1f, 1f, 1f, 1f);

        itemIcon.sprite = item.loadSprite(item.spritePath);
        itemCount.text = "" + item.count;
    }

    public void doAction()
    {
        refreshData();

        if (isSetSlot)
        {
            slotNumber = GameObject.Find("Canvas").GetComponent<ItemMenuSet>().slotNumber;
            item = PlayerInventory.instance.items[slotNumber];

            GameObject.Find("Canvas").GetComponent<InventoryUI>().uiOnOff();
            GameObject.Find("Canvas").GetComponent<ItemMenuSet>().isQuickSlotDataChanged = true;

            return;
        }

        if (PlayerInventory.instance.findItemByCode(item.code) == null)
        {
            clearSlot();
            return;
        }

        if (item.type == ItemType.Consumable || item.type == ItemType.Fish)
        {
            item.itemEffect.useItem();
            PlayerInventory.instance.removeItem(PlayerInventory.instance.findItemByCode(item.code));

            if (PlayerInventory.instance.findItemByCode(item.code) == null)
            {
                clearSlot();
                return;
            }

            item = PlayerInventory.instance.findItemByCode(item.code);
            refreshData();
        }

        GameObject.Find("Canvas").GetComponent<StatUI>().isDataChanged = true;
    }

    private void clearSlot()
    {
        itemIcon.sprite = null;
        item = null;
        itemCount.text = "";
        refreshData();

        GameObject.Find("Canvas").GetComponent<StatUI>().isDataChanged = true;

        isSetSlot = false;
        setAlpha();
    }

    public void setAlpha()
    {
        if (item == null || item.itemName.Length < 1)
        {
            itemIcon.color = new Color(1f, 1f, 1f, 0f);
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            //itemIcon.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
