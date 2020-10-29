using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private Animator animator;
    public bool isUIOn;
    private int menuButtonChildCount;

    public GameObject heroUI;

    public bool isPause = false;
    public bool isHeroUIOn = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
        isUIOn = false;
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
        if (heroUI.activeSelf && isHeroUIOn)
        {
            isHeroUIOn = false;
            HeroUI.instance.illustAnimator.SetTrigger("doUIOff");
            DoPause(false);
            Invoke("OffHeroUI", 0.3f);
        }
        else
        {
            isHeroUIOn = true;
            heroUI.SetActive(true);
            HeroUI.instance.heroUIAnimator.SetTrigger("doUIOn");
            Invoke("OnHeroIllust", 0.3f);
        }
    }

    public void OnHeroIllust()
    {
        HeroUI.instance.illustAnimator.SetTrigger("doHeroIllustOn");
        DoPause(true);

        CancelInvoke("OnCharacterIllust");
    }

    public void OffHeroUI()
    {
        heroUI.SetActive(false);

        CancelInvoke("OffHeroUI");
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
