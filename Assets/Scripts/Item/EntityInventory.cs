using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.IO;

public class EntityInventory : MonoBehaviour
{
    public static EntityInventory instance;

    public ItemDataFile itemDataFiles;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    private int slotCnt;
    public List<Item> items = new List<Item>();
    public EntityInventoryData entityInventoryData;

    public int slotCount
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }

    void Start()
    {
        slotCount = 10;
    }


    public bool addItem(Item item, int count = 1)
    {
        int countLimit = item.countLimit;
        Item instanceItem = item;
        instanceItem.count = count;
        int quantity = count;
        int space = countListItems(instanceItem);
        int spaceCheckValue;

        if (instanceItem.type == ItemType.Equipment && items.Count < slotCount)
        {
            items.Add(instanceItem);

            if (onChangeItem != null)
            {
                onChangeItem.Invoke();
                GameManager.instance.isDataChange();
            }

            return true;
        }

        if (instanceItem.type == ItemType.Equipment)
        {
            GameManager.instance.isDataChange();
            return false;
        }

        if (items.Count <= slotCount && quantity <= findItemLeftSpace(instanceItem))
        {
            List<int> itemsIndex = getItemIndexByCodeAll(instanceItem);

            // 아이템이 존재하고 남은 공간에 아이템을 넣을 수 있는지
            for (int i = 0; i < itemsIndex.Count; i++)
            {
                if (items[itemsIndex[i]].count + quantity > 100)
                {
                    items[itemsIndex[i]].count = 100;
                    quantity = quantity - (100 - items[itemsIndex[i]].count);
                }
                else
                {
                    items[itemsIndex[i]].count += quantity;
                    quantity = 0;
                }
            }

            // 인벤토리 오류 임시 방편
            saveAndLoadEntityInventoryTemp();

            entityInventoryData.saveEntityInventorydata(items);
            entityInventoryData.loadEntityInventorydata();

            if (quantity == 0)
            {
                onChangeItem.Invoke();
                GameManager.instance.isDataChange();
                return true;
            }

        }

        if (space + quantity / countLimit + 1 == 0)
        {
            spaceCheckValue = 0;
        }
        else
        {
            spaceCheckValue = (space + quantity) / 101;
        }

        // 아이템이 존재하고 추가할 아이템의 일부를 기존아이템에 넣고 남는양을 새로운 슬롯에 넣을 수 있는지
        if (items.Count + spaceCheckValue < slotCount)
        {
            List<int> itemsIndex = getItemIndexByCodeAll(instanceItem);

            for (int i = 0; i < itemsIndex.Count; i++)
            {
                if (items[itemsIndex[i]].count + quantity > 100)
                {
                    quantity -= 100 - items[itemsIndex[i]].count;
                    items[itemsIndex[i]].count = 100;

                    // 인벤토리 오류 임시 방편
                    saveAndLoadEntityInventoryTemp();

                    entityInventoryData.saveEntityInventorydata(items);
                    entityInventoryData.loadEntityInventorydata();
                }
            }
        }
        instanceItem.count = quantity;

        // 기존에 아이템이 없고 아이템을 추가할 슬롯이 있는지
        if (instanceItem.count > 0 && items.Count < slotCount && instanceItem.count <= 100)
        {
            if (onChangeItem != null)
            {
                items.Add(instanceItem);

                // 인벤토리 오류 임시 방편
                saveAndLoadEntityInventoryTemp();

                entityInventoryData.saveEntityInventorydata(items);
                entityInventoryData.loadEntityInventorydata();

                onChangeItem.Invoke();
            }
            GameManager.instance.isDataChange();
            return true;
        }
        else
        {
            GameManager.instance.isDataChange();
            return false;
        }
    }

    public void removeItem(int slotNumber, int quantity = 1)
    {
        Item instanceItem = items[slotNumber];

        if (instanceItem.type == ItemType.Equipment)
        {
            items.RemoveAt(slotNumber);

            // 인벤토리 오류 임시 방편
            saveAndLoadEntityInventoryTemp();

            entityInventoryData.saveEntityInventorydata(items);
            entityInventoryData.loadEntityInventorydata();

            GameManager.instance.isDataChange();
            onChangeItem.Invoke();

            return;
        }

        if (quantity == instanceItem.count)
        {
            items.RemoveAt(slotNumber);

            // 인벤토리 오류 임시 방편
            saveAndLoadEntityInventoryTemp();

            entityInventoryData.saveEntityInventorydata(items);
            entityInventoryData.loadEntityInventorydata();

            GameManager.instance.isDataChange();
            onChangeItem.Invoke();

            return;
        }

        if (quantity <= findItemLeftSpace(instanceItem))
        {
            List<int> itemsIndex = getItemIndexByCodeAll(instanceItem);

            for (int i = itemsIndex.Count - 1; i >= 0; i--)
            {
                if (items[itemsIndex[i]].count - quantity < 0)
                {
                    quantity -= items[itemsIndex[i]].count;
                    items[itemsIndex[i]].count = 0;
                }
                else
                {
                    items[itemsIndex[i]].count -= quantity;
                    quantity = 0;
                }
            }
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].count <= 0)
            {
                items.RemoveAt(i);
            }
        }

        // 인벤토리 오류 임시 방편
        saveAndLoadEntityInventoryTemp();

        entityInventoryData.saveEntityInventorydata(items);
        entityInventoryData.loadEntityInventorydata();

        GameManager.instance.isDataChange();
        onChangeItem.Invoke();
    }

    public bool removeItem(Item item, int quantity = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (quantity > totalItemAmount(item))
            {
                return false;
            }

            if (items[i].code == item.code)
            {
                if (items[i].count >= quantity)
                {
                    if (items[i].count == quantity)
                    {
                        items.RemoveAt(i);

                        // 인벤토리 오류 임시 방편
                        saveAndLoadEntityInventoryTemp();

                        entityInventoryData.saveEntityInventorydata(items);
                        entityInventoryData.loadEntityInventorydata();

                        GameManager.instance.isDataChange();
                        onChangeItem.Invoke();
                        return true;
                    }

                    items[i].count -= quantity;

                    // 인벤토리 오류 임시 방편
                    saveAndLoadEntityInventoryTemp();

                    entityInventoryData.saveEntityInventorydata(items);
                    entityInventoryData.loadEntityInventorydata();

                    GameManager.instance.isDataChange();
                    onChangeItem.Invoke();
                    return true;
                }
                else
                {
                    quantity -= items[i].count;
                    items.RemoveAt(i);
                }

                if (quantity <= 0)
                {
                    // 인벤토리 오류 임시 방편
                    saveAndLoadEntityInventoryTemp();

                    entityInventoryData.saveEntityInventorydata(items);
                    entityInventoryData.loadEntityInventorydata();

                    GameManager.instance.isDataChange();
                    onChangeItem.Invoke();
                    return true;
                }
            }
        }

        // 인벤토리 오류 임시 방편
        saveAndLoadEntityInventoryTemp();

        entityInventoryData.saveEntityInventorydata(items);
        entityInventoryData.loadEntityInventorydata();

        GameManager.instance.isDataChange();
        onChangeItem.Invoke();

        return true;
    }

    public void removeAll()
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            removeItem(i, items[i].count);
        }
    }

    public List<Item> findItemByCodeAll(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].code == item.code)
            {
                items.Add(items[i]);
            }
        }
        return items.Count > 0 ? items : null;
    }

    public Item findItemByCode(int itemCode)
    {
        Item item = null;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].code == itemCode)
            {
                item = items[i];

                return item;
            }
        }

        return item;
    }

    public List<int> getItemIndexByCodeAll(Item item)
    {
        List<int> index = new List<int>();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].code == item.code)
            {
                index.Add(i);
            }
        }

        return index;
    }

    public int findItemLeftSpace(Item item)
    {
        int size = 0;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].code == item.code)
            {
                size += 100 - items[i].count;
            }
        }

        return size;
    }

    public int countListItems(Item item)
    {
        int value = 0;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].code == item.code)
            {
                value += 100 - items[i].count;
            }
        }

        return value;
    }

    public int totalItemAmount(Item item)
    {
        int value = 0;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].code == item.code)
            {
                value += items[i].count;
            }
        }

        return value;
    }

    public void showInventory()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log("아이템 슬롯 : " + i + "  아이템 이름 : " + items[i].itemName + "  아이템 개수 : " + items[i].count);
        }
    }

    public void saveAndLoadEntityInventoryTemp()
    {
        itemDataFiles.itemDatas = items;
        bool isMobile = GameManager.instance.isMobile;

        string jsonData = JsonUtility.ToJson(itemDataFiles, true);
        File.WriteAllText(saveOrLoad(isMobile, true, "entityInventoryItemsTemp"), jsonData);

        jsonData = File.ReadAllText(saveOrLoad(isMobile, false, "entityInventoryItemsTemp"));
        itemDataFiles = JsonUtility.FromJson<ItemDataFile>(jsonData);
        items = itemDataFiles.itemDatas;
    }

    public string saveOrLoad(bool isMobile, bool isSave, string fileName)
    {
        if (isSave)
        {
            if (isMobile)
            {
                // 모바일 저장
                return Path.Combine(Application.persistentDataPath, fileName + ".json");
            }
            else
            {
                // pc 저장
                return Path.Combine(Application.dataPath, fileName + ".json");
            }
        }
        else
        {
            if (isMobile)
            {
                // 모바일 로드
                return Path.Combine(Application.persistentDataPath, fileName + ".json");
            }
            else
            {
                // pc 로드
                return Path.Combine(Application.dataPath, fileName + ".json");
            }
        }
    }

    public void testPutItems()
    {
        for (int i = 0; i < 50; i++)
        {
            Item item = ItemDatabase.instance.itemDB[UnityEngine.Random.Range(0, 13)];

            if (item.type == ItemType.Equipment)
            {
                item.price = 100;
            }
            else if (item.type == ItemType.Consumable)
            {
                item.price = 20;
            }
            else if (item.type == ItemType.Fish)
            {
                item.price = 10;
            }
            else
            {
                item.price = 5;
            }

            instance.addItem(ItemDatabase.instance.makeItem(item));
        }
    }
}
