using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    public HeroInventory heroInventory;
    public GameObject heroSet;

    public HeroSlot[] slots;
    public Transform slotHolder;

    // public Animator buttonMenuAnimator;

    void Start()
    {
        heroInventory = HeroInventory.instance;
        slots = slotHolder.GetComponentsInChildren<HeroSlot>();
        heroInventory.onHeroSlotCountChange += slotChange;
        heroInventory.onChangeHeroData += redrawSlotUI;

        heroSet.SetActive(false);
    }

    private void slotChange(int val)
    {
        Debug.Log("슬롯 카운트 변경");
        for (int i = 1; i < slots.Length; i++)
        {
            slots[i].slotNumber = i;

            if (i < heroInventory.slotCount)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
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
        SoundManager.instance.PlayButtonEffectSound();

        /*        if (GameObject.Find("Canvas").GetComponent<ItemMenuSet>().isReinforceProgressing)
                {
                    return;
                }
                GameManager.instance.saveAndLoadheroInventoryTemp();
                GameManager.instance.saveAndLoadPlayerEquipmentTemp();

                itemMenuSet.SetActive(false);*/

/*        if (inventorySet.activeSelf)
        {
            inventorySet.SetActive(false);
        }
        else
        {
            inventorySet.SetActive(true);
        }*/
    }

    public void addSlot()
    {
        if (GameManager.instance.playerData.inventorySize >= 25)
        {
            Debug.Log("더이상 늘릴 수 없습니다.");
            return;
        }

        heroInventory.slotCount++;
    }

    void redrawSlotUI()
    {
        Debug.Log("다시 그림, 히어로 카운트 : " + GameManager.instance.playerData.heroDatas.Count);
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].removeSlotUI();
        }
        for (int i = 0; i < GameManager.instance.playerData.heroDatas.Count; i++)
        {
            Debug.Log("동작함");
            slots[i].heroData = GameManager.instance.playerData.heroDatas[i];
            slots[i].updateSlotUI();
        }
    }
}
