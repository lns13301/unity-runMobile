using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Map location;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    private AudioSource playerAudioSource;

    // attack
    public Transform posRight;
    public Transform posLeft;
    public GameObject monster;
    public Vector2 boxSize;
    public int damageTimer;

    //levelUp
    public GameObject hudDamageText;
    public GameObject hudLevelUpText;
    public Transform hudPos;
    public bool isLevelUp;
    public bool isPointUp;

    //damaged
    private bool isMiss;
    public StatUI statUI;

    // Quest
    public GameObject scanObject;

    //equipment
/*    public GameObject leftHand;
    public GameObject[] leftHands;
    public int activeLeftHand;

    public GameObject rightHand;
    public GameObject[] rightHands;
    public int activeRightHand;

    public GameObject head;
    public GameObject[] heads;
    public int activeHead;

    public GameObject top;
    public GameObject[] tops;
    public int activeTop;

    public GameObject pant;
    public GameObject[] pants;
    public int activePants;

    public GameObject glove;
    public GameObject[] gloves;
    public int activeGloves;

    public GameObject shoe;
    public GameObject[] shoes;
    public int activeShoes;

    public GameObject neckless;
    public GameObject[] necklesses;
    public int activeNeckless;

    public GameObject earing;
    public GameObject[] earings;
    public int activeEaring;

    public GameObject ring;
    public GameObject[] rings;
    public int activeRing;

    public GameObject hair;
    public GameObject[] hairs;
    public int activeHair;*/

    [SerializeField] private AudioClip[] clip;

    // Object Controller
    public PlayerAction playerAction;

    private void Awake()
    {
        playerAudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        location = Map.VILLAGE;

        //rigid = GetComponent<Rigidbody2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
        //statUI = GameObject.Find("Canvas").GetComponent<StatUI>();
        damageTimer = 0;


        /*        setEquipmentGameObject();

                activeLeftHand = -1;
                activeRightHand = -1;
                activeHead = -1;
                activeTop = -1;
                activePants = -1;
                activeGloves = -1;
                activeShoes = -1;
                activeNeckless = -1;
                activeEaring = -1;
                activeRing = -1;
                activeHair = -1;*/

        playerAction = GetComponent<PlayerAction>();
    }

    // Update is called once per frame
    void Update()
    {
        //setEquipmentMotion();

        //levelUpSign
        if (isLevelUp)
        {
            Debug.Log("실행됨");
            SoundManager.instance.PlayEffectSound(5);
            GameObject hudText = Instantiate(hudLevelUpText);
            hudText.transform.position = hudPos.position;
            isLevelUp = false;
        }

        if (isPointUp)
        {
            playerAudioSource.clip = clip[4];
            playerAudioSource.Play();
            isPointUp = false;
        }
    }

    // 장비 아이템 모션 적용
    /*    public void setEquipmentMotion()
        {
            if (spriteRenderer.flipX)
            {
                if (activeLeftHand != -1)
                {
                    leftHands[activeLeftHand].GetComponent<SpriteRenderer>().flipX = true;
                }
                if (activeRightHand != -1)
                {
                    rightHands[activeRightHand].GetComponent<SpriteRenderer>().flipX = true;
                }
                if (activeHead != -1)
                {
                    heads[activeHead].GetComponent<SpriteRenderer>().flipX = true;
                }
                if (activeTop != -1)
                {
                    tops[activeTop].GetComponent<SpriteRenderer>().flipX = true;
                }
                if (activePants != -1)
                {
                    pants[activePants].GetComponent<SpriteRenderer>().flipX = true;
                }
                if (activeGloves != -1)
                {
                    gloves[activeGloves].GetComponent<SpriteRenderer>().flipX = true;
                }
                if (activeShoes != -1)
                {
                    shoes[activeShoes].GetComponent<SpriteRenderer>().flipX = true;
                }
                if (activeNeckless != -1)
                {
                    necklesses[activeNeckless].GetComponent<SpriteRenderer>().flipX = true;
                }
                if (activeEaring != -1)
                {
                    earings[activeEaring].GetComponent<SpriteRenderer>().flipX = true;
                }
                if (activeRing != -1)
                {
                    rings[activeRing].GetComponent<SpriteRenderer>().flipX = true;
                }
                if (activeHair != -1)
                {
                    hairs[activeHair].GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            else
            {
                if (activeLeftHand != -1)
                {
                    leftHands[activeLeftHand].GetComponent<SpriteRenderer>().flipX = false;
                }
                if (activeRightHand != -1)
                {
                    rightHands[activeRightHand].GetComponent<SpriteRenderer>().flipX = false;
                }
                if (activeHead != -1)
                {
                    heads[activeHead].GetComponent<SpriteRenderer>().flipX = false;
                }
                if (activeTop != -1)
                {
                    tops[activeTop].GetComponent<SpriteRenderer>().flipX = false;
                }
                if (activePants != -1)
                {
                    pants[activePants].GetComponent<SpriteRenderer>().flipX = false;
                }
                if (activeGloves != -1)
                {
                    gloves[activeGloves].GetComponent<SpriteRenderer>().flipX = false;
                }
                if (activeShoes != -1)
                {
                    shoes[activeShoes].GetComponent<SpriteRenderer>().flipX = false;
                }
                if (activeNeckless != -1)
                {
                    necklesses[activeNeckless].GetComponent<SpriteRenderer>().flipX = false;
                }
                if (activeEaring != -1)
                {
                    earings[activeEaring].GetComponent<SpriteRenderer>().flipX = false;
                }
                if (activeRing != -1)
                {
                    rings[activeRing].GetComponent<SpriteRenderer>().flipX = false;
                }
                if (activeHair != -1)
                {
                    hairs[activeHair].GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        }

        private void setEquipmentGameObject()
        {
            for (int i = 0; i < leftHands.Length; i++)
            {
                leftHands[i] = leftHand.transform.GetChild(i).gameObject;
            }

            for (int i = 0; i < rightHands.Length; i++)
            {
                rightHands[i] = rightHand.transform.GetChild(i).gameObject;
            }

            for (int i = 0; i < heads.Length; i++)
            {
                heads[i] = head.transform.GetChild(i).gameObject;
            }

            for (int i = 0; i < tops.Length; i++)
            {
                tops[i] = top.transform.GetChild(i).gameObject;
            }

            for (int i = 0; i < pants.Length; i++)
            {
                pants[i] = pant.transform.GetChild(i).gameObject;
            }

            for (int i = 0; i < gloves.Length; i++)
            {
                gloves[i] = glove.transform.GetChild(i).gameObject;
            }

            for (int i = 0; i < shoes.Length; i++)
            {
                shoes[i] = shoe.transform.GetChild(i).gameObject;
            }

            for (int i = 0; i < necklesses.Length; i++)
            {
                necklesses[i] = neckless.transform.GetChild(i).gameObject;
            }

            for (int i = 0; i < earings.Length; i++)
            {
                earings[i] = earing.transform.GetChild(i).gameObject;
            }

            for (int i = 0; i < rings.Length; i++)
            {
                rings[i] = ring.transform.GetChild(i).gameObject;
            }

            for (int i = 0; i < hairs.Length; i++)
            {
                hairs[i] = hair.transform.GetChild(i).gameObject;
            }
        }*/

    void onDamaged(Vector2 targetPos, bool isMiss, float damageValue)
    {
        statUI.isDataChanged = true;

        // playerAudioSource.clip = clip[3];
        //playerAudioSource.Play();
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = hudPos.position;

        if (damageValue <= 0 || isMiss)
        {
            hudText.GetComponent<DamageText>().damage = 0;
            gameObject.layer = 15;
            Invoke("offDamage", 3);

            return;
        }

        GameManager.instance.playerData.healthPoint -= damageValue;

        hudText.GetComponent<DamageText>().damage = (int)damageValue;

        if (!isMiss)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            int dirc = transform.position.x - targetPos.y > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(dirc, 1) * 100, ForceMode2D.Impulse);
        }

        Invoke("offDamage", 3);
    }

    void offDamage()
    {
        gameObject.layer = 12;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public bool isHit(int accuracy)
    {
        int hit = accuracy - GameManager.instance.playerData.avoid;

        if (hit <= 0)
        {
            return false;
        }

        if (Random.Range(0, 20) > hit)
        {
            return false;
        }

        return true;
    }

    public void progressTalk(GameObject gameObject)
    {
        if (scanObject != null)
        {
            scanObject = gameObject;
            GameManager.instance.setNPCName(scanObject.GetComponent<ObjectData>().getName());
            GameManager.instance.action(scanObject);
        }

        if (scanObject == null && GameManager.instance.isAction)
        {
            GameManager.instance.action();
        }
    }

    public void progressTalk()
    {
        if (GameManager.instance.isAction)
        {
            GameManager.instance.action();
        }
    }

    public void questTalkStart()
    {
        GameManager.instance.setNPCName(scanObject.GetComponent<ObjectData>().getName());
        GameManager.instance.action(scanObject);
    }
}
