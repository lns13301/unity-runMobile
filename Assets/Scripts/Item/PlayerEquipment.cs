using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public static PlayerEquipment instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public int slotCnt = 10;
    public Item[] items = new Item[12];
    public Item previousItem;

    // equipment total
    public int reinforce;

    public int power;
    public int armor;
    public int accuracy;
    public int avoid;
    public float critRate;
    public float critDam;

    public float healthPoint;
    public float manaPoint;

    public float intellect;
    public float wisdom;
    public float dexterity;
    public float concentration;

    public float expEff;

    private void Start()
    {
        previousItem = null;
    }

    /*private void registerMotion()
    {
        // 장비모션등록

        if (GameManager.instance.GetComponent<PlayerEquipment>().items[1] != null)
        {
            for (int i = 0; i < PlayerManager.instance.leftHands.Length; i++)
            {
                if (PlayerManager.instance.leftHands[i].name == ("" + GameManager.instance.GetComponent<PlayerEquipment>().items[1].code))
                {
                    PlayerManager.instance.leftHands[i].SetActive(true);
                    PlayerManager.instance.activeLeftHand = i;
                    break;
                }
                else
                {
                    PlayerManager.instance.leftHands[i].SetActive(false);
                    PlayerManager.instance.activeLeftHand = -1;
                }
            }

        }
        if (GameManager.instance.GetComponent<PlayerEquipment>().items[2] != null)
        {
            for (int i = 0; i < PlayerManager.instance.rightHands.Length; i++)
            {
                if (PlayerManager.instance.rightHands[i].name == ("" + GameManager.instance.GetComponent<PlayerEquipment>().items[2].code))
                {
                    PlayerManager.instance.rightHands[i].SetActive(true);
                    PlayerManager.instance.activeRightHand = i;
                    break;
                }
                else
                {
                    PlayerManager.instance.rightHands[i].SetActive(false);
                    PlayerManager.instance.activeRightHand = -1;
                }
            }

        }
        if (GameManager.instance.GetComponent<PlayerEquipment>().items[3] != null)
        {
            for (int i = 0; i < PlayerManager.instance.heads.Length; i++)
            {
                if (PlayerManager.instance.heads[i].name == ("" + GameManager.instance.GetComponent<PlayerEquipment>().items[3].code))
                {
                    PlayerManager.instance.heads[i].SetActive(true);
                    PlayerManager.instance.activeHead = i;
                    break;
                }
                else
                {
                    PlayerManager.instance.heads[i].SetActive(false);
                    PlayerManager.instance.activeHead = -1;
                }
            }

        }
        if (GameManager.instance.GetComponent<PlayerEquipment>().items[4] != null)
        {
            for (int i = 0; i < PlayerManager.instance.tops.Length; i++)
            {
                if (PlayerManager.instance.tops[i].name == ("" + GameManager.instance.GetComponent<PlayerEquipment>().items[4].code))
                {
                    PlayerManager.instance.tops[i].SetActive(true);
                    PlayerManager.instance.activeTop = i;
                    break;
                }
                else
                {
                    PlayerManager.instance.tops[i].SetActive(false);
                    PlayerManager.instance.activeTop = -1;
                }
            }

        }
        if (GameManager.instance.GetComponent<PlayerEquipment>().items[5] != null)
        {
            for (int i = 0; i < PlayerManager.instance.pants.Length; i++)
            {
                if (PlayerManager.instance.pants[i].name == ("" + GameManager.instance.GetComponent<PlayerEquipment>().items[5].code))
                {
                    PlayerManager.instance.pants[i].SetActive(true);
                    PlayerManager.instance.activePants = i;
                    break;
                }
                else
                {
                    PlayerManager.instance.pants[i].SetActive(false);
                    PlayerManager.instance.activePants = -1;
                }
            }

        }
        if (GameManager.instance.GetComponent<PlayerEquipment>().items[6] != null)
        {
            for (int i = 0; i < PlayerManager.instance.gloves.Length; i++)
            {
                if (PlayerManager.instance.gloves[i].name == ("" + GameManager.instance.GetComponent<PlayerEquipment>().items[6].code))
                {
                    PlayerManager.instance.gloves[i].SetActive(true);
                    PlayerManager.instance.activeGloves = i;
                    break;
                }
                else
                {
                    PlayerManager.instance.gloves[i].SetActive(false);
                    PlayerManager.instance.activeGloves = -1;
                }
            }

        }
        if (GameManager.instance.GetComponent<PlayerEquipment>().items[7] != null)
        {
            for (int i = 0; i < PlayerManager.instance.shoes.Length; i++)
            {
                if (PlayerManager.instance.shoes[i].name == ("" + GameManager.instance.GetComponent<PlayerEquipment>().items[7].code))
                {
                    PlayerManager.instance.shoes[i].SetActive(true);
                    PlayerManager.instance.activeShoes = i;
                    break;
                }
                else
                {
                    PlayerManager.instance.shoes[i].SetActive(false);
                    PlayerManager.instance.activeShoes = -1;
                }
            }

        }
        if (GameManager.instance.GetComponent<PlayerEquipment>().items[8] != null)
        {
            for (int i = 0; i < PlayerManager.instance.necklesses.Length; i++)
            {
                if (PlayerManager.instance.necklesses[i].name == ("" + GameManager.instance.GetComponent<PlayerEquipment>().items[8].code))
                {
                    PlayerManager.instance.necklesses[i].SetActive(true);
                    PlayerManager.instance.activeNeckless = i;
                    break;
                }
                else
                {
                    PlayerManager.instance.tops[i].SetActive(false);
                    PlayerManager.instance.activeNeckless = -1;
                }
            }

        }
        if (GameManager.instance.GetComponent<PlayerEquipment>().items[9] != null)
        {
            for (int i = 0; i < PlayerManager.instance.earings.Length; i++)
            {
                if (PlayerManager.instance.earings[i].name == ("" + GameManager.instance.GetComponent<PlayerEquipment>().items[9].code))
                {
                    PlayerManager.instance.earings[i].SetActive(true);
                    PlayerManager.instance.activeEaring = i;
                    break;
                }
                else
                {
                    PlayerManager.instance.earings[i].SetActive(false);
                    PlayerManager.instance.activeEaring = -1;
                }
            }

        }
        if (GameManager.instance.GetComponent<PlayerEquipment>().items[10] != null)
        {
            for (int i = 0; i < PlayerManager.instance.rings.Length; i++)
            {
                if (PlayerManager.instance.rings[i].name == ("" + GameManager.instance.GetComponent<PlayerEquipment>().items[10].code))
                {
                    PlayerManager.instance.rings[i].SetActive(true);
                    PlayerManager.instance.activeRing = i;
                    break;
                }
                else
                {
                    PlayerManager.instance.rings[i].SetActive(false);
                    PlayerManager.instance.activeRing = -1;
                }
            }

        }
        if (GameManager.instance.GetComponent<PlayerEquipment>().items[11] != null)
        {
            for (int i = 0; i < PlayerManager.instance.hairs.Length; i++)
            {
                if (PlayerManager.instance.hairs[i].name == ("" + GameManager.instance.GetComponent<PlayerEquipment>().items[11].code))
                {
                    PlayerManager.instance.hairs[i].SetActive(true);
                    PlayerManager.instance.activeHair = i;
                    break;
                }
                else
                {
                    PlayerManager.instance.hairs[i].SetActive(false);
                    PlayerManager.instance.activeHair = -1;
                }
            }

        }
    }

    private void disregisterMotion(int slotNumber)
    {
        // 장비모션등록
        if (slotNumber == 1)
        {
            PlayerManager.instance.leftHands[PlayerManager.instance.activeLeftHand].SetActive(false);
            PlayerManager.instance.activeLeftHand = -1;
        }
        if (slotNumber == 2)
        {
            PlayerManager.instance.rightHands[PlayerManager.instance.activeRightHand].SetActive(false);
            PlayerManager.instance.activeRightHand = -1;
        }
        if (slotNumber == 3)
        {
            PlayerManager.instance.heads[PlayerManager.instance.activeHead].SetActive(false);
            PlayerManager.instance.activeHead = -1;
        }
        if (slotNumber == 4)
        {
            PlayerManager.instance.tops[PlayerManager.instance.activeTop].SetActive(false);
            PlayerManager.instance.activeTop = -1;
        }
        if (slotNumber == 5)
        {
            PlayerManager.instance.pants[PlayerManager.instance.activePants].SetActive(false);
            PlayerManager.instance.activePants = -1;
        }
        if (slotNumber == 6)
        {
            PlayerManager.instance.gloves[PlayerManager.instance.activeGloves].SetActive(false);
            PlayerManager.instance.activeGloves = -1;
        }
        if (slotNumber == 7)
        {
            PlayerManager.instance.shoes[PlayerManager.instance.activeShoes].SetActive(false);
            PlayerManager.instance.activeShoes = -1;
        }
        if (slotNumber == 8)
        {
            PlayerManager.instance.tops[PlayerManager.instance.activeNeckless].SetActive(false);
            PlayerManager.instance.activeNeckless = -1;
        }
        if (slotNumber == 9)
        {
            PlayerManager.instance.earings[PlayerManager.instance.activeEaring].SetActive(false);
            PlayerManager.instance.activeEaring = -1;
        }
        if (slotNumber == 10)
        {
            PlayerManager.instance.rings[PlayerManager.instance.activeRing].SetActive(false);
            PlayerManager.instance.activeRing = -1;
        }
        if (slotNumber == 11)
        {
            PlayerManager.instance.hairs[PlayerManager.instance.activeHair].SetActive(false);
            PlayerManager.instance.activeHair = -1;
        }
    }*/

    /*    public void removeMotion(EquipmentType slotType)
        {
            switch (slotType)
            {
                case EquipmentType.RightHand:
                    PlayerManager.instance.nowRightHand = null;
                    break;
                case EquipmentType.Top:
                    PlayerManager.instance.nowTop = null;
                    break;
                default:
                    break;
            }
        }*/

    public bool addItem(Item item, int slotNumber)
    {
        previousItem = null;
        int typeNumber = getEquipmentIndex(item.equipmentType);

        if (items[typeNumber] != null && items[typeNumber].itemName.Length > 0)
        {
            previousItem = items[typeNumber];
            Debug.Log("이전 아이템 이름 : " + previousItem.itemName);

            //disregisterMotion(getEquipmentIndex(item.equipmentType));
        }
        items[typeNumber] = item;

        PlayerInventory.instance.removeItem(slotNumber);

        if (previousItem != null)
        {
            PlayerInventory.instance.addItem(previousItem);
        }

        instance.onChangeItem.Invoke();
            //PlayerInventory.instance.onChangeItem.Invoke();
        //registerMotion();

        return true;
    }

    public bool addItem(Item item)
    {
        int typeNumber = getEquipmentIndex(item.equipmentType);
        items[typeNumber] = item;

        instance.onChangeItem.Invoke();
            //PlayerInventory.instance.onChangeItem.Invoke();
        //registerMotion();

        return true;
    }

    public int getEquipmentIndex(EquipmentType type)
    {
        switch (type)
        {
            case EquipmentType.LeftHand:
                return 1;
            case EquipmentType.RightHand:
                return 2;
            case EquipmentType.TwoHands:
                return 2;
            case EquipmentType.Head:
                return 3;
            case EquipmentType.Top:
                return 4;
            case EquipmentType.Pants:
                return 5;
            case EquipmentType.Body:
                return 4;
            case EquipmentType.Gloves:
                return 6;
            case EquipmentType.Shoes:
                return 7;
            case EquipmentType.Neckless:
                return 8;
            case EquipmentType.Earing:
                return 9;
            case EquipmentType.Ring:
                return 10;
            case EquipmentType.Hair:
                return 11;
            default: return 0;
        }
    }

    public string getEquipmentTypeName(EquipmentType type)
    {
        switch (type)
        {
            case EquipmentType.LeftHand:
                return "왼손";
            case EquipmentType.RightHand:
                return "오른손";
            case EquipmentType.TwoHands:
                return "양손";
            case EquipmentType.Head:
                return "머리";
            case EquipmentType.Top:
                return "상의";
            case EquipmentType.Pants:
                return "하의";
            case EquipmentType.Body:
                return "전신";
            case EquipmentType.Gloves:
                return "장갑";
            case EquipmentType.Shoes:
                return "신발";
            case EquipmentType.Neckless:
                return "목걸이";
            case EquipmentType.Earing:
                return "귀고리";
            case EquipmentType.Ring:
                return "반지";
            case EquipmentType hair:
                return "머리카락";
            default: return "";
        }
    }

    public void removeItem(EquipmentType slotType, int quantity = 1)
    {
        int slotNumber = getEquipmentIndex(slotType);
        previousItem = null;

        // null 장비템 제거
        if (instance.items[slotNumber] == null || instance.items[slotNumber].itemName.Length < 1)
        {
            items[slotNumber] = null;
            onChangeItem.Invoke();
        }

        if (PlayerInventory.instance.items.Count >= PlayerInventory.instance.slotCount)
        {
            //sDebug.Log("인벤토리 용량초과로 장비 해제불가!");
            return;
        }

        if (items[slotNumber] != null)
        {
            previousItem = items[slotNumber];
            //disregisterMotion(slotNumber);
            PlayerInventory.instance.addItem(previousItem);
        }
        items[slotNumber] = null;

        onChangeItem.Invoke();
        GameObject.Find("Canvas").GetComponent<StatUI>().isDataChanged = true;
    }

    public void updateTotalStats()
    {
        reinforce = 0;

        power = 0;
        armor = 0;
        accuracy = 0;
        avoid = 0;
        critRate = 0;
        critDam = 0;

        healthPoint = 0;
        manaPoint = 0;

        expEff = 0;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                putStats(items[i]);
            }
        }
        putStatsToPlayerData();
    }

    public void putStats(Item item)
    {
        reinforce += item.reinforce;

        power += item.power + item.powerReinforce;
        armor += item.armor + item.armorReinforce;
        accuracy += item.accuracy + item.accuracyReinforce;
        avoid += item.avoid + item.avoidReinforce;
        critRate += item.critRate + item.critRateReinforce;
        critDam += item.critDam + item.critDamReinforce;

        healthPoint += item.healthPoint + item.healthPointReinforce;
        manaPoint += item.manaPoint + item.manaPointReinforce;

        expEff += item.expEff + item.expEffReinforce;

        // 도구관련능력치도 추후 추가해야함!!!
    }

    public void putStatsToPlayerData()
    {
/*        GameManager.instance.playerData.powerEquipment = power;
        GameManager.instance.playerData.armorEquipment = armor;
        GameManager.instance.playerData.accuracyEquipment = accuracy;
        GameManager.instance.playerData.avoidEquipment = avoid;
        GameManager.instance.playerData.critRateEquipment = critRate;
        GameManager.instance.playerData.critDamEquipment = critDam;
        GameManager.instance.playerData.healthPointEquipment = healthPoint;
        GameManager.instance.playerData.manaPointEquipment = manaPoint;
        GameManager.instance.playerData.intellectEquipment = intellect;
        GameManager.instance.playerData.wisdomEquipment = wisdom;
        GameManager.instance.playerData.dexterityEquipment = dexterity;
        GameManager.instance.playerData.concentrationEquipment = concentration;
        GameManager.instance.playerData.expEffEquipment = expEff;*/
    }
}
