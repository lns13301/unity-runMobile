using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class HeroInventory : MonoBehaviour
{
    public static HeroInventory instance;
    public PlayerData playerData;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public delegate void OnHeroSlotCountChange(int val);
    public OnHeroSlotCountChange onHeroSlotCountChange;

    public delegate void OnChangeHeroData();
    public OnChangeHeroData onChangeHeroData;

    private int slotCnt;
    public List<EntityData> heroDatas = new List<EntityData>();

    public int slotCount
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onHeroSlotCountChange.Invoke(slotCnt);
            playerData.inventorySize = slotCnt;
        }
    }

    void Start()
    {
        playerData = GameManager.instance.playerData;
        heroDatas = playerData.heroDatas;
        slotCount = playerData.inventorySize;
        Debug.Log("슬롯크기 : " + slotCount);
    }

    public bool AddHero(EntityData heroData)
    {
        if (heroDatas.Count < slotCount)
        {
            Debug.Log("영웅 추가 성공");
            heroDatas.Add(heroData);

            if (onChangeHeroData != null)
            {
                onChangeHeroData.Invoke();
                GetComponent<GameManager>().isDataChange();
            }

            return true;
        }
        Debug.Log("영웅 추가 실패");
        return false;
    }

    public void RemoveItem(int slotNumber)
    {
        heroDatas.RemoveAt(slotNumber);

        // 인벤토리 오류 임시 방편
        //GameManager.instance.saveAndLoadHeroInventoryTemp();

        GameManager.instance.isDataChange();
        onChangeHeroData.Invoke();
    }

    public EntityData FindHeroByCodeAll(EntityData heroData)
    {
        for (int i = 0; i < heroDatas.Count; i++)
        {
            if (heroDatas[i].code == heroData.code)
            {
                return heroDatas[i];
            }
        }

        return null;
    }

    public int GetHeroIndexByCodeAll(EntityData heroData)
    {
        List<int> index = new List<int>();

        for (int i = 0; i < heroDatas.Count; i++)
        {
            if (heroDatas[i].code == heroData.code)
            {
                return i;
            }
        }

        return -1;
    }

    public void ShowHeroInventory()
    {
        for (int i = 0; i < heroDatas.Count; i++)
        {
            Debug.Log("아이템 슬롯 : " + i + "  아이템 이름 : " + heroDatas[i].entityName);
        }
    }
}
