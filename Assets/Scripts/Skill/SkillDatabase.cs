using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SkillDatabase : MonoBehaviour
{
    public static SkillDatabase instance;
    public SkillDataFile skillDataFile;

    public List<Skill> skillDB = new List<Skill>();

    public string spritePath;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        skillDataFile = new SkillDataFile();
        skillDataFile.skillDatas = new List<Skill>();

        spritePath = "Images/UI/Spell";

        //saveSkillData();
        loadSkillData();
        //GameManager.instance.playerData.addSkill(skillDB[0]);
        GameManager.instance.playerData.addSkill(skillDB[0]);
        GameManager.instance.playerData.addSkill(skillDB[1]);
        GameManager.instance.playerData.addSkill(skillDB[2]);
        GameManager.instance.playerData.addSkill(skillDB[3]);
        GameManager.instance.playerData.addSkill(skillDB[4]);
        GameManager.instance.playerData.addSkill(skillDB[5]);
        GameManager.instance.playerData.addSkill(skillDB[6]);
        GameManager.instance.playerData.addSkill(skillDB[7]);
        GameManager.instance.playerData.addSkill(skillDB[8]);
        GameManager.instance.playerData.addSkill(skillDB[9]);
        GameManager.instance.playerData.addSkill(skillDB[10]);
        GameManager.instance.playerData.addSkill(skillDB[11]);
        GameManager.instance.playerData.addSkill(skillDB[12]);
        GameManager.instance.playerData.addSkill(skillDB[13]);
        GameManager.instance.playerData.addSkill(skillDB[14]);
        GameManager.instance.playerData.addSkill(skillDB[15]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("To Json Data")]
    public void saveSkillData()
    {
        Debug.Log("저장 성공");
        skillDataFile.skillDatas = new List<Skill>();

        skillDataFile.skillDatas.Add(new Skill(0, "파이어 볼", spritePath + "/" + "Spell", 100, 10000
            , "불 속성의 마력 분자를 작은 구의 형태로 응축시킨 후 대상에게 날려서 피해를 주는 마법", 3, Element.FIRE, 1, 20, 130));
        skillDataFile.skillDatas.Add(new Skill(1, "파이어 빔", spritePath + "/" + "Spell", 100, 10000
            , "불 속성의 마력을 응축시켜 한 곳으로 날리는 마법으로 피격 받은 대상은 화상 효과로 4턴간 준 피해량의 50% 지속 피해를 주는 마법", 4, Element.FIRE, 2.5f, 80, 150, true,
            0, 0, 0, 0, 0, 0, 0, StatusEffect.BURNNING, 4, 0));
        skillDataFile.skillDatas.Add(new Skill(2, "파이어 레이", spritePath + "/" + "Spell", 100, 10000
            , "다수의 파이어 볼을 응축시켜 하나의 거대한 불덩이로 만들어 단일 대상에게 강력한 피해를 주는 마법", 5, Element.FIRE, 2f, 350, 450));
        skillDataFile.skillDatas.Add(new Skill(3, "파이어 봄", spritePath + "/" + "Spell", 100, 10000
            , "불 속성의 폭발 성질을 모아 다수의 대상에게 순간적으로 폭발을 일으키며 피해를 주는 마법", 6, Element.FIRE, 4f, 1000, 350, false));

        skillDataFile.skillDatas.Add(new Skill(100, "아이스 커버", spritePath + "/" + "Spell", 100, 10000
            , "냉기 속성의 마력 분자를 대상에게 입힌 후 깨뜨려 외상을 입히는 마법", 12, Element.ICE, 1, 20, 130));
        skillDataFile.skillDatas.Add(new Skill(101, "아이스 스피어", spritePath + "/" + "Spell", 100, 10000
            , "냉기 속성의 마력 분자를 응축시켜 한 곳으로 날리는 마법으로 피격 받은 대상은 동상 효과로 5초간 캐스팅 속도를 50% 느리게 하는 마법", 13, Element.ICE, 2.5f, 80, 150, true,
            0, 0, 0, 0, 0, 0, 0, StatusEffect.FROSTBITE, 5, 0));
        skillDataFile.skillDatas.Add(new Skill(102, "아이스 청크", spritePath + "/" + "Spell", 100, 10000
            , "냉기 속성의 마력 분자를 여러겹 응축시켜 거대화하여 단일 대상에게 강력한 피해를 주는 마법", 14, Element.ICE, 2f, 350, 450));
        skillDataFile.skillDatas.Add(new Skill(103, "블리자드", spritePath + "/" + "Spell", 100, 10000
            , "주위의 마력을 순간적으로 매우 차갑게하여 바닥에서부터 고드름이 올라와서 다수의 대상에게 피해를 주는 마법", 15, Element.ICE, 4f, 1000, 350, false));

        skillDataFile.skillDatas.Add(new Skill(200, "폴링 스톤", spritePath + "/" + "Spell", 100, 10000
            , "대지의 돌맹이를 단일 대상에게 떨어뜨리는 마법", 20, Element.EARTH, 1, 20, 130));
        skillDataFile.skillDatas.Add(new Skill(201, "스톤 핸드", spritePath + "/" + "Spell", 100, 10000
            , "대지와 마력의 힘을 합하여 거대한 손의 형태를 지닌 돌로 단일 대상을 기절 효과로 차징 게이지를 0으로 만든다.", 21, Element.EARTH, 2.5f, 80, 150, true,
            0, 0, 0, 0, 0, 0, 0, StatusEffect.STUN, 0, 0));
        skillDataFile.skillDatas.Add(new Skill(202, "자이언트 스톤", spritePath + "/" + "Spell", 100, 10000
            , "마력을 통해 대지의 돌을 하나의 거대한 돌로 만들어 대상에게 피해를 주는 마법", 22, Element.EARTH, 2f, 350, 450));
        skillDataFile.skillDatas.Add(new Skill(203, "어스 퀘이크", spritePath + "/" + "Spell", 100, 10000
            , "대지 깊은 곳을 자극하여 주변 일대의 대지를 흔들어 다수의 대상에게 피해를 주는 마법", 23, Element.EARTH, 4f, 1000, 350, false));

        skillDataFile.skillDatas.Add(new Skill(300, "클리어", spritePath + "/" + "Spell", 100, 10000
            , "자연의 마력을 활용하여 단일 대상의 디버프 마법 1개를 제거하는 마법", 27, Element.NONE, 2f, 100, 0));
        skillDataFile.skillDatas.Add(new Skill(301, "베이직 큐어", spritePath + "/" + "Spell", 100, 10000
            , "자연의 마력을 활용하여 단일 대상을 마법 공격력의 비율만큼 치유하는 마법", 28, Element.NONE, 4f, 350, 0, true, 0, 200));
        skillDataFile.skillDatas.Add(new Skill(302, "캐스팅 포커스", spritePath + "/" + "Spell", 100, 10000
            , "30초간 캐스팅 속도가 50% 감소하는 마법", 29, Element.NONE, 1, 500, 0, true, 0, 0));
        skillDataFile.skillDatas.Add(new Skill(303, "콘센트레이트", spritePath + "/" + "Spell", 100, 10000
            , "20초간 집중하여 피격 시에도 캐스팅이 취소되지 않도록 하는 마법", 30, Element.NONE, 2.5f, 1500, 0, true, 0, 0));


        string jsonData = JsonUtility.ToJson(skillDataFile, true);

        Debug.Log(jsonData.Length);

        File.WriteAllText(saveOrLoad(false, true, "SkillData"), jsonData);
    }

    [ContextMenu("From Json Data")]
    public Sprite loadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    public Sprite[] loadAllSprite(string path)
    {
        return Resources.LoadAll<Sprite>(path);
    }

    [ContextMenu("From Json Data")]
    public void loadSkillData()
    {
        try
        {
            sprites = loadAllSprite(spritePath);
            Debug.Log("스킬 정보 로드 성공");
/*            string jsonData = File.ReadAllText(saveOrLoad(false, false, "SkillData"));
            skillDataFile = JsonUtility.FromJson<SkillDataFile>(jsonData);*/

            skillDataFile = JsonUtility.FromJson<SkillDataFile>(Resources.Load<TextAsset>("SkillData").ToString());

            for (int i = 0; i < skillDataFile.skillDatas.Count; i++)
            {
                // skillDataFile.skillDatas[i].sprite = loadSprite(skillDataFile.skillDatas[i].imagePath);  각각 이미지 로드 시 사용
                skillDataFile.skillDatas[i].sprite = sprites[skillDataFile.skillDatas[i].spriteNum];  // 통 이미지 로드 시 사용
                skillDB.Add(skillDataFile.skillDatas[i]);
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("로드 오류");

            string jsonData = JsonUtility.ToJson(skillDataFile, true);

            File.WriteAllText(saveOrLoad(true, false, "SkillData"), jsonData);
            loadSkillData();
        }
    }

    public string saveOrLoad(bool isMobile, bool isSave, string fileName)
    {
        if (isSave)
        {
            if (isMobile)
            {
                // 모바일 저장
                return Path.Combine(Application.persistentDataPath, fileName + ".json");
            }
            else
            {
                // pc 저장
                return Path.Combine(Application.dataPath, fileName + ".json");
            }
        }
        else
        {
            if (isMobile)
            {
                // 모바일 로드
                return Path.Combine(Application.persistentDataPath, fileName + ".json");
            }
            else
            {
                // pc 로드
                return Path.Combine(Application.dataPath, fileName + ".json");
            }
        }
    }

    public Skill findSkillById(int id)
    {
        for (int i = 0; i < skillDB.Count; i++)
        {
            if (skillDB[i].skillId == id)
            {
                return skillDB[i];
                Debug.Log(skillDB[i].skillName);
            }
        }
        Debug.Log("실패");
        return skillDB[0];
    }
}


[System.Serializable]
public class SkillDataFile
{
    public List<Skill> skillDatas;

    public Skill getSkill(int skillId)
    {
        for (int i = 0; i < skillDatas.Count; i++)
        {
            if (skillDatas[i].skillId == skillId)
            {
                return skillDatas[i];
            }
        }

        return null;
    }

    public void resetSkills()
    {
        for (int i = 0; i < skillDatas.Count; i++)
        {
            skillDatas[i].experience = 0;
            skillDatas[i].level = 0;
        }
    }
}