using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectData : MonoBehaviour, IPointerUpHandler
{
    public int id;
    public bool isNpc;
    public bool isShop;
    public bool isChest;
    public bool isCollecting;
    public int shopSlotCount = 10;
    public string objectName;

    public List<int> questStart;
    public List<int> tempQuestStart;
    public List<int> questEnd;
    public List<int> tempQuestEnd;
    public bool isChangeData;

    public GameObject newQuest;
    public GameObject doneQuest;

    public GameObject canvas;
    public GameObject hud;

    public int itemCode;
    public int itemCount;

    public Image portrait;

    public string getName()
    {
        return objectName;
    }

    private void Start()
    {
        questStart = new List<int>();
        questEnd = new List<int>();
        tempQuestStart = new List<int>();
        tempQuestEnd = new List<int>();
        isChangeData = false;

        if (isNpc)
        {
            newQuest = transform.GetChild(0).gameObject;
            doneQuest = transform.GetChild(1).gameObject;
            newQuest.SetActive(false);
            doneQuest.SetActive(false);
        }

        isCollecting = false;

        //putQuestToEntity();
    }

    public void setNewQuestOn()
    {
        newQuest.SetActive(true);
    }

    public void setNewQuestOff()
    {
        newQuest.SetActive(false);
    }

    public void setDoneQuestOn()
    {
        setNewQuestOff();
        doneQuest.SetActive(true);
    }

    public void setDoneQuestOff()
    {
        doneQuest.SetActive(false);
    }

    public void refresh()
    {
        if (!isChangeData)
        {
            return;
        }

        questStart = tempQuestStart;
        tempQuestStart = new List<int>();
        questEnd = tempQuestEnd;
        tempQuestEnd = new List<int>();

        isChangeData = false;
    }

    public void scanObjectData()
    {
        SoundManager.instance.PlayButtonEffectSound();
        PlayerManager.instance.progressTalk(gameObject);

        if (portrait != null)
        {
            GameObject.Find("PortraitNPC").GetComponent<Image>().sprite = portrait.sprite;
            GameObject.Find("PortraitNPC").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            GameObject.Find("PortraitNPC").GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void questTalkStart()
    {
        Debug.Log("퀘스트 대화 버튼 누름 : " + getName());
        GameManager.instance.setNPCName(getName());
        GameManager.instance.action(gameObject);
        GameManager.instance.objectData = this;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        questTalkStart();
    }

    /*    public void putQuestToEntity()
        {
            for (int i = 0; i < QuestDatabase.instance.questDB.Count; i++)
            {
                if (QuestDatabase.instance.questDB[i].npcIdStart == id)
                {
                    questStart.Add(QuestDatabase.instance.questDB[i]);
                }

                if (QuestDatabase.instance.questDB[i].npcIdEnd == id)
                {
                    questEnd.Add(QuestDatabase.instance.questDB[i]);
                }
            }
        }*/
}
