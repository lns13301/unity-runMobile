using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class HeroSlot : MonoBehaviour, IPointerUpHandler
{
    public int slotNumber;
    public EntityData heroData;
    public Image heroDataIcon;
    public Text heroDataCount;

    private void Start()
    {
        // heroDataMenuSet = GameObject.Find("Canvas").GetComponent<heroDataMenuSet>();
    }

    public void updateSlotUI()
    {
        heroDataIcon.color = new Color(1, 1, 1, 1);
        heroDataIcon.sprite = heroData.spriteIcon;
        heroDataCount.text = "Lv. " + heroData.level;

        heroDataIcon.gameObject.SetActive(true);
        heroDataCount.gameObject.SetActive(true);
    }

    public void removeSlotUI()
    {
        try
        {
            heroDataIcon.color = new Color(1, 1, 1, 0);
            heroData = null;
            heroDataIcon.gameObject.SetActive(false);
            heroDataCount.gameObject.SetActive(false);
        }
        catch (Exception)
        {

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        HeroUI.instance.SelectHero(heroData);
    }

    /*public void heroDataMenuOnOff()
    {
        try
        {
            if (heroDataMenuSet.heroDataMenuSet.activeSelf || heroData == null || heroData.count < 1)
            {
                heroDataMenuSet.heroDataMenuSet.SetActive(false);
            }
            else
            {
                heroDataMenuSet.heroData = heroData;
                heroDataMenuSet.slotNumber = slotNumber;

                heroDataMenuSet.buttonsOnOff();
                heroDataMenuSet.setButton(heroData, new Vector2(transform.position.x + 290, transform.position.y - 340));
            }
        }
        catch (NullReferenceException)
        {
            Debug.Log("빈 아이템 슬롯");
        }
    }*/
}
