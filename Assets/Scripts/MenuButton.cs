using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private Animator animator;
    private Animator heroUIAnimator;
    public bool isUIOn;
    private int menuButtonChildCount;

    public GameObject heroUI;

    public bool isPause = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
        heroUIAnimator = heroUI.GetComponent<Animator>();
        isUIOn = false;

        // Invoke("AddTestHero", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonMenu()
    {
        if (isUIOn)
        {
            animator.SetBool("isMenuOn", false);
            isUIOn = false;
        }
        else
        {
            animator.SetBool("isMenuOn", true);
            isUIOn = true;
        }
    }

    // 영웅 정보창
    public void ButtonHeroUIOnOff()
    {
        if (heroUI.activeSelf)
        {
            heroUI.SetActive(false);
            DoPause(false);
        }
        else
        {
            heroUI.SetActive(true);
            Invoke("OnCharacterIllust", 0.3f);
        }
    }

    public void OnCharacterIllust()
    {
        heroUIAnimator.SetTrigger("doCharacterIllustOn");
        heroUIAnimator.SetTrigger("doUIOn");
        DoPause(true);

        CancelInvoke("OnCharacterIllust");
    }

    public void AddTestHero()
    {
        Debug.Log("추가");
        HeroInventory.instance.addHero(GameManager.instance.playerData.heroDatas[0]);

        CancelInvoke("AddTestHero");
    }

    public void DoPause(bool isPause)
    {
        this.isPause = isPause;

        Time.timeScale = (isPause) ? 0.0f : 1.0f;

        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if (isPause)
        {
            GetComponent<MonoBehaviour>().enabled = true;
        }
    }
}
