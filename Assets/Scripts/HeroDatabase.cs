using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HeroDatabase : MonoBehaviour
{
    public static HeroDatabase instance;
    public HeroDataFile heroDataFile;

    public Dictionary<int, EntityData> heroDatas = new Dictionary<int, EntityData>();

    public List<EntityData> heroDB = new List<EntityData>();

    private string spritePath = "Images/hero";
    // private string prefabPath = "Images/hero";

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        heroDataFile = new HeroDataFile();
        heroDataFile.heroDatas = new List<EntityData>();

        //SaveheroData();
        LoadheroData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("To Json Data")]
    public void saveheroData()
    {
        Debug.Log("저장 성공");
        heroDataFile.heroDatas = new List<EntityData>();

        /*        heroDataFile.heroDatas.Add(new EntityData
                    ("대지 버섯", 0, Job.NONE, Element.EARTH, 1, 3, 0, 5, 0, null, 0, 0, 0, 0, 0, 5, 5, 3, 3, 3, 2, 5, 30, 100, 100, 20, 20, spritePath + "/" + "mushroom_red_idle"));
                heroDataFile.heroDatas.Add(new EntityData
                    ("대지 연지비그", 1, Job.NONE, Element.EARTH, 3, 10, 0, 8, 0, null, 0, 0, 0, 0, 0, 7, 10, 5, 15, 5, 3, 3, 50, 200, 200, 100, 100,
                    spritePath + "/" + "Flowerhero", prefabPath + "/" + "Flowerhero"));*/

        string jsonData = JsonUtility.ToJson(heroDataFile, true);

        File.WriteAllText(SaveOrLoad(false, true, "heroData"), jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadheroData()
    {
        try
        {
            Debug.Log("몬스터 정보 로드 성공");
            /*string jsonData = File.ReadAllText(saveOrLoad(false, false, "heroData"));
            heroDataFile = JsonUtility.FromJson<heroDataFile>(jsonData);*/

            heroDataFile = JsonUtility.FromJson<HeroDataFile>(Resources.Load<TextAsset>("heroData").ToString());

            for (int i = 0; i < heroDataFile.heroDatas.Count; i++)
            {
                //heroDataFile.heroDatas[i].sprite = loadSprite(heroDataFile.heroDatas[i].spritePath);
                heroDB.Add(heroDataFile.heroDatas[i]);
            }

            // 딕셔너리에 몬스터 정보 입력
            for (int i = 0; i < heroDB.Count; i++)
            {
                heroDatas.Add(heroDB[i].code, heroDB[i]);
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("로드 오류");

            string jsonData = JsonUtility.ToJson(heroDataFile, true);

            File.WriteAllText(SaveOrLoad(false, false, "heroData"), jsonData);
            LoadheroData();
        }
    }

    public string SaveOrLoad(bool isheroile, bool isSave, string fileName)
    {
        if (isSave)
        {
            if (isheroile)
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
            if (isheroile)
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
}

[System.Serializable]
public class HeroDataFile
{
    public List<EntityData> heroDatas;
}
