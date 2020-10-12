using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;

    private Dictionary<int, QuestData> questList;
    // Start is called before the first frame update
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        generateData();
    }

    // Update is called once per frame
    void generateData()
    {
        questList.Add(mainQuest(1), new QuestData("첫 대화 시도", new int[] { 100000, 110000 }));
        questList.Add(mainQuest(2), new QuestData("불피울 도구 줍기", new int[] { 110000 }));
        questList.Add(mainQuest(3), new QuestData("불을 피워보자", new int[] { 110000 }));
    }

    public int getQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string checkQuest()
    {
        return questList[questId].questName;
    }

    public string checkQuest(int id)
    {
        try
        {
            if (id == questList[questId].npcId[questActionIndex])
            {
                questActionIndex++;
                //GetComponent<DialogManager>().playerData.currentQuest.Add(questId);
            }
            
            if (questActionIndex == questList[questId].npcId.Length)
            {
                nextQuest();
            }

            return questList[questId].questName;
        }
        catch (KeyNotFoundException)
        {
            Debug.LogError("checkQuest 오류발생!!");
            return "";
        }
    }

    void nextQuest()
    {
        //GetComponent<DialogManager>().playerData.currentQuest.Remove(questId);
        //GetComponent<DialogManager>().playerData.clearQuest.Add(questId);
        questId += 20;
        questActionIndex = 0;
    }

    // 이 수치는 한 퀘스트 이름아래 20번까지 npc와 대화를 진행할 수 있다는 것으로 20번이 넘어가면 다음 메인퀘스트가 되버린다.
    public int mainQuest(int questNumber)
    {
        return questNumber = 80 + questNumber * 20;
    }
}
