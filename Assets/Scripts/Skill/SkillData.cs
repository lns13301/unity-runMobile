using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public List<Skill> skills;

    public SkillData(List<Skill> skills)
    {
        this.skills = skills;
    }


}
