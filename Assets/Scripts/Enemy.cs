using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rigidbody;

    public Animator arrowUI;
    public Animator animator;

    public int transformIndex;
    public float cooldown;
    public float castingTimer;
    public bool isCasting;
    //public Action action;
    public GameObject target;
    //public Skill skill;
    public List<GameObject> targets;
    public bool isDead;

    public float playerX;
    public float playerY;
    public int questId;
    public int questActionIndex;
    public string entityName;
    //public Job job;
    //public Element element;
    public int level;
    public int exp = 5;
    public int nextExp;
    public int money;
    public int gold;
    public int inventorySize;
    //public List<QuestInformation> questInformation;
    //public List<Item> items;
    //public Item[] equipments;

    //Quest
    public List<int> startQuest;
    //public List<Quest> currentQuest;
    public List<int> clearQuest;

    //Stats
    public float power;
    public float armor;
    public float accuracy;
    public float avoid;
    public float critRate;
    public float critDam;
    public float penetration;
    public float patience;

    public float healthPoint;
    public float healthPointMax;
    public float manaPoint;
    public float manaPointMax;

    public float expEff;

    // public int sortingIndex;

    // HealthBar text
    public int damagedTimer;
    public GameObject healthBarBackground;
    //public Image healthBar;
    public float delayHP;

    // Damage text
    public GameObject hudDamageText;
    public Transform hudPos;

    // Skill
    //public List<Skill> skills;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(ActionType actionType)
    {
        SoundManager.instance.PlayOneShotEffectSound(1);

        if (actionType != ActionType.ATTACK4)
        {
            rigidbody.AddForce(new Vector3(80, 100));
        }
        else
        {
            rigidbody.AddForce(new Vector3(400, 250));
        }

        ChangeTagWhenHit();
    }

    private void ChangeTagWhenHit()
    {
        gameObject.tag = "DamagedEnemy";
        Invoke("ChangeTagOriginalState", 0.35f);
    }

    private void ChangeTagOriginalState()
    {
        gameObject.tag = "Enemy";
        CancelInvoke("ChangeTagOriginalState");
    }

    /*public float getElementResult(Skill skill, BattleEntity entityData)
    {
        switch (skill.element)
        {
            case Element.FIRE:
                if (entityData.element == Element.ICE)
                {
                    return 1.25f;
                }
                else if (entityData.element == Element.EARTH)
                {
                    return 0.75f;
                }
                break;

            case Element.ICE:
                if (entityData.element == Element.EARTH)
                {
                    return 1.25f;
                }
                else if (entityData.element == Element.FIRE)
                {
                    return 0.75f;
                }
                break;

            case Element.EARTH:
                if (entityData.element == Element.FIRE)
                {
                    return 1.25f;
                }
                else if (entityData.element == Element.ICE)
                {
                    return 0.75f;
                }
                break;
        }

        return 1f;
    }*/
}
