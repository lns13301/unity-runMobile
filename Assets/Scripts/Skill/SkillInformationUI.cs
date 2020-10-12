using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInformationUI : MonoBehaviour
{
    public Text skillName;
    public Text skillLevel;
    public Text skillExp;
    public Text content;
    public Image skillImage;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
