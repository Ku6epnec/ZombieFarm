using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityTools.Runtime.StatefulEvent;
using ZombieFarm.Interfaces;

public class FloatingJoystick : Joystick, IJoystick
{
    public IStatefulEvent<bool> IsActive => isActive;
    private readonly StatefulEventInt<bool> isActive = StatefulEventInt.Create(false);
    public Vector3 GetCurrentMoveCommand() => new Vector3(-Horizontal, 0, -Vertical);

    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);

        base.OnPointerDown(eventData);

        Activate(true);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        Activate(false);
    }

    private void Activate(bool activate)
    {
        background.gameObject.SetActive(activate);
        isActive.Set(activate);
    }
}