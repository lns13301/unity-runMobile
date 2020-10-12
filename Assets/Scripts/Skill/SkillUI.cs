using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public static SkillUI instance;
    public GameObject skillSet;

    public GameObject content;
    public GameObject skillTitlePanel;

    public List<Skill> skills;
    public List<Skill> fires;
    public List<Skill> ices;
    public List<Skill> earths;
    public List<Skill> nones;
    public GameObject informationPanel;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        skillSet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void uiOnOff()
    {
        SoundManager.instance.PlayButtonEffectSound();

        if (skillSet.activeSelf)
        {
            skillSet.SetActive(false);
        }
        else
        {
            if (GetComponent<InventoryUI>().inventorySet.activeSelf)
            {
                GetComponent<InventoryUI>().inventorySet.SetActive(false);
            }
            if (GetComponent<StatUI>().statSet.activeSelf)
            {
                GetComponent<StatUI>().statSet.SetActive(false);
            }
            if (GetComponent<QuestUI>().questSet.activeSelf)
            {
                GetComponent<QuestUI>().questSet.SetActive(false);
            }

            clearInformationText();
            skillSet.SetActive(true);
        }
    }

    private void separateSkills()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            // 불 마법
            if (skills[i].skillId < 100)
            {
                fires.Add(skills[i]);
            }
            // 얼음 마법
            else if (skills[i].skillId >= 100 && skills[i].skillId < 200)
            {
                ices.Add(skills[i]);
            }
            else if (skills[i].skillId >= 200 && skills[i].skillId < 300)
            {
                earths.Add(skills[i]);
            }
            else if (skills[i].skillId >= 300 && skills[i].skillId < 400)
            {
                nones.Add(skills[i]);
            }
        }
    }

    private void setSkill(PlayerData playerData)
    {
        this.skills = playerData.skills;
        fires = new List<Skill>();
        ices = new List<Skill>();
        earths = new List<Skill>();
        nones = new List<Skill>();

        separateSkills();
    }

    public void selectSkill(int index)
    {
        setSkill(GameManager.instance.playerData);

        switch (index)
        {
            case 0:
                setSkillList(fires);
                break;
            case 1:
                setSkillList(ices);
                break;
            case 2:
                setSkillList(earths);
                break;
            case 3:
                setSkillList(nones);
                break;
        }
    }

    public void setSkillList(List<Skill> skills)
    {
        setSkillDestroy();

        for (int i = 0; i < skills.Count; i++)
        {
            GameObject go = Instantiate(skillTitlePanel);
            go.transform.SetParent(content.transform);
            go.GetComponent<SkillTitlePanel>().setSkill(skills[i]);
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            // 타이틀 제목
            GameObject title = go.transform.GetChild(0).gameObject;
            title.GetComponent<Text>().text = skills[i].skillName;
        }
    }

    private void setSkillDestroy()
    {
        for (int i = content.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(content.transform.GetChild(i).gameObject);
        }
    }

    public void clearInformationText()
    {
        informationPanel.transform.GetChild(0).gameObject.GetComponent<Image>().gameObject.SetActive(false);
        informationPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = "";
        informationPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "";
        informationPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = "";
        informationPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "";
        informationPanel.transform.GetChild(5).gameObject.GetComponent<Text>().text = "";
        informationPanel.transform.GetChild(6).gameObject.GetComponent<Text>().text = "";
    }

    public void setSkillInformation(Skill skill, Image image)
    {
        informationPanel.transform.GetChild(0).gameObject.GetComponent<Image>().gameObject.SetActive(true);

        informationPanel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = image.sprite;
        informationPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = skill.skillName;
        informationPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "소모 마력 : " + skill.costMP;
        if (skill.magicPower > 0)
        {
            informationPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "효과 : 마법 공격력의 " + skill.magicPower
            + "%의 피해를 준다.";
        }
        else if (skill.heal > 0)
        {
            informationPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "효과 : 마법 공격력의 " + skill.heal
            + "%의 생명력을 회복시킨다.";
        }
        else
        {
            informationPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "특수한 효과를 가진 마법입니다.";
        }
        informationPanel.transform.GetChild(5).gameObject.GetComponent<Text>().text = "캐스팅 시간 : " + skill.castingTime + "초";
        informationPanel.transform.GetChild(6).gameObject.GetComponent<Text>().text = skill.information;

        // TODO 상태 이상 공격에 대한 텍스트를 정의할 메서드 추가 GetChild(4)

        if (skill.isTargetOne)
        {
            informationPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = "타겟 : 단일";
        }
        else
        {
            informationPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = "타겟 : 다수";
        }
    }

    /*    public void informationUIOnOff(Skill skill, Image skillImage)
        {
            if (infoSet.gameObject.activeSelf)
            {
                infoSet.gameObject.SetActive(false);
            }
            else
            {
                infoSet.gameObject.SetActive(true);
                buttonMenuAnimator.SetBool("isUIOn", true);

                infoSet.skillName.text = skill.skillName;
                infoSet.skillLevel.text = "기술 레벨 : " + GameManager.instance.playerSkillData.getSkill(skill.skillId).level + " / " + skill.level;
                infoSet.skillExp.text = "숙련도 : " + GameManager.instance.playerSkillData.getSkill(skill.skillId).experience + " / " + skill.calculateMastery(skill.level);
                infoSet.content.text = skill.information;
                infoSet.skillImage.sprite = skillImage.sprite;
            }
        }*/
}