using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSet : MonoBehaviour
{
    public int skillId;
    public Skill skill;

    public Text skillName;
    public Image panelImage;
    public Image symbol;
    public Text skillLevel;
    public Button showInformation;
    
    // Start is called before the first frame update
    void Start()
    {
        skillName = transform.GetChild(0).gameObject.GetComponent<Text>();
        panelImage = transform.GetChild(1).gameObject.GetComponent<Image>();
        symbol = transform.GetChild(2).gameObject.GetComponent<Image>();
        skillLevel = transform.GetChild(3).gameObject.GetComponent<Text>();
        showInformation = transform.GetChild(4).gameObject.GetComponent<Button>();
        skill = new Skill();

        setSkill();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setSkill()
    {
        skill.skillName = SkillDatabase.instance.findSkillById(skillId).skillName;
        skill.level = SkillDatabase.instance.findSkillById(skillId).level;
        skill.sprite = SkillDatabase.instance.findSkillById(skillId).sprite;
        skill.experience = SkillDatabase.instance.findSkillById(skillId).experience;
        skill.imagePath = SkillDatabase.instance.findSkillById(skillId).imagePath;
        skill.information = SkillDatabase.instance.findSkillById(skillId).information;
        skill.skillId = skillId;

        skillName.text = skill.skillName;
/*        panelImage = Resources.Load<Image>(skill.imagePath);
        symbol = skill.image;*/
        skillLevel.text = "기술 레벨 : " + GameManager.instance.playerData.level;
    }

    public void uiOnOff()
    {
        //GameObject.Find("Canvas").GetComponent<SkillUI>().informationUIOnOff(skill, symbol);
    }
}
