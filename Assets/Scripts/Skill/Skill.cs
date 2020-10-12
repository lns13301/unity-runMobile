using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Skill
{
    public int skillId;
    public string skillName;
    public string imagePath;
    public int level;
    public int experience;
    public string information;

    public int spriteNum;
    public Sprite sprite;

    public Element element;
    public float castingTime;
    public float costMP;
    public float magicPower;
    public bool isTargetOne;
    public float remainDamage;
    public float heal;
    public float fixedDamage;
    public float sacrificeHP;
    public float sacrificeMP;
    public float power;
    public float shield;
    public StatusEffect statusEffect;
    public int duration;
    public float costHP;

    public Skill()
    {

    }

    public Skill(int skillId)
    {
        this.skillId = skillId;
    }

    public Skill(int skillId, string skillName, string imagePath, int level, int experience, string information, int spriteNum, Element element, float castingTime,
        float costMP, float magicPower, bool isTargetOne = true, float remainDamage = 0, float heal = 0, float fixedDamage = 0, float sacrificeHP = 0, float sacrificeMP = 0,
        float power = 0, float shield = 0, StatusEffect statusEffect = StatusEffect.NONE, int duration = 0, float costHP = 0)
    {
        this.skillId = skillId;
        this.skillName = skillName;
        this.imagePath = imagePath;
        this.level = level;
        this.experience = experience;
        this.information = information;
        this.spriteNum = spriteNum;
        this.element = element;
        this.castingTime = castingTime;
        this.costMP = costMP;
        this.magicPower = magicPower;
        this.isTargetOne = isTargetOne;
        this.remainDamage = remainDamage;
        this.heal = heal;
        this.fixedDamage = fixedDamage;
        this.sacrificeHP = sacrificeHP;
        this.sacrificeMP = sacrificeMP;
        this.power = power;
        this.shield = shield;
        this.statusEffect = statusEffect;
        this.duration = duration;
        this.costHP = costHP;

        sprite = loadSprite();
    }

    [ContextMenu("From Json Data")]
    public Image loadImage(string path)
    {
        return Resources.Load<Image>(path);
    }

    public Sprite loadSprite()
    {
        return Resources.Load<Sprite>(imagePath);
    }

    public int calculateMastery(int level)
    {
        int value = 1;

        if (level < 10)
        {
            for (int i = 0; i < level + 1; i++)
            {
                value *= 2;
            }
            return level * value;
        }
        else
        {
            return 2000;
        }
    }

    public void addSkillExperience(int skillId)
    {
        experience += skillId;
    }

    public void setLevel()
    {
        int tempExp = experience;
        for(int i = 0; i < 100; i++)
        {
            if (experience - calculateMastery(i) < 0)
            {
                level = i;
                return;
            }
        }
    }
}

public enum StatusEffect
{
    NONE,
    SLEEP,
    STUN,
    FROZEN,
    BLEEDING,
    BURNNING,
    FROSTBITE,
    INFECT
}