using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHolding : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int buttonType;

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerAction.instance.SetActionType(buttonType);
        PlayerAction.instance.animator.SetBool("isRunning", false);

        if(buttonType == 4)
        {
            PlayerAction.instance.ButtonJump();
        }

        if (buttonType == 8)
        {
            PlayerAction.instance.isAttackButtonPressing = true;

            // 촙핑 캔슬
/*            if (PlayerAction.instance.actionType == ActionType.CHOPPING)
            {
                PlayerAction.instance.SetLanding();
            }*/
        }
        // TODO 누르고 있을 때, 버튼 효과 추가
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerAction.instance.animator.SetBool("isRunning", true);
        PlayerAction.instance.isAttackButtonPressing = false;
    }
}
