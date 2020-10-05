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
        PlayerAction.instance.isAttackButtonPressing = true;
        // TODO 누르고 있을 때, 버튼 효과 추가
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerAction.instance.animator.SetBool("isRunning", true);
        PlayerAction.instance.isAttackButtonPressing = false;
    }
}
