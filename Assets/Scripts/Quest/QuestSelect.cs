using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSelect : MonoBehaviour
{
    public int questId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectStartQuest()
    {
        GameManager.instance.nowQuest = QuestDatabase.instance.makeQuest(questId);
        GameManager.instance.playerData.startQuest.Remove(questId);
        GameManager.instance.isQuestTalk = true;
        GameManager.instance.talkIndex = 0;
        GameManager.instance.talkQuestPanel.SetActive(false);
        GameManager.instance.objectData.questTalkStart();

        /*        for (int i = 0; i < GameManager.instance.playerData.startQuest.Count; i++)
                {
                    if (GameManager.instance.playerData.startQuest[i] == questId)
                    {
                        GameManager.instance.playerData.startQuest.RemoveAt(i);
                    }
                }*/
    }

    public void selectClearQuest()
    {
        // 인벤토리에 보상받을 공간없을 때
        if (PlayerInventory.instance.slotCount - PlayerInventory.instance.items.Count < QuestDatabase.instance.questDB[questId].questReword.itemCode.Count)
        {
            //Debug.Log("슬롯 : " + PlayerInventory.instance.slotCount + "   아이템 보유수 : " + PlayerInventory.instance.items.Count + "   퀘스트 보상수 : " + QuestDatabase.instance.questDB[questId].questReword.itemCode.Count);
            QuestUI.instance.showSpaceLimitMessage(QuestDatabase.instance.questDB[questId].questReword.itemCode.Count
                - PlayerInventory.instance.slotCount - PlayerInventory.instance.items.Count);

            return;
        }

        for (int i = 0; i < GameManager.instance.playerData.currentQuest.Count; i++)
        {
            if (GameManager.instance.playerData.currentQuest[i].questId == questId)
            {
                GameManager.instance.nowQuest = GameManager.instance.playerData.currentQuest[i];
            }
        }
        
        for (int i = 0; i < GameManager.instance.playerData.currentQuest.Count; i++)
        {
            if (GameManager.instance.playerData.currentQuest[i].questId == questId)
            {
                GameManager.instance.playerData.currentQuest.RemoveAt(i);
            }
        }

        GameManager.instance.isQuestTalk = true;
        GameManager.instance.talkIndex = 0;
        GameManager.instance.talkQuestPanel.SetActive(false);
        GameManager.instance.objectData.questTalkStart();

        /*        for (int i = 0; i < GameManager.instance.playerData.startQuest.Count; i++)
                {
                    if (GameManager.instance.playerData.startQuest[i] == questId)
                    {
                        GameManager.instance.playerData.startQuest.RemoveAt(i);
                    }
                }*/
    }

    public void setQuestId(int id)
    {
        questId = id;
    }

    public void test()
    {
        Debug.Log("Hi");
    }
}
