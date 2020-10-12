using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTitlePanel : MonoBehaviour
{
    public Skill skill;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setSkill(Skill skill)
    {
        this.skill = new Skill(skill.skillId, skill.skillName, skill.imagePath, skill.level, skill.experience, skill.information, skill.spriteNum, skill.element, skill.castingTime,
            skill.costMP, skill.magicPower, skill.isTargetOne, skill.remainDamage, skill.heal, skill.fixedDamage, skill.sacrificeHP, skill.sacrificeMP, skill.power, skill.shield,
            skill.statusEffect, skill.duration, skill.costHP);
        image.sprite = skill.sprite;
    }

    public void showSkillInformation()
    {
        SkillUI.instance.setSkillInformation(skill, image);
    }
}
