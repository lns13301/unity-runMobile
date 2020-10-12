using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float playerX;
    public float playerY;
    public int questId;
    public int questActionIndex;
    public string name;
    public Job job;
    public Element element;
    public int level;
    public int exp;
    public int nextExp;
    public int money;
    public int gold;
    public int inventorySize;
    //public List<QuestInformation> questInformation;
    public List<Item> items;
    //public Item[] equipments;

    //Quest
    public List<int> startQuest;
    public List<Quest> currentQuest;
    public List<int> clearQuest;

    public int power;
    public int armor;
    public int magicPower;
    public int magicArmor;
    public int accuracy;
    public int avoid;
    public float critRate;
    public float critDam;

    public float healthPoint;
    public float healthPointMax;
    public float manaPoint;
    public float manaPointMax;

    public float expEff;

    public int fame;
    public int charm;
    public float weight;
    public float weightMax;

    public float expStack;
    public int sortingIndex;

    public List<Skill> skills;
    public List<HeroData> heroDatas;

    //Equipments
    /*    public int reinforce;

        public int powerEquipment;
        public int armorEquipment;
        public int accuracyEquipment;
        public int avoidEquipment;
        public float critRateEquipment;
        public float critDamEquipment;

        public float healthPointEquipment;
        public float manaPointEquipment;

        public float intellectEquipment;
        public float wisdomEquipment;
        public float dexterityEquipment;
        public float concentrationEquipment;

        public float expEffEquipment;*/

    public string getJobName()
    {
        switch (job)
        {
            case Job.APPRENTICE:
                return "견습 마법사";
            case Job.BEGINNER:
                return "초보 마법사";
            case Job.EXPERT:
                return "숙련 마법사";
            case Job.MAGE:
                return "마도사";
            case Job.GREATMAGE:
                return "대마도사";
            case Job.SAGE:
                return "현자";
            case Job.GREATSAGE:
                return "대현자";
            case Job.SUPERMAGE:
                return "초대마도사 (" + getElementName() + ")";
            default :
                return "무직";
        }
    }

    public string getElementName()
    {
        switch (element)
        {
            case Element.FIRE:
                return "화염";
            case Element.ICE:
                return "얼음";
            case Element.EARTH:
                return "대지";
            case Element.THUNDER:
                return "번개";
            case Element.WIND:
                return "바람";
            case Element.LIGHTNESS:
                return "빛";
            case Element.DARKNESS:
                return "어둠";
            default:
                return "무속";
        }
    }

    /*public void setPlayerDataByBattleEntity(BattleEntity battleEntity)
    {
        this.playerX = battleEntity.playerX;
        this.playerY = battleEntity.playerY;
        this.questId = battleEntity.questId;
        this.questActionIndex = battleEntity.questActionIndex;
        this.name = battleEntity.entityName;
        this.job = battleEntity.job;
        this.element = battleEntity.element;
        this.level = battleEntity.level;
        this.exp = battleEntity.exp;
        this.nextExp = battleEntity.nextExp;
        this.money = battleEntity.money;
        this.gold = battleEntity.gold;
        this.inventorySize = battleEntity.inventorySize;
        //this.questInformation = battleEntity.questInformation;
        this.items = battleEntity.items;
        //this.Item[] equipments = battleEntity.equipments;

        //Quest
        this.startQuest = battleEntity.startQuest;
        this.currentQuest = battleEntity.currentQuest;
        this.clearQuest = battleEntity.clearQuest;

        //Stats
        this.statPoint = battleEntity.statPoint;
        this.intellectPoint = battleEntity.intellectPoint;
        this.wisdomPoint = battleEntity.wisdomPoint;
        this.dexterityPoint = battleEntity.dexterityPoint;
        this.concentrationPoint = battleEntity.concentrationPoint;

        this.power = battleEntity.power;
        this.armor = battleEntity.armor;
        this.magicPower = battleEntity.magicPower;
        this.magicArmor = battleEntity.magicArmor;
        this.accuracy = battleEntity.accuracy;
        this.avoid = battleEntity.avoid;
        this.critRate = battleEntity.critRate;
        this.critDam = battleEntity.critDam;

        this.healthPoint = battleEntity.healthPoint;
        this.healthPointMax = battleEntity.healthPointMax;
        this.manaPoint = battleEntity.manaPoint;
        this.manaPointMax = battleEntity.manaPointMax;

        this.expEff = battleEntity.expEff;

        this.fame = battleEntity.fame;
        this.charm = battleEntity.charm;
        this.weight = battleEntity.weight;
        this.weightMax = battleEntity.weightMax;

        this.expStack = battleEntity.expStack;
        this.sortingIndex = battleEntity.sortingIndex;
    }*/

    public void addSkill(Skill skill)
    {
        skills.Add(skill);
    }
}

[System.Serializable]
public enum Job
{
    NONE,
    APPRENTICE,
    BEGINNER,
    EXPERT,
    MAGE,
    GREATMAGE,
    SAGE,
    GREATSAGE,
    SUPERMAGE
}

[System.Serializable]
public enum Element
{
    NONE,
    FIRE,
    ICE,
    EARTH,
    THUNDER,
    WIND,
    LIGHTNESS,
    DARKNESS
}