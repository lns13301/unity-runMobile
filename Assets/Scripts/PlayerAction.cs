using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public static PlayerAction instance;
    public Animator animator;
    public ActionType actionType;
    public bool isAttackButtonPressing;

    public Animator mapAnimator;
    public float defaultMapSpeed;

    // Range
    public GameObject boxRange;
    public GameObject groundRange;
    private Vector3 boxRangeBasePosition;
    private Vector3 boxRangeBaseSize;
    public bool doAttack;

    public Rigidbody2D rigidbody;
    public int jumpCount;
    public bool isJumping;

    // Skill
    public float skillCooldown;
    public float skillFinish;
    public float skillFinishGravityTime;
    public bool doSkill;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        actionType = ActionType.RUN;

        // 애니메이션 등록
        animator = transform.GetChild(1).GetComponent<Animator>();

        mapAnimator = GameObject.Find("ForestMap").GetComponent<Animator>();
        defaultMapSpeed = 1f;

        boxRange = transform.GetChild(0).GetChild(0).gameObject;
        groundRange = transform.GetChild(0).GetChild(1).gameObject;

        // 공격 범위 기본 세팅
        boxRangeBasePosition = boxRange.transform.position;
        boxRangeBaseSize = boxRange.transform.localScale;

        // 강체
        rigidbody = GetComponent<Rigidbody2D>();
        jumpCount = 0;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetBaseState();
        UpdateAttackState();
        SetAnimationState(actionType);
        UpdateRangePosition();
    }

    private void FixedUpdate()
    {
        UpdateCooldown();
    }

    private void OnPlayerWalk(Animator animator, string animationName)
    {
        animator.SetBool(animationName, true);
    }

    public void SetActionType(int value)
    {
        if (!IsCurrentAnimation(animator, "PlayerRun"))
        {
            return;
        }

        switch (value)
        {
            case 0:
                actionType = ActionType.NONE;
                break;
            case 1:
                actionType = ActionType.IDLE;
                break;
            case 2:
                actionType = ActionType.WALK;
                break;
            case 3:
                actionType = ActionType.RUN;
                break;
            case 4:
                actionType = ActionType.JUMP;
                break;
            case 5:
                actionType = ActionType.DOUBLEJUMP;
                break;
            case 6:
                actionType = ActionType.TRIPLEJUMP;
                break;
            case 7:
                actionType = ActionType.UNDERJUMP;
                break;
            case 8:
                actionType = ActionType.ATTACK;
                break;
            case 9:
                actionType = ActionType.ATTACK1;
                break;
            case 10:
                actionType = ActionType.ATTACK2;
                break;
            case 11:
                actionType = ActionType.ATTACK3;
                break;
            case 12:
                actionType = ActionType.ATTACK4;
                break;
            case 13:
                actionType = ActionType.ATTACK5;
                break;
            case 14:
                ButtonSkill(); // ActionType.SKILL and ActionType.SKILLFINISH
                break;
            case 15:
                actionType = ActionType.SKILL1;
                break;
            case 16:
                actionType = ActionType.SKILL2;
                break;
            case 17:
                actionType = ActionType.ULTIMATE;
                break;
        }
    }

    public void SetBaseState()
    {
        if (actionType == ActionType.NONE)
        {
            actionType = ActionType.RUN;
        }

        if (actionType == ActionType.RUN)
        {
            ReSetTriggers();
        }
    }

    public void SetAnimationState(ActionType actionType)
    {
        switch (actionType)
        {
            case ActionType.RUN:
                animator.SetBool("isRunning", true);
                ReSetTriggers();
                mapAnimator.SetFloat("GroundSpeed", defaultMapSpeed);
                break;
            case ActionType.ATTACK:
                animator.SetTrigger("doAttack");
                mapAnimator.SetFloat("GroundSpeed", 0.3f);
                break;
            case ActionType.ATTACK1:
                animator.SetTrigger("doAttack1");
                break;
            case ActionType.ATTACK2:
                animator.SetTrigger("doAttack2");
                break;
            case ActionType.ATTACK3:
                animator.SetTrigger("doAttack3");
                break;
            case ActionType.ATTACK4:
                animator.SetTrigger("doAttack4");
                break;
            case ActionType.SKILL:
                animator.SetTrigger("doSkill");
                break;
            case ActionType.SKILLFINISH:
                animator.SetTrigger("doSkill0-2");
                break;
        }
    }

    public void ReSetTriggers()
    {
        animator.ResetTrigger("doAttack");
        animator.ResetTrigger("doAttack1");
        animator.ResetTrigger("doAttack2");
        animator.ResetTrigger("doAttack3");
        animator.ResetTrigger("doAttack4");
        animator.ResetTrigger("doSkill");
        animator.ResetTrigger("doSkill0-2");
    }

    public void UpdateRangePosition()
    {
        boxRange.transform.position = new Vector2(boxRangeBasePosition.x, transform.position.y - 0.1f);
    }

    // 제거 해야할 것
    private void SetBoxRangeStart(Vector3 position, Vector3 colliderSize)
    {
        boxRange.SetActive(true);
        boxRange.transform.position = position;
        boxRange.GetComponent<BoxCollider2D>().size = colliderSize;
    }

    // 제거 해야할 것
    private void SetBoxRangeEnd()
    {
        boxRange.SetActive(false);
        boxRange.transform.position = boxRangeBasePosition;
        boxRange.GetComponent<BoxCollider2D>().size = boxRangeBaseSize;
    }

    public void ButtonJump()
    {
        if (jumpCount > 1)
        {
            return;
        }
        actionType = ActionType.JUMP;

        SoundManager.instance.PlayOneShotEffectSound(2);
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(new Vector3(0, 500, 0));
        ChangeAnimation("isJumping");
        jumpCount++;

        mapAnimator.SetFloat("GroundSpeed", 0.6f);

        Invoke("SetActiveGroundRange", 0.15f);
    }

    public void ButtonSkill()
    {
        if (skillCooldown > 0)
        {
            if (skillFinish > 0)
            {
                actionType = ActionType.SKILLFINISH;
                skillFinish = 0;
            }

            return;
        }
        actionType = ActionType.SKILL;

        skillCooldown = 4.0f;
    }

    private void ChangeAnimation(string name, bool isTrue = true)
    {
        animator.SetBool(name, isTrue);
        animator.SetBool("isRunning", !isTrue);
    }

    public void SetLanding()
    {
        ChangeAnimation("isJumping", false);
        jumpCount = 0;
        mapAnimator.SetFloat("GroundSpeed", defaultMapSpeed);

        // Invoke("LandingDelay", 0.5f);

        animator.SetBool("isChopping", false);
        isJumping = false;

        CancelInvoke("SetActiveGroundRange");
    }

    public void LandingDelay()
    {
        actionType = ActionType.RUN;
        CancelInvoke("LandingDelay");
    }

    private void SetActiveGroundRange()
    {
        isJumping = true;
    }

    public void DoChopping()
    {
        if (!isJumping || actionType == ActionType.CHOPPING)
        {
            return;
        }

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(new Vector3(0, 300, 0));
        mapAnimator.SetFloat("GroundSpeed", 0.1f);
        animator.SetBool("isChopping", true);
        actionType = ActionType.CHOPPING;
        jumpCount += 2;

        Invoke("AddChoppingGravity", 0.5f);
    }

    public void AddChoppingGravity()
    {
        rigidbody.AddForce(new Vector3(0, -500, 0));
        CancelInvoke("AddChoppingGravity");
    }

    private void UpdateJumpState()
    {

    }

    private void UpdateCooldown()
    {
        if (skillCooldown > 0)
        {
            skillCooldown -= Time.fixedDeltaTime;
        }

        if (skillFinish > 0)
        {
            skillFinish -= Time.fixedDeltaTime;
        }
    }

    private void UpdateAttackState()
    {
        float value = GetAnimationNormalizedTime();

        /*        if (boxRange.activeSelf)
                {
                    SetBoxRangeEnd();
                }*/

        if (isAttackButtonPressing)
        {
            DoChopping();
        }

        switch (actionType)
        {
            case ActionType.ATTACK:
                if (IsCurrentAnimation(animator, "Attack"))
                {
                    // 공격 범위 처리
                    UpdateAttackAction(value);
                    doSkill = false;
                    skillFinish = 0;

                    if (isAttackButtonPressing && value >= 0.4f && value <= 0.7f)
                    {
                        actionType = ActionType.ATTACK1;
                    }
                    else if (value > 0.95f)
                    {
                        UpdateAttackAndSkillFinish();
                    }
                }
                break;
            case ActionType.ATTACK1:
                if (IsCurrentAnimation(animator, "Attack 1"))
                {
                    // 공격 범위 처리
                    UpdateAttackAction(value);

                    if (isAttackButtonPressing && value >= 0.35f && value <= 0.7f)
                    {
                        actionType = ActionType.ATTACK2;
                    }
                    else if (value > 0.95f)
                    {
                        UpdateAttackAndSkillFinish();
                    }
                }
                break;
            case ActionType.ATTACK2:
                if (IsCurrentAnimation(animator, "Attack 2"))
                {
                    // 공격 범위 처리
                    UpdateAttackAction(value);

                    if (isAttackButtonPressing && value >= 0.35f && value <= 0.75f)
                    {
                        actionType = ActionType.ATTACK3;
                    }
                    else if (value > 0.95f)
                    {
                        UpdateAttackAndSkillFinish();
                    }
                }
                break;
            case ActionType.ATTACK3:
                if (IsCurrentAnimation(animator, "Attack 3"))
                {
                    // 공격 범위 처리
                    UpdateAttackAction(value);

                    if (isAttackButtonPressing && value >= 0.55f && value <= 0.8f)
                    {
                        actionType = ActionType.ATTACK4;
                    }
                    else if (value > 0.95f)
                    {
                        UpdateAttackAndSkillFinish();
                    }
                }
                break;
            case ActionType.ATTACK4:
                if (IsCurrentAnimation(animator, "Attack 4"))
                {
                    // 공격 범위 처리
                    UpdateAttackAction(value, 0.8f, 0.95f);

                    if (value > 0.95f)
                    {
                        UpdateAttackAndSkillFinish();
                    }
                }
                break;
            case ActionType.SKILL:
                if (IsCurrentAnimation(animator, "Skill"))
                {
                    // 공격 범위 처리
                    UpdateSkillAction(value);
                    doAttack = false;

                    if (value > 0.75f)
                    {
                        mapAnimator.SetFloat("GroundSpeed", defaultMapSpeed);
                        skillFinish = 2.5f;
                        skillFinishGravityTime = 2f;
                        UpdateAttackAndSkillFinish();
                    }
                }
                break;
            case ActionType.SKILLFINISH:
                if (IsCurrentAnimation(animator, "Skill 0-2"))
                {
                    // 공격 범위 처리
                    UpdateSkillAction(value, 0.45f, 0.6f, 3f);
                    rigidbody.AddForce(new Vector2(0, 20) * skillFinishGravityTime);
                    
                    if (skillFinishGravityTime > 0)
                    {
                        skillFinishGravityTime -= Time.fixedDeltaTime;
                    }

                    doAttack = false;

                    if (value > 0.8f)
                    {
                        mapAnimator.SetFloat("GroundSpeed", defaultMapSpeed);
                        UpdateAttackAndSkillFinish();
                    }
                }
                break;
        }

        // isAttackButtonPressing = false;
    }

    private void UpdateAttackAndSkillFinish()
    {
        actionType = ActionType.RUN;
        doAttack = false;
        doSkill = false;
    }

    private void UpdateAttackAction(float value, float startValue = 0.3f, float EndValue = 0.4f)
    {
        // 공격 범위 처리
        if (value >= startValue && value < EndValue)
        {
            // SetBoxRangeStart(GetBoxAttackRagnePosition(), boxRangeBaseSize);
            doAttack = true;
        }
    }

    private void UpdateSkillAction(float value, float startValue = 0.3f, float EndValue = 0.4f, float groundSpeed = 0.3f)
    {
        // 공격 범위 처리
        if (value >= startValue && value < EndValue)
        {
            mapAnimator.SetFloat("GroundSpeed", groundSpeed);
            doSkill = true;
        }
    }

    private Vector2 GetBoxAttackRagnePosition()
    {
        return transform.position;
    }

    public bool IsCurrentAnimation(Animator animator, string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    public float GetAnimationNormalizedTime()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}

public enum ActionType
{
    NONE,
    IDLE,
    WALK,
    RUN,
    JUMP,
    DOUBLEJUMP, // 5
    TRIPLEJUMP,
    UNDERJUMP,
    ATTACK,
    ATTACK1,
    ATTACK2, // 10
    ATTACK3,
    ATTACK4,
    ATTACK5,
    SKILL,
    SKILL1, // 15
    SKILL2,
    ULTIMATE,
    CHOPPING,
    FLY,
    SKILLFINISH // 20
}
