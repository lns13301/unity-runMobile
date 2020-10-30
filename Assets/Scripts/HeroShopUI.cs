using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroShopUI : MonoBehaviour
{
    public GameObject heroShopSet;

    public Text jewelText;

    // Start is called before the first frame update
    void Start()
    {
        heroShopSet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UIOnOff()
    {
        jewelText.text = formattingNumber(GameManager.instance.playerData.money);

        // SoundManager.instance.PlayButtonEffectSound();

        if (heroShopSet.activeSelf)
        {
            heroShopSet.SetActive(false);
        }
        else
        {
            heroShopSet.SetActive(true);
        }
    }

    public void DoPickUpOne()
    {
        if (GameManager.instance.playerData.money < 100)
        {
            return;
        }

        GameManager.instance.playerData.money -= 100;
        jewelText.text = formattingNumber(GameManager.instance.playerData.money);

        Rating rating = GetGrade();

        // 나중에 등급별로 db 나눠서 랜덤돌려야함
        EntityData hero = GetHero(rating);
        HeroInventory.instance.AddHero(hero);
    }

    public void DoPickUpTen()
    {

    }

    private Rating GetGrade()
    {
        int percentage = Random.Range(0, 10000);

        if (percentage < 100)
        {
            return Rating.MASTER;
        }
        else if (percentage < 600)
        {
            return Rating.SENIER;
        }
        else if (percentage < 1600)
        {
            return Rating.EXPERT;
        }
        else if (percentage < 4600)
        {
            return Rating.BEGINNER;
        }

        return Rating.APPRENTICE;
    }
    
    private EntityData GetHero(Rating rating)
    {
        int index = Random.Range(0, HeroDatabase.instance.heroDB.Count);

        return HeroDatabase.instance.heroDB[index];
    }

    public string formattingNumber(long num)
    {
        string result = "";

        if (num < 1000)
        {
            return "" + num;
        }
        else
        {
            return formattingNumber(num, result);
        }
    }

    private string formattingNumber(long num, string result)
    {
        if (num >= 1000)
        {
            if (num % 1000 < 10)
            {
                result = ",00" + num % 1000 + result;
                return formattingNumber(num / 1000, result);
            }
            else if (num % 1000 < 100)
            {
                result = ",0" + num % 1000 + result;
                return formattingNumber(num / 1000, result);
            }
            else
            {
                result = "," + num % 1000 + result;
                return formattingNumber(num / 1000, result);
            }
        }
        else
        {
            return num + result;
        }
    }
}
