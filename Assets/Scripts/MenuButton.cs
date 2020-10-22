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

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
        heroUIAnimator = heroUI.GetComponent<Animator>();
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

    public void ButtonHeroUIOnOff()
    {
        if (heroUI.activeSelf)
        {
            heroUI.SetActive(false);
        }
        else
        {
            heroUI.SetActive(true);
            heroUIAnimator.SetTrigger("doUIOn");
            Invoke("OnCharacterIllust", 0.5f);
        }
    }

    public void OnCharacterIllust()
    {
        heroUIAnimator.SetTrigger("doCharacterIllustOn");
        CancelInvoke("OnCharacterIllust");
    }
}
