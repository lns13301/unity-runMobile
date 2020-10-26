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
    public List<Item> items;
    //public Item[] equipments;

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

    public List<Skill> skills;

    public float expStack;
    public int sortingIndex;

    public Sprite spriteIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
