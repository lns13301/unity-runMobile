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

    private string spritePath = "Images/Character/Illust";
    // private string prefabPath = "Images/hero";

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        heroDataFile = new HeroDataFile();
        heroDataFile.heroDatas = new List<EntityData>();

        //SaveHeroData();
        LoadHeroData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("To Json Data")]
    public void SaveHeroData()
    {
        Debug.Log("저장 성공");
        heroDataFile.heroDatas = new List<EntityData>();

        heroDataFile.heroDatas.Add(new EntityData
            ("[나뭇잎의 친구] 너구리", 0, spritePath + "/" + "0", Element.EARTH, Rating.APPRENTICE, 1, 0, 0, 50, 5, 15, 15, 5, 20, 200, 200, 0, 0));
        heroDataFile.heroDatas.Add(new EntityData
            ("[흩날리는 깃털] 비둘기", 1, spritePath + "/" + "1", Element.WIND, Rating.BEGINNER, 1, 0, 0, 55, 5, 5, 25, 10, 30, 150, 150, 0, 0));
        heroDataFile.heroDatas.Add(new EntityData
            ("[검은 그림자] 까마귀", 2, spritePath + "/" + "2", Element.FIRE, Rating.BEGINNER, 1, 0, 0, 45, 5, 5, 10, 0, 50, 150, 150, 0, 0));

        string jsonData = JsonUtility.ToJson(heroDataFile, true);

        File.WriteAllText(SaveOrLoad(false, true, "heroData"), jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadHeroData()
    {
        try
        {
            Debug.Log("영웅 정보 로드 성공");
            /*string jsonData = File.ReadAllText(saveOrLoad(false, false, "heroData"));
            heroDataFile = JsonUtility.FromJson<heroDataFile>(jsonData);*/

            heroDataFile = JsonUtility.FromJson<HeroDataFile>(Resources.Load<TextAsset>("heroData").ToString());

            for (int i = 0; i < heroDataFile.heroDatas.Count; i++)
            {
                //heroDataFile.heroDatas[i].sprite = loadSprite(heroDataFile.heroDatas[i].spritePath);
                heroDataFile.heroDatas[i].applySprite();
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
            LoadHeroData();
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
