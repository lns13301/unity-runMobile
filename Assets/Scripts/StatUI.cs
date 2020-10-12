using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StatUI : MonoBehaviour
{
    public string NEW_LINE = "\n";
    public GameObject statSet;
    public StatUI instance;

    public Image[] statPointUp;

    public Text nameText;
    public Text typeText;
    public Text levelText;
    public Text jobText;
    public Text moneyText;
    public Text beneficenceScoreText;

    public Text hpText;
    public Text mpText;
    public Text expText;

    public Text powerText;
    public Text armorText;
    public Text magicPowerText;
    public Text magicArmorText;
    public Text critRateText;
    public Text critDamText;
    public Text accuracyText;
    public Text avoidText;

    public Text statPointText;
    public Text intellectText;
    public Text wisdomText;
    public Text dexterityText;
    public Text concentrationText;

    public Text expEffText;

    public Text fameTextText;
    public Text charmTextText;

    // 추후 delegate로 변경해야함
    public bool isDataChanged;

    public bool isUIOn;

    // Start is called before the first frame update
    void Start()
    {
        isDataChanged = true;

        instance = this;

        if (statSet.activeSelf)
        {
            statSet.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            uiOnOff();
        }
    }

    private void FixedUpdate()
    {
        if (isDataChanged)
        {
            refresh();
        }
        // refresh();
    }

    public void uiOnOff()
    {
        SoundManager.instance.PlayButtonEffectSound();

        if (statSet.activeSelf)
        {
            statSet.SetActive(false);
        }
        else
        {
            if (GetComponent<InventoryUI>().inventorySet.activeSelf)
            {
                GetComponent<InventoryUI>().inventorySet.SetActive(false);
            }
            if (GetComponent<QuestUI>().questSet.activeSelf)
            {
                GetComponent<QuestUI>().questSet.SetActive(false);
            }
            if (GetComponent<SkillUI>().skillSet.activeSelf)
            {
                GetComponent<SkillUI>().skillSet.SetActive(false);
            }

            refresh();
            statSet.SetActive(true);
        }
    }

    public void setPlayerEquipedTool()
    {
        // 빈 아이템일 경우 Null로 처리
        try
        {
            if (GameManager.instance.playerEquipment.items[2] != null && GameManager.instance.playerEquipment.items[2].itemName.Length > 0)
            {
                GameManager.instance.isEquipedTool = true;
            }
            else
            {
                GameManager.instance.isEquipedTool = false;
            }

            int dummy = GameManager.instance.playerEquipment.items[2].itemName.Length;
        }
        catch
        {
            PlayerEquipment.instance.removeItem(EquipmentType.RightHand);
            GameManager.instance.playerEquipment.items[2] = null;
        }
    }

/*    private void updateQuickSlot()
    {
        for (int i = 0; i < GetComponent<ItemMenuSet>().quickSlot.Length; i++)
        {
            if (GetComponent<ItemMenuSet>().quickSlot[i].item != null && GetComponent<ItemMenuSet>().quickSlot[i].item.count > 1)
            {
                GetComponent<ItemMenuSet>().quickSlot[i].isDataChanged = true;
            }
        }
    }*/

    public void refresh()
    {
        try
        {
            isDataChanged = false;

            nameText.text = "닉네임 : " + GameManager.instance.playerData.name;
            typeText.text = "종 족 : " + GameManager.instance.playerData.getElementName() + "종족";
            levelText.text = "레 벨 : " + GameManager.instance.playerData.level;
            jobText.text = "직 업 : " + GameManager.instance.playerData.getJobName();
            moneyText.text = "보유 핀 : " + GameManager.instance.playerData.money;
            beneficenceScoreText.text = "선행 점수 : " + GameManager.instance.playerData.fame;

            hpText.text = GameManager.instance.playerData.healthPoint + " / " + GameManager.instance.playerData.healthPointMax;
            mpText.text = GameManager.instance.playerData.manaPoint + " / " + GameManager.instance.playerData.manaPointMax;
            expText.text = GameManager.instance.playerData.exp + " / " + GameManager.instance.playerData.nextExp;
            powerText.text = "" + GameManager.instance.playerData.power;
            armorText.text = "" + GameManager.instance.playerData.armor;
            magicPowerText.text = "" + GameManager.instance.playerData.power;
            magicArmorText.text = "" + GameManager.instance.playerData.armor;
            accuracyText.text = "" + GameManager.instance.playerData.accuracy;
            avoidText.text = "" + GameManager.instance.playerData.avoid;
            critRateText.text = "" + Mathf.Round(GameManager.instance.playerData.critRate * 10) / 10 + "%";
            critDamText.text = "" + Mathf.Round(GameManager.instance.playerData.critDam * 10) / 10 + "%";
        }
        catch (NullReferenceException)
        {
            Debug.Log("스탯 텍스트 오류 발생");
        }
    }
}
