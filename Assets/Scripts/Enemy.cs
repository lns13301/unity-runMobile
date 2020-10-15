using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EntityData entityData;

    public Rigidbody2D rigidbody;

    // public int sortingIndex;

    // HealthBar text
    public int damagedTimer;
    public GameObject healthBarBackground;
    public Image healthBar;
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
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, entityData.healthPoint / entityData.healthPointMax, Time.deltaTime * 3f);
    }

    public void SetEntityData(EntityData entityData)
    {
        this.entityData = entityData;
    }

    public void TakeDamage(ActionType actionType, EntityData heroData)
    {
        SoundManager.instance.PlayOneShotEffectSound(1);

        ChangeTagWhenHit();

        if (actionType == ActionType.CHOPPING)
        {
            rigidbody.AddForce(new Vector3(800, 500));
        }
        else if (actionType != ActionType.ATTACK4)
        {
            rigidbody.AddForce(new Vector3(100, 100));
        }
        else if (PlayerAction.instance.IsCurrentAnimation(PlayerAction.instance.animator, "Attack 4"))
        {
            rigidbody.AddForce(new Vector3(400, 250));
        }

        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = hudPos.position;

        int avoidValue = entityData.avoid - heroData.accuracy;
        int damage = -((entityData.armor - heroData.power * Random.Range(600, 1010)) / 1000);

        if (damage <= 0)
        {
            hudText.GetComponent<DamageText>().damage = 0;
            return;
        }

        if (avoidValue > 0)
        {
            if (Random.Range(0, 30) < avoidValue)
            {
                hudText.GetComponent<DamageText>().damage = 0;
                return;
            }
        }

        GameObject.Find("Player Camera").GetComponent<MainCamera>().SetCameraShake();

        try
        {
            // animator.SetTrigger("doDamaged");
            // animator.SetTrigger("doCriticalDamaged");
        }
        catch (System.Exception)
        {
            Debug.Log("등록되지 않은 동작입니다.");
        }

        entityData.healthPoint -= damage;
        damagedTimer = 30;
        SummonEffect(entityData);
        healthBarBackground.SetActive(true);

        hudText.GetComponent<DamageText>().damage = damage;

        /*else if (action == Action.MAGIC)
        {
            int avoidValue = avoid - battleEntity.accuracy;
            int damage = -((armor - (int)(battleEntity.skill.magicPower * Random.Range(600, 1010)) / 1000));
            battleEntity.manaPoint -= battleEntity.skill.costMP;

            if (damage <= 0)
            {
                hudText.GetComponent<DamageText>().damage = 0;
                return;
            }

            if (avoidValue > 0)
            {
                if (Random.Range(0, 30) < avoidValue)
                {
                    hudText.GetComponent<DamageText>().damage = 0;
                    return;
                }
            }

            GameObject.Find("Main Camera").GetComponent<MainCamera>().setCameraShake();

            try
            {
                animator.SetTrigger("doDamaged");
                // animator.SetTrigger("doCriticalDamaged");
            }
            catch (System.Exception)
            {
                Debug.Log("등록되지 않은 동작입니다.");
            }

            summonEffect(battleEntity);

            damage = (int)Mathf.Round(damage * getElementResult(battleEntity.skill, this));
            healthPoint -= damage;
            damagedTimer = 30;
            // healthBar.fillAmount = healthPoint / healthPointMax;
            healthBarBackground.SetActive(true);

            hudText.GetComponent<DamageText>().damage = damage;

            if (damage > 0 && isCasting)
            {
                Debug.Log("캐스팅 취소");
                BattleManager.instance.cancleCasting(transformIndex);
            }
        }*/


        if (entityData.healthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void ChangeTagWhenHit()
    {
        // 해당 부분을 나중에 공격마다 쿨타임 지정해서 변수로 줘야할 듯
        gameObject.tag = "DamagedEnemy";
        Invoke("ChangeTagOriginalState", 0.38f);
    }

    private void ChangeTagOriginalState()
    {
        gameObject.tag = "Enemy";
        CancelInvoke("ChangeTagOriginalState");
    }

    private void ChangeLayerWhenHit()
    {
        gameObject.layer = 10;
        //Invoke("ChangeLayerOriginalState", 0.3f);
    }

    private void ChangeLayerOriginalState()
    {
        gameObject.layer = 9;
        CancelInvoke("ChangeLayerOriginalState");
    }

    public float GetElementResult(Skill skill, EntityData entityData)
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
    }

    private void SummonEffect(EntityData entityData)
    {
        ParticleManager.instance.CreateEffect(transform.position, gameObject, 1);
        // 임시 마법 이펙트
        /*        switch (entityData.skill.skillId)
                {
                    case 0:
                        EffectManager.instance.createEffect(transform.position, gameObject, 4);
                        SoundManager.instance.PlayEffectSound(9);
                        break;
                    case 100:
                        EffectManager.instance.createEffect(transform.position, gameObject, 3);
                        SoundManager.instance.PlayEffectSound(7);
                        break;
                    case 103:
                        EffectManager.instance.createEffect(transform.position, gameObject, 5);
                        SoundManager.instance.PlayEffectSound(7);
                        break;
                    default:
                        EffectManager.instance.createEffect(transform.position, gameObject, 6);
                        SoundManager.instance.PlayEffectSound(12);
                        break;
                }*/
    }

/*    public Skill findSkillById(int id)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].skillId == id)
            {
                return skills[i];
            }
        }

        return null;
    }*/
}
