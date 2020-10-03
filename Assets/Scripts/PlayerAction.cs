using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public ActionType actionType = ActionType.ATTACK;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPlayerWalk(Animator animator, string animationName)
    {
        animator.SetBool(animationName, true);
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
    ATTACK
}
