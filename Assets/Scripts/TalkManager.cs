using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TalkManager : MonoBehaviour
{
    private static int QUEST_EXIST_CHECK = 1000;
    private static int QUEST_BASIC_DIALOGUE = 1000;
    //private Dictionary<int, string[]> talkData;
    private List<TalkData> talkData;
    private Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArray;

    //Resources
    //public QuestInformationList questInformationList;
    //public List<QuestInformation> questInformations;

    public TextAsset textAsset;
    public TalkResources viciniGirl;
    public TalkResources underwearBoy;
    public TalkResources survival;

    //public GameObject questRewordPanel;

    private QuestType type;

    public GameObject questRewordPanel;

    private void Start()
    {
        viciniGirl = new TalkResources();
        viciniGirl.talkData = new List<string>();
        underwearBoy = new TalkResources();
        underwearBoy.talkData = new List<string>();
        survival = new TalkResources();
        survival.talkData = new List<string>();

        //questRewordPanel.SetActive(false);
    }
    void Awake()
    {
        //talkData = new Dictionary<int, string[]>();
        talkData = new List<TalkData>();
        portraitData = new Dictionary<int, Sprite>();
        //loadTalkResources();
        GenerateData();
        //loadQuestResources();
        //saveQuestInformation();
    }

    // Update is called once per frame
    void GenerateData()
    {
        // 기본캐릭
        portraitData.Add(1 + 0, portraitArray[30]);
        portraitData.Add(1 + 1, portraitArray[31]);
        portraitData.Add(1 + 2, portraitArray[32]);
        portraitData.Add(1 + 3, portraitArray[33]);

        // 생존자
        portraitData.Add(120000 + 0, portraitArray[0]);
        portraitData.Add(120000 + 1, portraitArray[1]);
        portraitData.Add(120000 + 2, portraitArray[2]);
        portraitData.Add(120000 + 3, portraitArray[3]);
        
        // 비키니걸
        portraitData.Add(100000 + 0, portraitArray[4]);
        portraitData.Add(100000 + 1, portraitArray[5]);
        portraitData.Add(100000 + 2, portraitArray[6]);
        portraitData.Add(100000 + 3, portraitArray[7]);

        // 팬티보이
        portraitData.Add(110000 + 0, portraitArray[0]);
        portraitData.Add(110000 + 1, portraitArray[1]);
        portraitData.Add(110000 + 2, portraitArray[2]);
        portraitData.Add(110000 + 3, portraitArray[3]);
    }

    public string getTalk(int id, int talkIndex)
    {
        string talk = "";

        switch (id)
        {
            case 1:
                talk = "마법은 어렵고 청소는 쉽지...$0@2";
                break;
            case 120000:
                talk = "필요한 물건은 사가고 필요없는 물건은 나한테 팔아!$0";
                break;
            case 100000:
                talk = "이런... 햇빛이 강한데...$0#썬크림을 바르고 싶단 말이야!$3#피부가 다 타버릴까봐 걱정돼...$2";
                break;
            case 110000:
                talk = "정신을 차린거니?$0#우린 아무래도 이곳에 갇힌것 같아$1#탈출할 방법을 모색해보자!$0";
                break;
            default:
                talk = "너와는 말하고 싶지 않아";
                    break;
        }

        if (id >= 10000 && id <=10100)
        {
            talk = "낡아보이지만 꽤나 튼튼한 나무상자가 있다.";
        }

        if (id == 9001)
        {
            talk = "소지품을 보관할 때 사용하는 상자이다.";
        }
        
        string[] talks = talk.Split('#');

        if (talkIndex == talks.Length)
        {
            return null;
        }

        return talks[talkIndex];
    }

    public Sprite getPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }

/*    [ContextMenu("From Json Data")]
    public void loadTalkResources()
    {
        Debug.Log("대화 데이터 로드 성공");

        textAsset = Resources.Load<TextAsset>("Npc/viciniGirl");
        viciniGirl = JsonUtility.FromJson<TalkResources>(textAsset.ToString());
        putTalkData(viciniGirl.talkData, 100000);

        textAsset = Resources.Load<TextAsset>("Npc/underwearBoy");
        underwearBoy = JsonUtility.FromJson<TalkResources>(textAsset.ToString());
        putTalkData(underwearBoy.talkData, 110000);

        textAsset = Resources.Load<TextAsset>("Npc/survival");
        survival = JsonUtility.FromJson<TalkResources>(textAsset.ToString());
        putTalkData(survival.talkData, 120000);
    }

    [ContextMenu("To Json Data")]
    public void saveQuestInformation()
    {
        Debug.Log("저장 성공");
        //questInformationList.questInformations = new List<QuestInformation>();

        List<int> itemCodes = new List<int>();
        List<int> itemCounts = new List<int>();

        List<int> itemCodes2 = new List<int>();
        List<int> itemCounts2 = new List<int>();
        List<string> mobName = new List<string>();
        List<int> killCount = new List<int>();

        itemCodes.Add(3000001);
        itemCodes.Add(3000002);
        itemCounts.Add(2);
        itemCounts.Add(2);

        itemCodes2.Add(2000002);
        itemCounts2.Add(5);
        mobName.Add("slime");
        killCount.Add(3);

        //questInformationList.questInformations.Add(new QuestInformation(mainQuest(2), 0, 0, null, null, null, itemCodes, itemCounts));
        //questInformationList.questInformations.Add(new QuestInformation(mainQuest(5), 0, 0, null, null, null, itemCodes, itemCounts, mobName, killCount));

        string jsonData = JsonUtility.ToJson(questInformationList, true);
        Debug.Log(jsonData.Length);

        File.WriteAllText(saveOrLoad(false, true, "QuestInformation"), jsonData);
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

    [ContextMenu("From Json Data")]
    public void loadQuestResources()
    {
        Debug.Log("퀘스트 정보 로드 성공");

        textAsset = Resources.Load<TextAsset>("Npc/QuestInformation");
        questInformationList = JsonUtility.FromJson<QuestInformationList>(textAsset.ToString());
        //putQuestInfomation(questInformationList);
    }*/

    public void putTalkData(List<string> datas, int npcId)
    {
        string splitedType = "";
        int questIndex = 0;
        string line = "";
        string[] lines = null;
        for (int i = 0; i < datas.Count; i++)
        {
            try
            {
                splitedType = datas[i].Split('^')[1];
            }
            catch
            {
                splitedType = "None";
            }

            switch (splitedType)
            {
                case "Main":
                    type = QuestType.Main;
                    break;
                case "Sub":
                    type = QuestType.Sub;
                    break;
                case "Repeat":
                    type = QuestType.Repeat;
                    break;
                case "Event":
                    type = QuestType.Event;
                    break;
                default:
                    type = QuestType.None;
                        break;
            }

            splitedType = datas[i].Split('^')[0];
            questIndex = int.Parse(splitedType.Split('@')[0]);
            line = splitedType.Split('@')[1];
            lines = line.Split('#');

            talkData.Add(new TalkData(npcId + questIndex, lines, npcId, type));
        }
    }

    public int mainQuest(int questNumber)
    {
        return questNumber = 80 + questNumber * 20;
    }

    public void rewordPanelOff()
    {
        questRewordPanel.SetActive(false);
    }
}

[System.Serializable]
public class TalkResources
{
    public List<string> talkData;
}

public class TalkData
{
    public int id;
    public string[] talks;
    public int npcId;
    public QuestType type;

    public TalkData(int id, string[] talks, int npcId, QuestType type = QuestType.None)
    {
        this.id = id;
        this.talks = talks;
        this.npcId = npcId;
        this.type = type;
    }
}
