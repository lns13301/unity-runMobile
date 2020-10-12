using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int questId;
    public string questTitle;
    public string questExplainStart;
    public string questExplainCurrent;
    public string questExplainComplete;
    public QuestType questType;
    public QuestState questState;

    public int npcIdStart;
    public int npcIdEnd;
    //public int talkContext; // 현재 퀘스트의 몇 번째 문장을 진행할 지
    public string talkStart;
    public string talkEnd;
    public QuestReword questReword;
    public QuestClearLimit questClearLimit;
    public QuestStartLimit questStartLimit;

    public Quest(int questId, int npcIdStart, int npcIdEnd, string questTitle, string questExplainStart, string questExplainCurrent, string questExplainComplete,
        string talkStart, string talkEnd, QuestType questType, QuestState questState, QuestReword questReword, QuestClearLimit questClearLimit, QuestStartLimit questStartLimit = null)
    {
        this.questId = questId;
        this.questTitle = questTitle;
        this.questExplainStart = questExplainStart;
        this.questExplainCurrent = questExplainCurrent;
        this.questExplainComplete = questExplainComplete;

        this.questType = questType;
        this.questState = questState;

        this.talkStart = talkStart;
        this.talkEnd = talkEnd;
        this.questReword = questReword;
        this.questClearLimit = questClearLimit;
        this.questStartLimit = questStartLimit;

        this.npcIdStart = npcIdStart;
        this.npcIdEnd = npcIdEnd;
        //this.talkContext = talkContext;
    }

    public string getTalkDataStart(int talkIndex)
    {
/*        if (!(npcId == npcIdStart))
        {
            return null;
        }*/

        if (talkIndex == talkStart.Split('#').Length)
        {
            return null;
        }

        return talkStart.Split('#')[talkIndex];
    }

    public string getTalkDataEnd(int talkIndex)
    {
/*        if (!(npcId == npcIdEnd))
        {
            return null;
        }*/

        if (talkIndex == talkEnd.Split('#').Length)
        {
            return null;
        }

        return talkEnd.Split('#')[talkIndex];
    }
}

public class QuestPlayer
{
    public int questId;

    public QuestType questType;
    public QuestState questState;

    public QuestClearLimit questClearLimit;

    public QuestPlayer(int questId, QuestType questType, QuestState questState, QuestClearLimit questClearLimit)
    {
        this.questId = questId;
        this.questType = questType;
        this.questState = questState;
        this.questClearLimit = questClearLimit;
    }
}

[System.Serializable]
public class QuestReword
{
    public int rewordExp;
    public int rewordMoney;
    public List<int> itemCode;
    public List<int> itemCount;
    public List<Item> rewordItem;

    public QuestReword(int rewordExp, int rewordMoney, List<int> itemCode = null, List<int> itemCount = null, List<Item> rewordItem = null)
    {
        this.rewordExp = rewordExp;
        this.rewordMoney = rewordMoney;
        this.itemCode = itemCode;
        this.itemCount = itemCount;
        this.rewordItem = rewordItem;
    }

    public string toString()
    {
        string reword = "";

        if (rewordExp != 0)
        {
            reword += rewordExp + " 경험치";
        }
        if (rewordMoney != 0)
        {
            if (!reword.Equals(""))
            {
                reword += ", ";
            }
            reword += rewordMoney + " 핀";
        }
        if (itemCode != null)
        {
            for (int i = 0; i < itemCode.Count; i++)
            {
                if (!reword.Equals(""))
                {
                    reword += ", ";
                }
                reword += itemCode[i] + " " + itemCount[i] + "개";
            }
        }
        if (itemCode != null)
        {
            for (int i = 0; i < rewordItem.Count; i++)
            {
                if (!reword.Equals(""))
                {
                    reword += ", ";
                }
                reword += rewordItem[i].itemName + " " + rewordItem[i].count + "개";
            }
        }

        return reword;
    }
}

[System.Serializable]
public class QuestStartLimit
{
    public List<int> beforeQuestLimit;
    public PlayerData playerDataLimit;

    public QuestStartLimit(List<int> beforeQuestLimit = null, PlayerData playerDataLimit = null)
    {       
        this.beforeQuestLimit = beforeQuestLimit;
        this.playerDataLimit = playerDataLimit;
    }
}

[System.Serializable]
public class QuestClearLimit
{
    public List<int> itemCode;
    public List<int> itemCount;
    public List<string> mobName;
    public List<int> killCount;
    public List<int> visitLocation;
    public List<bool> isVisit;

    public QuestClearLimit(List<int> itemCode = null, List<int> itemCount = null, List<string> mobName = null, List<int> killCount = null, List<int> visitLocation = null, List<bool> isVisit = null)
    {
        this.itemCode = itemCode;
        this.itemCount = itemCount;
        this.mobName = mobName;
        this.killCount = killCount;

        this.visitLocation = visitLocation;
        this.isVisit = isVisit;
    }
}

[System.Serializable]
public enum QuestType
{
    None,
    Main,
    Sub,
    Repeat,
    Event
}

[System.Serializable]
public enum QuestState
{
    Startable,
    Proceeding,
    Complete
}
