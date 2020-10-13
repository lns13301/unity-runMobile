using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class MonsterDatabase : MonoBehaviour
{
    public static MonsterDatabase instance;
    public MobDataFile mobDataFile;

    public Dictionary<int, EntityData> mobDatas = new Dictionary<int, EntityData>();

    public List<EntityData> mobDB = new List<EntityData>();

    private string spritePath = "Images/Mob";
    // private string prefabPath = "Images/Mob";

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        mobDataFile = new MobDataFile();
        mobDataFile.mobDatas = new List<EntityData>();

        //SaveMobData();
        LoadMobData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("To Json Data")]
    public void saveMobData()
    {
        Debug.Log("저장 성공");
        mobDataFile.mobDatas = new List<EntityData>();

/*        mobDataFile.mobDatas.Add(new EntityData
            ("대지 버섯", 0, Job.NONE, Element.EARTH, 1, 3, 0, 5, 0, null, 0, 0, 0, 0, 0, 5, 5, 3, 3, 3, 2, 5, 30, 100, 100, 20, 20, spritePath + "/" + "mushroom_red_idle"));
        mobDataFile.mobDatas.Add(new EntityData
            ("대지 연지비그", 1, Job.NONE, Element.EARTH, 3, 10, 0, 8, 0, null, 0, 0, 0, 0, 0, 7, 10, 5, 15, 5, 3, 3, 50, 200, 200, 100, 100,
            spritePath + "/" + "FlowerMob", prefabPath + "/" + "FlowerMob"));*/

        string jsonData = JsonUtility.ToJson(mobDataFile, true);

        File.WriteAllText(SaveOrLoad(false, true, "mobData"), jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadMobData()
    {
        try
        {
            Debug.Log("몬스터 정보 로드 성공");
            /*string jsonData = File.ReadAllText(saveOrLoad(false, false, "mobData"));
            mobDataFile = JsonUtility.FromJson<mobDataFile>(jsonData);*/

            mobDataFile = JsonUtility.FromJson<MobDataFile>(Resources.Load<TextAsset>("mobData").ToString());

            for (int i = 0; i < mobDataFile.mobDatas.Count; i++)
            {
                //mobDataFile.mobDatas[i].sprite = loadSprite(mobDataFile.mobDatas[i].spritePath);
                mobDB.Add(mobDataFile.mobDatas[i]);
            }

            // 딕셔너리에 몬스터 정보 입력
            for (int i = 0; i < mobDB.Count; i++)
            {
                mobDatas.Add(mobDB[i].code, mobDB[i]);
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("로드 오류");

            string jsonData = JsonUtility.ToJson(mobDataFile, true);

            File.WriteAllText(SaveOrLoad(false, false, "mobData"), jsonData);
            LoadMobData();
        }
    }

    public string SaveOrLoad(bool isMobile, bool isSave, string fileName)
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

    [ContextMenu("From Json Data")]
    public Sprite LoadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }
}

[System.Serializable]
public class MobDataFile
{
    public List<EntityData> mobDatas;
}
