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

    public float expEff;

    public int fame;
    public int charm;

    public int sortingIndex;

    public List<Skill> skills;
    public List<EntityData> heroDatas;
    public int heroIndex;

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