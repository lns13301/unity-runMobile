using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInformation : MonoBehaviour
{
    public static ShopInformation instance;

    public static string NEW_LINE = "\n";
    public Item item;
    public int slotNumber;

    public GameObject shopInformationUI;
    public Text itemName;
    public Text itemRating;
    public Text itemStats;
    public Text itemContents;
    public Text itemLeftCount;
    public Text itemPrice;
    public Text itemSellPrice;

    public GameObject infoTag;
    public int optionSize;
    public RectTransform shopInformationUIRect;

    public GameObject notice;

    public GameObject buttonPurchase;
    public GameObject buttonSell;

    private void Start()
    {
        instance = this;
        noticeOff();
    }

    public void setItem(Item item, int slotNumber)
    {
        this.item = item;
        this.slotNumber = slotNumber;
    }

    public void showInformation()
    {
        if (item == null || item.itemName.Length < 1)
        {
            return;
        }

        shopInformationUI.SetActive(true);

        itemName.text = item.itemName;

        itemRating.text = "등급 : " + item.rating;
        itemContents.text = item.itemInfo;

        if (item.type != ItemType.Equipment)
        {
            itemLeftCount.text = "수량 : " + item.count + "개";
        }
        else
        {
            itemLeftCount.text = "";
        }

        if (item.price == 0)
        {

            if (GetComponent<ShopUI>().buyUI.activeSelf) 
            {
                itemPrice.text = item.price + " Isle";
            }
            else
            {
                itemSellPrice.text = item.price + " Isle";
            }
        }
        else
        {
            if (GetComponent<ShopUI>().buyUI.activeSelf)
            {
                itemPrice.text = Calculator.numberToFormatting(item.price) + " Isle";
            }
            else
            {
                itemSellPrice.text = Calculator.numberToFormatting(item.price) + " Isle";
            }
        }
               
        if (item.type == ItemType.Fish)
        {
            itemRating.text = "등급 : " + item.rating + "    길이 : " + item.size + "Cm";
            itemContents.text = item.itemInfo;
        }

        if (item.type == ItemType.Equipment)
        {
            if (item.reinforce > 0)
            {
                itemName.text += "  ( +" + item.reinforce + ")";
            }

            itemRating.text += "         타입 : " + PlayerEquipment.instance.getEquipmentTypeName(item.equipmentType);
            //itemStats.text = "================ 아이템 정보 ==============" + NEW_LINE;
            infoTag.SetActive(true);
            optionSize = 0;

            if (item.levelLimit != 0)
            {
                itemStats.text = "레벨제한 : " + item.levelLimit + NEW_LINE;
                optionSize++;
            }
            if (item.power != 0)
            {
                itemStats.text += "공격력 : " + (item.power + item.powerReinforce) + "( + " + item.powerReinforce + ")" + NEW_LINE;
                optionSize++;
            }
            if (item.armor != 0)
            {
                itemStats.text += "방어력 : " + (item.armor + item.armorReinforce) + "( + " + item.armorReinforce + ")" + NEW_LINE;
                optionSize++;
            }
            if (item.accuracy != 0)
            {
                itemStats.text += "명중률 : " + (item.accuracy + item.accuracyReinforce) + "( + " + item.accuracyReinforce + ")" + NEW_LINE;
                optionSize++;
            }
            if (item.avoid != 0)
            {
                itemStats.text += "회피율 : " + (item.avoid + item.avoidReinforce) + "( + " + item.avoidReinforce + ")" + NEW_LINE;
                optionSize++;
            }
            if (item.critRate != 0)
            {
                itemStats.text += "치명율 : " + Mathf.Round((item.critRate + item.critRateReinforce) * 10) / 10 + "%" + "( + " + Mathf.Round(item.critRateReinforce * 10) / 10 + "%)" + NEW_LINE;
                optionSize++;
            }
            if (item.critDam != 0)
            {
                itemStats.text += "치명피해 : " + Mathf.Round((item.critDam + item.critDamReinforce) * 10) / 10 + "%" + "( + " + Mathf.Round(item.critDamReinforce * 10) / 10 + "%)" + NEW_LINE;
                optionSize++;
            }
            if (item.intellectPoint != 0)
            {
                itemStats.text += "지력 : " + (item.intellectPoint + item.intellectPointReinforce) + "( + " + item.intellectPointReinforce + ")" + NEW_LINE;
                optionSize++;
            }
            if (item.wisdomPoint != 0)
            {
                itemStats.text += "지혜 : " + (item.wisdomPoint + item.wisdomPointReinforce) + "( + " + item.wisdomPointReinforce + ")" + NEW_LINE;
                optionSize++;
            }
            if (item.dexterityPoint != 0)
            {
                itemStats.text += "순발력 : " + (item.dexterityPoint + item.dexterityPointReinforce) + "( + " + item.dexterityPointReinforce + ")" + NEW_LINE;
                optionSize++;
            }
            if (item.concentrationPoint != 0)
            {
                itemStats.text += "집중력 : " + (item.concentrationPoint + item.concentrationPointReinforce) + "( + " + item.concentrationPointReinforce + ")" + NEW_LINE;
                optionSize++;
            }
            if (item.healthPoint != 0)
            {
                itemStats.text += "체력 : " + (item.healthPoint + item.healthPointReinforce) + "( + " + item.healthPointReinforce + ")" + NEW_LINE;
                optionSize++;
            }
            if (item.manaPoint != 0)
            {
                itemStats.text += "마력 : " + (item.manaPoint + item.manaPointReinforce) + "( + " + item.manaPointReinforce + ")" + NEW_LINE;
                optionSize++;
            }
            if (item.expEff != 0)
            {
                itemStats.text += "경험치 보너스 : " + Mathf.Round((item.expEff + item.expEffReinforce) * 10) / 10 + "%" + "( + " + Mathf.Round(item.expEffReinforce * 10) / 10 + "%)" + NEW_LINE;
                optionSize++;
            }

            shopInformationUIRect.sizeDelta = new Vector2(800, 580 + optionSize * 30);
        }
        else if (item.type == ItemType.Consumable || item.type == ItemType.Fish)
        {
            //itemStats.text = "================ 아이템 정보 ==============" + NEW_LINE;
            infoTag.SetActive(true);
            optionSize = 0;

            itemStats.text = "";

            if (item.itemEffect.healthPoint != 0)
            {
                itemStats.text += "체력 : " + item.itemEffect.healthPoint + NEW_LINE;
                optionSize++;
            }
            if (item.itemEffect.manaPoint != 0)
            {
                itemStats.text += "마력 : " + item.itemEffect.manaPoint + NEW_LINE;
                optionSize++;
            }

            shopInformationUIRect.sizeDelta = new Vector2(800, 580 + optionSize * 30);
        }
        else
        {
            infoTag.SetActive(false);
            itemStats.text = null;
            shopInformationUIRect.sizeDelta = new Vector2(800, 400);
        }
    }

    public void offInformation()
    {
        shopInformationUI.SetActive(false);
    }

    public void purchaseItem()
    {
        int purchaseCount = TouchPad.instance.getNumber();

        if (purchaseCount == 0)
        {
            return;
        }

        // 플레이어 인벤토리에 아이템 추가할 수 있는지 검사할 때 아이템 수량 주소 참조 값이 변해버리므로 임시저장용 변수
        int tempCount = item.count;

        if (GameManager.instance.playerData.money >= item.price * purchaseCount)
        {
            if (PlayerInventory.instance.addItem(item, purchaseCount))
            {
                item.count = tempCount;
                GameManager.instance.playerData.money -= item.price * purchaseCount;
                Debug.Log((EntityInventory.instance.items[slotNumber].count));
                EntityInventory.instance.removeItem(EntityInventory.instance.items[slotNumber], purchaseCount);

                GameObject.Find("Canvas").GetComponent<ShopUI>().money.text = "" + GameManager.instance.playerData.money;
                offInformation();

                return;
            }

            item.count = tempCount;
        }

        notice.SetActive(true);
    }

    public void sellItem()
    {
        int sellCount = TouchPad.instance.getNumber();

        if (sellCount == 0)
        {
            return;
        }

        int tempCount = item.count;

        if (PlayerInventory.instance.removeItem(item, sellCount))
        {
            item.count = tempCount;
            GameManager.instance.playerData.money += item.price * sellCount;

            GameObject.Find("Canvas").GetComponent<ShopUI>().money.text = "" + GameManager.instance.playerData.money;
            offInformation();
            return;
        }

        item.count = tempCount;
    }

    public void noticeOff()
    {
        notice.SetActive(false);
    }
}
