using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public ItemDataFile itemDataFile;

    public string spritePath = "Images/Items/Images";
    public string effectsPath = "Effects/";

    public Dictionary<int, Item> itemDatas = new Dictionary<int, Item>();

    private void Awake()
    {
        instance = this;
    }

    public List<Item> itemDB = new List<Item>();

    public GameObject entityItemPrefab;
    public Vector2[] pos;

    private void Start()
    {
        itemDataFile = new ItemDataFile();
        itemDataFile.itemDatas = new List<Item>();

        //saveItemData();
        loadItemData();

        //spawnItem();

        // 딕셔너리에 아이템 정보 입력
        for (int i = 0; i < itemDB.Count; i++)
        {
            itemDatas.Add(itemDB[i].code, itemDB[i]);
        }
    }

    public int findItemDBPositionByCode(int code)
    {
        for (int i = 0; i < itemDB.Count; i++)
        {
            if (itemDB[i].code == code)
            {
                return i;
            }
        }

        return -1;
    }

    public Item findItemByName(string name)
    {
        for (int i = 0; i < itemDB.Count; i++)
        {
            if (itemDB[i].itemName == name)
            {
                return itemDB[i];
            }
        }
        return null;
    }

/*    public Item findItemByCode(int code)
    {
        for (int i = 0; i < itemDB.Count; i++)
        {
            if (itemDB[i].code == code)
            {
                return itemDB[i];
            }
        }
        return null;
    }*/

    public Item findItemByCode(int code)
    {
        return itemDatas[code];
    }

    public Item pickRandomItem()
    {
        return itemDB[Random.Range(0, itemDB.Count)];
    }

    [ContextMenu("From Json Data")]
    public Sprite loadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    [ContextMenu("To Json Data")]
    public void saveItemData()
    {
        Debug.Log("저장 성공");
        itemDataFile.itemDatas = new List<Item>();

        itemDataFile.itemDatas.Add(new Item
            (1, 3000001, "나뭇가지", ItemType.Etc, EquipmentType.None, spritePath + "/" + 3000001, "평범", 0.1f, 100, 0, "불피우기에 유용한 잔가지이다."));

        string jsonData = JsonUtility.ToJson(itemDataFile, true);

        File.WriteAllText(saveOrLoad(false, true, "ItemData"), jsonData);
    }

    [ContextMenu("From Json Data")]
    public void loadItemData()
    {
        return;
        try
        {
            Debug.Log("아이템 정보 로드 성공");
            /*string jsonData = File.ReadAllText(saveOrLoad(false, false, "ItemData"));
            itemDataFile = JsonUtility.FromJson<ItemDataFile>(jsonData);*/

            itemDataFile = JsonUtility.FromJson<ItemDataFile>(Resources.Load<TextAsset>("ItemData").ToString());

            for (int i = 0; i < itemDataFile.itemDatas.Count; i++)
            {
                itemDataFile.itemDatas[i].sprite = loadSprite(itemDataFile.itemDatas[i].spritePath);
                itemDB.Add(itemDataFile.itemDatas[i]);
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("로드 오류");

            string jsonData = JsonUtility.ToJson(itemDataFile, true);

            File.WriteAllText(saveOrLoad(false, false, "ItemData"), jsonData);
            loadItemData();
        }
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

    public Item makeItem(Item item)
    {
        return new Item(item.count, item.code, item.itemName, item.type, item.equipmentType, item.spritePath, item.rating, item.weight, item.countLimit,
            item.price, item.itemInfo, item.itemEffect, item.size, item.levelLimit, item.reinforce, item.power, item.armor, item.accuracy, item.avoid,
            item.critRate, item.critDam, item.intellectPoint, item.wisdomPoint, item.dexterityPoint, item.concentrationPoint, item.healthPoint, item.manaPoint,
            item.expEff, item.powerReinforce, item.armorReinforce, item.accuracyReinforce, item.avoidReinforce, item.critRateReinforce, item.critDamReinforce,
            item.intellectPointReinforce, item.wisdomPointReinforce, item.dexterityPointReinforce, item.concentrationPointReinforce,
            item.healthPointReinforce, item.manaPointReinforce, item.expEffReinforce, item.effecienty, item.speed, item.luck, item.bonus, item.ability,
            item.effecientyReinforce, item.speedReinforce, item.luckReinforce, item.bonusReinforce, item.abilityReinforce, item.handType, item.spriteAnimatorPath);
    }
}

[System.Serializable]
public class ItemDataFile
{
    public List<Item> itemDatas;
}
