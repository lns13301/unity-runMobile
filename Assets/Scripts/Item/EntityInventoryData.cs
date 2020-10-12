using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EntityInventoryData : MonoBehaviour
{
    public List<Item> items;
    public int id;
    public bool isSavedInventory;

    public ItemDataFile itemDataFiles;
    public int slotCount;

    // Start is called before the first frame update
    void Start()
    {
        if (isSavedInventory)
        {
            loadEntityInventorydata();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveEntityInventorydata()
    {
        id = transform.gameObject.GetComponent<ObjectData>().id;
        itemDataFiles.itemDatas = items;
        bool isMobile = GameManager.instance.isMobile;

        string jsonData = JsonUtility.ToJson(itemDataFiles, true);
        File.WriteAllText(saveOrLoad(isMobile, true, "InventoryData_" + id), jsonData);
    }

    public void saveEntityInventorydata(List<Item> items)
    {
        id = transform.gameObject.GetComponent<ObjectData>().id;
        itemDataFiles.itemDatas = items;
        bool isMobile = GameManager.instance.isMobile;

        string jsonData = JsonUtility.ToJson(itemDataFiles, true);
        File.WriteAllText(saveOrLoad(isMobile, true, "InventoryData_" + id), jsonData);
    }

    public void loadEntityInventorydata()
    {
        if (!isSavedInventory)
        {
            return;
        }

        try
        {
            bool isMobile = GameManager.instance.isMobile;

            string jsonData = File.ReadAllText(saveOrLoad(isMobile, false, "InventoryData_" + id));
            itemDataFiles = JsonUtility.FromJson<ItemDataFile>(jsonData);
            items = itemDataFiles.itemDatas;
        }
        catch (FileNotFoundException)
        {

        }
    }

    public void setEntityInventoryDataToEntityInventory()
    {
        EntityInventory.instance.entityInventoryData = this;
        EntityInventory.instance.items = items;
        EntityInventory.instance.slotCount = slotCount;
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
}
