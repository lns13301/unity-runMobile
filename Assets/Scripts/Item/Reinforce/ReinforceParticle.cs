using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReinforceParticle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int particleNumber;

    public Animator animator;
    public Rigidbody2D rigid;
    private float moveHorizontal;

    public float jumpPower;

    public GameObject hudScoreText;
    public Transform hudPos;

    public string scoreType;

    private bool isBtnDown = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (touchScoreStart())
        {
            gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + jumpPower--);
            if (jumpPower <= 0)
            {
                jumpPower = 0;
            }
        }

        gameObject.transform.position = new Vector2(transform.position.x + moveHorizontal, transform.position.y);

        if (isBtnDown)
        {
            touchDectect();
        }

        if (touchScoreDone())
        {
            scoreType = "MISS";
            GameObject.Find("Canvas").GetComponent<ReinforceUI>().particleScore -= 1;
            setScoreState();

            Destroy(gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isBtnDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isBtnDown = false;
    }

    public void touchDectect()
    {
        SoundManager.instance.PlayEffectSound(0);

        if (touchScorePerfect())
        {
            scoreType = "PERFECT";
            GameObject.Find("Canvas").GetComponent<ReinforceUI>().particleScore += 3;
        }
        else if (touchScoreGreat())
        {
            scoreType = "GREAT";
            GameObject.Find("Canvas").GetComponent<ReinforceUI>().particleScore += 2;
        }
        else if (touchScoreGood())
        {
            scoreType = "GOOD";
            GameObject.Find("Canvas").GetComponent<ReinforceUI>().particleScore += 1;
        }
        else if (touchScoreBad())
        {
            scoreType = "BAD";
        }
        else
        {
            scoreType = "MISS";
            GameObject.Find("Canvas").GetComponent<ReinforceUI>().particleScore -= 1;
        }

        setScoreState();

        Destroy(gameObject);
    }

    public bool touchScoreBad()
    {
        return (animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 82f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) ||
            (animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.28f);
    }

    public bool touchScoreGood()
    {
        return (animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.64f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.82f) ||
            (animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.28f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.43f);
    }

    public bool touchScoreGreat()
    {
        return (animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.57f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.64f) ||
            (animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.43f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f);
    }

    public bool touchScorePerfect()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.57f;
    }

    public bool touchScoreStart()
    {
        return !(animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle")) ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.06f;
    }

    public bool touchScoreDone()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("TouchParticle") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.96f;
    }

    public void setScoreState()
    {
        GameObject hudText = Instantiate(hudScoreText);
        hudText.transform.position = hudPos.position;

        hudText.GetComponent<ReinforceScore>().scoreType = scoreType;
        hudText.transform.SetParent(this.transform.parent);
    }

    public void setNote()
    {
        moveHorizontal = Random.Range(-110, 110) / 10;
        jumpPower = Random.Range(0, 32);
    }
}
