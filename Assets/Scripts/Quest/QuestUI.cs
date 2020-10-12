using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public static QuestUI instance;
    private static string NEW_LINE = "\n";

    public GameObject questSet;

    public GameObject questTitlePanel;
    public GameObject content;
    public GameObject rewordPanel;

    public GameObject questInformationPanel;
    public QuestState buttonState;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        questSet.SetActive(false);
        rewordPanel.SetActive(false);
        buttonState = QuestState.Startable;
        questInformationPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            uiOnOff();
        }
    }

    public void uiOnOff()
    {
        SoundManager.instance.PlayButtonEffectSound();

        if (GameObject.Find("Canvas").GetComponent<ItemMenuSet>().isReinforceProgressing)
        {
            return;
        }

        questSet.SetActive(false);

        if (questSet.activeSelf)
        {
            questSet.SetActive(false);
        }
        else
        {
            if (GetComponent<StatUI>().statSet.activeSelf)
            {
                GetComponent<StatUI>().statSet.SetActive(false);
            }
            if (GetComponent<InventoryUI>().inventorySet.activeSelf)
            {
                GetComponent<InventoryUI>().inventorySet.SetActive(false);
            }
            if (GetComponent<SkillUI>().skillSet.activeSelf)
            {
                GetComponent<SkillUI>().skillSet.SetActive(false);
            }

            questSet.SetActive(true);
        }
    }

    private void setQuestDestroy()
    {
        for (int i = content.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(content.transform.GetChild(i).gameObject);
        }
    }


    public void setStartableQuest()
    {
        setQuestDestroy();
        buttonState = QuestState.Startable;

        for (int i = 0; i < GameManager.instance.playerData.startQuest.Count; i++)
        {
            GameObject go = Instantiate(questTitlePanel);
            go.transform.SetParent(content.transform);
            go.GetComponent<QuestTitleButton>().questId = GameManager.instance.playerData.startQuest[i];
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //GameManager.instance.applyResolutionScale(go);

            // 타이틀 제목
            GameObject title = go.transform.GetChild(0).gameObject;
            title.GetComponent<Text>().text = QuestDatabase.instance.questDB[GameManager.instance.playerData.startQuest[i]].questTitle;
        }
    }

    public void setProceedingQuest()
    {
        setQuestDestroy();
        buttonState = QuestState.Proceeding;

        for (int i = 0; i < GameManager.instance.playerData.currentQuest.Count; i++)
        {
            GameObject go = Instantiate(questTitlePanel);
            go.transform.SetParent(content.transform);
            go.GetComponent<QuestTitleButton>().questId = GameManager.instance.playerData.currentQuest[i].questId;
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //GameManager.instance.applyResolutionScale(go);
            //go.GetComponent<Text>().text = QuestDatabase.instance.questDB[dialogManager.playerData.currentQuest[i].questId].questTitle;

            // 타이틀 제목
            GameObject title = go.transform.GetChild(0).gameObject;
            title.GetComponent<Text>().text = QuestDatabase.instance.questDB[GameManager.instance.playerData.currentQuest[i].questId].questTitle;
        }
    }

    public void setCompleteQuest()
    {
        setQuestDestroy();
        buttonState = QuestState.Complete;

        for (int i = 0; i < GameManager.instance.playerData.clearQuest.Count; i++)
        {
            GameObject go = Instantiate(questTitlePanel);
            go.transform.SetParent(content.transform);
            go.GetComponent<QuestTitleButton>().questId = GameManager.instance.playerData.clearQuest[i];
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //GameManager.instance.applyResolutionScale(go);
            //go.GetComponent<Text>().text = QuestDatabase.instance.questDB[dialogManager.playerData.clearQuest[i]].questTitle;

            // 타이틀 제목
            GameObject title = go.transform.GetChild(0).gameObject;
            title.GetComponent<Text>().text = QuestDatabase.instance.questDB[GameManager.instance.playerData.clearQuest[i]].questTitle;
        }
    }

    public void showRewordPanel(Quest quest)
    {
        rewordPanel.SetActive(true);

        GameObject reword = rewordPanel.transform.Find("Reword").gameObject;

        reword.GetComponent<Text>().text = "";

        reword.GetComponent<Text>().text += Calculator.numberToFormatting(quest.questReword.rewordExp) + " 경험치" + NEW_LINE;
        reword.GetComponent<Text>().text += Calculator.numberToFormatting(quest.questReword.rewordMoney) + " 핀" + NEW_LINE;

        for (int i = 0; i < quest.questReword.itemCode.Count; i++)
        {
            reword.GetComponent<Text>().text += ItemDatabase.instance.findItemByCode(quest.questReword.itemCode[i]).itemName + " ";

            if (quest.questReword.itemCount[i] > 1)
            {
                reword.GetComponent<Text>().text += quest.questReword.itemCount[i] + "개";
            }

            reword.GetComponent<Text>().text += NEW_LINE;
        }
    }

    public void showSpaceLimitMessage(int needSlotCount)
    {
        rewordPanel.SetActive(true);

        GameObject reword = rewordPanel.transform.Find("Reword").gameObject;
        reword.GetComponent<Text>().text = "소지품 공간이 부족하여 퀘스트를 완료할 수 없습니다. 최소 공간을 " + -needSlotCount + "칸 이상 확보해주세요.";
    }

    public void showQuestInformationPanel(int questId)
    {
        questInformationPanel.SetActive(true);

        if (buttonState == QuestState.Startable)
        {
            questInformationPanel.transform.Find("QuestTitle").GetComponent<Text>().text = QuestDatabase.instance.questDB[questId].questTitle;
            questInformationPanel.transform.Find("QuestType").GetComponent<Text>().text = showQuestType(QuestDatabase.instance.questDB[questId].questType);
            questInformationPanel.transform.Find("QuestNPC").GetComponent<Text>().text = QuestDatabase.instance.findNpcNameByCode(QuestDatabase.instance.questDB[questId].npcIdStart, false);
            questInformationPanel.transform.Find("QuestContent").GetComponent<Text>().text = QuestDatabase.instance.questDB[questId].questExplainStart;
            questInformationPanel.transform.Find("QuestReword").GetComponent<Text>().text = QuestDatabase.instance.questDB[questId].questReword.toString();
        }

        if (buttonState == QuestState.Proceeding)
        {
            questInformationPanel.transform.Find("QuestTitle").GetComponent<Text>().text = QuestDatabase.instance.questDB[questId].questTitle;
            questInformationPanel.transform.Find("QuestType").GetComponent<Text>().text = showQuestType(QuestDatabase.instance.questDB[questId].questType);
            questInformationPanel.transform.Find("QuestNPC").GetComponent<Text>().text = QuestDatabase.instance.findNpcNameByCode(QuestDatabase.instance.questDB[questId].npcIdEnd);
            questInformationPanel.transform.Find("QuestContent").GetComponent<Text>().text = QuestDatabase.instance.questDB[questId].questExplainCurrent;

            questInformationPanel.transform.Find("QuestLimit").GetComponent<Text>().text = "";

            // 요구 아이템 수량
            for (int i = 0; i < QuestDatabase.instance.questDB[questId].questClearLimit.itemCode.Count; i++)
            {
                questInformationPanel.transform.Find("QuestLimit").GetComponent<Text>().text += ItemDatabase.instance.findItemByCode(QuestDatabase.instance.questDB[questId].questClearLimit.itemCode[i]).itemName
                    + "  " + PlayerInventory.instance.totalItemAmount(ItemDatabase.instance.findItemByCode(QuestDatabase.instance.questDB[questId].questClearLimit.itemCode[i]))
                    + " / " + QuestDatabase.instance.questDB[questId].questClearLimit.itemCount[i] + NEW_LINE;

                // 리스트 특성상 요구아이템 1개일 때 두 번째 중복 값이 일치하면 반복문 나감
                if (QuestDatabase.instance.questDB[questId].questClearLimit.itemCode.Count == 2 && QuestDatabase.instance.questDB[questId].questClearLimit.itemCode[0] == QuestDatabase.instance.questDB[questId].questClearLimit.itemCode[1])
                {
                    break;
                }
            }

            // 해당 퀘스트 진행사항 찾기
            Quest currentQuest = null;

            for (int i = 0; i < GameManager.instance.playerData.currentQuest.Count; i++)
            {
                if (GameManager.instance.playerData.currentQuest[i].questId == questId)
                {
                    currentQuest = GameManager.instance.playerData.currentQuest[i];
                }
            }

            // 요구 사냥
            for (int i = 0; i < QuestDatabase.instance.questDB[questId].questClearLimit.mobName.Count; i++)
            {
                questInformationPanel.transform.Find("QuestLimit").GetComponent<Text>().text += QuestDatabase.instance.questDB[questId].questClearLimit.mobName[i]
                    + " 처치  " + currentQuest.questClearLimit.killCount[i]
                    + " / " + QuestDatabase.instance.questDB[questId].questClearLimit.killCount[i];

                // 리스트 특성상 요구사냥 1개일 때 두 번째 중복 값이 일치하면 반복문 나감
                if (QuestDatabase.instance.questDB[questId].questClearLimit.mobName.Count == 2 && QuestDatabase.instance.questDB[questId].questClearLimit.mobName[0] == QuestDatabase.instance.questDB[questId].questClearLimit.mobName[1])
                {
                    break;
                }
            }

            //요구 방문지역

            // 보상 아이템 목록
            questInformationPanel.transform.Find("QuestReword").GetComponent<Text>().text = "";

            for (int i = 0; i < QuestDatabase.instance.questDB[questId].questReword.itemCount.Count; i++)
            {
                questInformationPanel.transform.Find("QuestReword").GetComponent<Text>().text += ItemDatabase.instance.findItemByCode(QuestDatabase.instance.questDB[questId].questReword.itemCode[i]).itemName
                    + " " + QuestDatabase.instance.questDB[questId].questReword.itemCount[i] + "개" + NEW_LINE;
            }

            for (int i = 0; i < QuestDatabase.instance.questDB[questId].questReword.rewordItem.Count; i++)
            {
                questInformationPanel.transform.Find("QuestReword").GetComponent<Text>().text += QuestDatabase.instance.questDB[questId].questReword.rewordItem[i].itemName
                    + " " + QuestDatabase.instance.questDB[questId].questReword.itemCount[i] + "개" + NEW_LINE;
            }

        }

        if (buttonState == QuestState.Complete)
        {
            questInformationPanel.transform.Find("QuestTitle").GetComponent<Text>().text = QuestDatabase.instance.questDB[questId].questTitle;
            questInformationPanel.transform.Find("QuestType").GetComponent<Text>().text = showQuestType(QuestDatabase.instance.questDB[questId].questType);
            questInformationPanel.transform.Find("QuestNPC").GetComponent<Text>().text = QuestDatabase.instance.findNpcNameByCode(QuestDatabase.instance.questDB[questId].npcIdStart) + " ~ " + QuestDatabase.instance.findNpcNameByCode(QuestDatabase.instance.questDB[questId].npcIdEnd);
            questInformationPanel.transform.Find("QuestContent").GetComponent<Text>().text = QuestDatabase.instance.questDB[questId].questExplainComplete;
        }
    }


    public void closeQuestInformationPanel()
    {
        questInformationPanel.SetActive(false);
    }

    public string showQuestType(QuestType questType)
    {
        switch (questType)
        {
            case QuestType.Main:
                return "메인 퀘스트";
            case QuestType.Sub:
                return "서브 퀘스트";
            case QuestType.Repeat:
                return "반복 퀘스트";
            case QuestType.Event:
                return "이벤트 퀘스트";
            default:
                return "";
        }
    }
}
