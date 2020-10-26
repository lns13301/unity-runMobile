using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EntityData
{
    public string entityName;
    public int code;
    public float playerX;
    public float playerY;
    public Element element;
    public Rating rating;
    public int level;
    public int exp;
    public int nextExp;
    //public Item[] equipments;

    public int power;
    public int armor;
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

    public List<Skill> skills;

    public float expStack;
    public int sortingIndex;

    public string spritePath;
    public Sprite spriteIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public EntityData(string entityName, int code, string spritePath, Element element, Rating rating,
    int level, int exp, int nextExp, int power, int armor, int accuracy, int avoid, float critRate,
    float critDam, float healthPoint, float healthPointMax, float manaPoint, float manaPointMax, float expEff = 0, int fame = 0, int charm = 0,
    float expStack = 0, int sortingIndex = 0, List<Skill> skills = null, float playerX = 0, float playerY = 0)
    {
        this.entityName = entityName;
        this.code = code;
        this.spritePath = spritePath;
        this.element = element;
        this.rating = rating;
        this.level = level;
        this.exp = exp;
        this.nextExp = exp;
        this.power = power;
        this.armor = armor;
        this.accuracy = accuracy;
        this.avoid = avoid;
        this.critRate = critRate;
        this.critDam = critDam;
        this.healthPoint = healthPoint;
        this.healthPointMax = healthPointMax;
        this.manaPoint = manaPoint;
        this.manaPointMax = manaPointMax;
        this.expEff = expEff;
        this.fame = fame;
        this.charm = charm;
        this.expStack = expStack;
        this.sortingIndex = sortingIndex;
        this.skills = skills;
        this.playerX = playerX;
        this.playerY = playerY;

        spriteIcon = loadSprite(spritePath);
    }

    [ContextMenu("From Json Data")]
    public Sprite loadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    public string GetElementName()
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
}

[System.Serializable]
public enum Rating
{
    NONE,
    APPRENTICE,
    BEGINNER,
    EXPERT,
    SENIER,
    MASTER
}
