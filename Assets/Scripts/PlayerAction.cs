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

    // Range
    public GameObject boxRange;
    private Vector3 boxRangeBasePosition;
    private Vector3 boxRangeBaseSize;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        actionType = ActionType.RUN;

        // 애니메이션 등록
        animator = transform.GetChild(1).GetComponent<Animator>();

        mapAnimator = GameObject.Find("SampleGround4X").GetComponent<Animator>();

        boxRange = transform.GetChild(0).GetChild(0).gameObject;
        boxRange.SetActive(false);

        // 공격 범위 기본 세팅
        boxRangeBasePosition = new Vector3(-4.5f, -2f, 0);
        boxRangeBaseSize = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        SetBaseState();
        UpdateAttackState();
        SetAnimationState(actionType);
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
                actionType = ActionType.SKILL;
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
                mapAnimator.SetFloat("GroundSpeed", 2.0f);
                break;
            case ActionType.ATTACK:
                animator.SetTrigger("doAttack");
                mapAnimator.SetFloat("GroundSpeed", 0.5f);
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
        }
    }

    public void ReSetTriggers()
    {
        animator.ResetTrigger("doAttack");
        animator.ResetTrigger("doAttack1");
        animator.ResetTrigger("doAttack2");
        animator.ResetTrigger("doAttack3");
        animator.ResetTrigger("doAttack4");
    }

    private void SetBoxRangeStart(Vector3 position, Vector3 colliderSize)
    {
        boxRange.SetActive(true);
        boxRange.transform.position = position;
        boxRange.GetComponent<BoxCollider>().size = colliderSize;
    }

    private void SetBoxRangeEnd()
    {
        boxRange.SetActive(false);
        boxRange.transform.position = boxRangeBasePosition;
        boxRange.GetComponent<BoxCollider>().size = boxRangeBaseSize;
    }

    public void UpdateAttackState()
    {
        float value = GetAnimationNormalizedTime(animator);

        switch (actionType)
        {
            case ActionType.ATTACK:
                if (IsCurrentAnimation(animator, "Attack"))
                {
                    // 공격 범위 처리
                    if (boxRange.activeSelf)
                    {
                        SetBoxRangeEnd();
                    }
                    if (value >= 0.3f && value < 0.4f)
                    {
                        SetBoxRangeStart(boxRangeBasePosition, boxRangeBaseSize);
                    }

                    if (isAttackButtonPressing && value >= 0.4f && value <= 0.7f)
                    {
                        actionType = ActionType.ATTACK1;
                    }
                    else if (value > 0.95f)
                    {
                        actionType = ActionType.RUN;
                    }
                }
                break;
            case ActionType.ATTACK1:
                if (IsCurrentAnimation(animator, "Attack 1"))
                {
                    if (isAttackButtonPressing && value >= 0.35f && value <= 0.7f)
                    {
                        actionType = ActionType.ATTACK2;
                    }
                    else if (value > 0.95f)
                    {
                        actionType = ActionType.RUN;
                    }
                }
                break;
            case ActionType.ATTACK2:
                if (IsCurrentAnimation(animator, "Attack 2"))
                {
                    if (isAttackButtonPressing && value >= 0.35f && value <= 0.75f)
                    {
                        actionType = ActionType.ATTACK3;
                    }
                    else if (value > 0.95f)
                    {
                        actionType = ActionType.RUN;
                    }
                }
                break;
            case ActionType.ATTACK3:
                if (IsCurrentAnimation(animator, "Attack 3"))
                {
                    if (isAttackButtonPressing && value >= 0.55f && value <= 0.9f)
                    {
                        actionType = ActionType.ATTACK4;
                    }
                    else if (value > 0.95f)
                    {
                        actionType = ActionType.RUN;
                    }
                }
                break;
            case ActionType.ATTACK4:
                if (IsCurrentAnimation(animator, "Attack 4"))
                {
                    if (value > 0.95f)
                    {
                        actionType = ActionType.RUN;
                    }
                }
                break;
        }

        // isAttackButtonPressing = false;
    }

    public bool IsCurrentAnimation(Animator animator, string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    public float GetAnimationNormalizedTime(Animator animator)
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
    DOUBLEJUMP,
    TRIPLEJUMP,
    UNDERJUMP,
    ATTACK,
    ATTACK1,
    ATTACK2,
    ATTACK3,
    ATTACK4,
    ATTACK5,
    SKILL,
    SKILL1,
    SKILL2,
    ULTIMATE
}
