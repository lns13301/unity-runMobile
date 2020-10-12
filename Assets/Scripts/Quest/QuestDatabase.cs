using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestDatabase : MonoBehaviour
{
    public static QuestDatabase instance;

    private TextAsset textAsset;
    public QuestResource questResource;

    public List<Quest> questDB;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        saveQuestResources();
        loadQuestResources();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [ContextMenu("To Json Data")]
    public void saveQuestResources()
    {
        List<int> itemCodes = new List<int>();
        itemCodes.Add(3000001);
        itemCodes.Add(3000002);

        List<int> itemCounts = new List<int>();
        itemCounts.Add(2);
        itemCounts.Add(2);

        List<int> rewordItemCodes = new List<int>();
        rewordItemCodes.Add(1000001);
        rewordItemCodes.Add(2000001);

        List<int> rewordItemCounts = new List<int>();
        rewordItemCounts.Add(1);
        rewordItemCounts.Add(5);

        QuestReword questReword = new QuestReword(10, 5);
        QuestReword questReword1 = new QuestReword(30, 15, rewordItemCodes, rewordItemCounts);

        QuestClearLimit questClearLimit = new QuestClearLimit(itemCodes, itemCounts);

        string talkStart0 = "일단은 몸을 데워야하지 않겠어 ?$0#설마 이상한 생각을 한 것은 아니겠지?$1#옆에 팬티보이에게 어떻게하면 좋을지 물어보도록 해!$0";
        string talkEnd0 = "무슨 일이지?$0";

        string talkStart1 = "불을 피우겠다고?$1#어렵겠지만 그렇다면...$0#주변에 쓸만한 나뭇가지랑 불을 크게 만들 마른 나뭇잎을 주워오도록 해!$0#나뭇가지 2개와 마른 나뭇잎 2개 정도면 불씨를 만들 수 있을거야$0";
        string talkEnd1 = "재료를 다 구해왔구나?$0";

        questResource.quests = new List<Quest>();

        questResource.quests.Add(new Quest(0, 100000, 110000, "불피울 방법을 알아내자",
            "비키니걸이 무슨 문제가 있는듯 하다.",
            "'비키니걸'이 말하길 '팬티보이'에게 가면 불피울 방법을 알려준다고 한다.",
            "비키니걸이 말한대로 팬티보이에게 말을 걸어보았다.",
            talkStart0, talkEnd0, QuestType.Main, QuestState.Startable, questReword, null));

        questResource.quests.Add(new Quest(1, 110000, 110000, "불피울 재료를 줍자",
            "팬티보이에게 다시 말을 걸어보자",
            "불을 피우기위한 기초 준비물로 나무가지 2개와 마른 나뭇잎 2개가 필요하다고 한다. \n해당 재료를 구하려면 해변가에 떨어진 재료를 주워서 모아보자.",
            "불을 피우기 위한 재료를 구해다 주었다.",
            talkStart1, talkEnd1, QuestType.Main, QuestState.Startable, questReword1, questClearLimit, new QuestStartLimit(new List<int> { 0, 0 })));

        string jsonData = JsonUtility.ToJson(questResource, true);
        Debug.Log(jsonData.Length);

        File.WriteAllText(saveOrLoad(true, true, "QuestDatas"), jsonData);
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

        textAsset = Resources.Load<TextAsset>("Quest/QuestDatas");
        questResource = JsonUtility.FromJson<QuestResource>(textAsset.ToString());
        questDB = questResource.quests;
        //putQuest(questResource.quests);
    }

/*    public void putQuest(List<Quest> datas)
    {
        foreach (Quest data in datas)
        {
            questDB.Add(data);
        }
    }*/

    public bool isSatisfactQuestStartLimit(int id, List<int> startQuest, List<Quest> currentQuest, List<int> questClearId, PlayerData playerData)
    {
        int beforeQuestCount = questDB[id].questStartLimit.beforeQuestLimit.Count;
        int clearQuestCount = 0;

        // 시작가능한 퀘스트에 이미 추가된 내역은 열외
        for (int i = 0; i < startQuest.Count; i++)
        {
            if (questDB[id].questId == startQuest[i])
            {
                return false;
            }
        }

        // 진행중인 퀘스트 열외
        for (int j = 0; j < currentQuest.Count; j++)
        {
            if (questDB[id].questId == currentQuest[j].questId)
            {
                return false;
            }
        }

        // 완료된 퀘스트 열외, 
        for (int j = 0; j < questClearId.Count; j++)
        {
            if (questDB[id].questId == questClearId[j])
            {
                return false;
            }
        }

        // 퀘스트 상태여부, 선행조건으로 클리어해야하는 퀘스트
        for (int i = 0; i < questDB[id].questStartLimit.beforeQuestLimit.Count; i++)
        {
            for (int j = 0; j < questClearId.Count; j++)
            {
                if (questDB[id].questStartLimit.beforeQuestLimit[i] == questClearId[j])
                {
                    clearQuestCount++;
                }
            }
        }

        // 플레이어 데이터 선행조건
        if (questDB[id].questStartLimit.playerDataLimit.fame > playerData.fame
            || questDB[id].questStartLimit.playerDataLimit.charm > playerData.charm
            || questDB[id].questStartLimit.playerDataLimit.level > playerData.level)
        {
            return false;
        }

        return clearQuestCount == beforeQuestCount;
    }

    public bool isSatisfactionClearLimit(Quest quest)
    {
        // 아이템 수량확인
        for (int i = 0; i < questDB[quest.questId].questClearLimit.itemCount.Count; i++)
        {
            try
            {
                if (questDB[quest.questId].questClearLimit.itemCount[i] > PlayerInventory.instance.findItemByCode(questDB[quest.questId].questClearLimit.itemCode[i]).count)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // 몬스터 처치 수 확인
        for (int i = 0; i < questDB[quest.questId].questClearLimit.killCount.Count; i++)
        {
            try
            {
                if (questDB[quest.questId].questClearLimit.killCount[i] > quest.questClearLimit.killCount[i])
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // 방문 위치 확인
        for (int i = 0; i < questDB[quest.questId].questClearLimit.isVisit.Count; i++)
        {
            try
            {
                if (quest.questClearLimit.isVisit[i])
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        return true;
    }

    public string findNpcNameByCode(int code, bool isEnglish = true)
    {
        switch (code)
        {
            case 1:
                return isEnglish ? "George" : "조지";
        }

        return null;
    }

    public Quest makeQuest(int id)
    {
        return new Quest(questDB[id].questId, questDB[id].npcIdStart, questDB[id].npcIdEnd, questDB[id].questTitle, questDB[id].questExplainStart, questDB[id].questExplainCurrent, questDB[id].questExplainComplete,
            questDB[id].talkStart, questDB[id].talkEnd, questDB[id].questType, questDB[id].questState, makeQuestReword(questDB[id].questReword), makeQuestClearLimit(questDB[id].questClearLimit), makeQuestStartLimit(questDB[id].questStartLimit));
    }

    public QuestReword makeQuestReword(QuestReword questReword)
    {
        return new QuestReword(questReword.rewordExp, questReword.rewordMoney, questReword.itemCode, questReword.itemCount, questReword.rewordItem);
    }

    public QuestClearLimit makeQuestClearLimit(QuestClearLimit questClearLimit)
    {
        List<int> itemCounts = new List<int>();
        List<int> killCounts = new List<int>();
        List<bool> isVisits = new List<bool>();

        for (int i = 0; i < questClearLimit.itemCount.Count; i++)
        {
            itemCounts.Add(0);
        }

        for (int i = 0; i < questClearLimit.killCount.Count; i++)
        {
            killCounts.Add(0);
        }

        for (int i = 0; i < questClearLimit.isVisit.Count; i++)
        {
            isVisits.Add(false);
        }

        QuestClearLimit qcl = new QuestClearLimit(questClearLimit.itemCode, itemCounts, questClearLimit.mobName, killCounts, questClearLimit.visitLocation, isVisits);

        return qcl;
    }

    public QuestStartLimit makeQuestStartLimit(QuestStartLimit questStartLimit)
    {
        return new QuestStartLimit(questStartLimit.beforeQuestLimit, questStartLimit.playerDataLimit);
    }
}

[System.Serializable]
public class QuestResource
{
    public List<Quest> quests;
}
