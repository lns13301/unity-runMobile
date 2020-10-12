using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTitleButton : MonoBehaviour
{
    public int questId;

    private void Start()
    {
        // transform.Find("Mark").Find("Done").gameObject.SetActive(false);
    }

    public void showQuestInformation()
    {
        QuestUI.instance.showQuestInformationPanel(questId);
    }

    public void setStateDone()
    {
        transform.Find("Mark").Find("Start").gameObject.SetActive(false);
        transform.Find("Mark").Find("Done").gameObject.SetActive(true);
    }

    public void setStateStart()
    {
        transform.Find("Mark").Find("Start").gameObject.SetActive(true);
        transform.Find("Mark").Find("Done").gameObject.SetActive(false);
    }
}
