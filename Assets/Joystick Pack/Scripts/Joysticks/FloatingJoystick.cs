using System;
using UnityEngine;
using UnityEngine.EventSystems;
using ZombieFarm.Interfaces;

public class FloatingJoystick : Joystick, IJoystick
{
    public event Action<bool> OnPointerStateChanged = (pointerDown) => { };
    public Vector3 GetCurrentMoveCommand() => new Vector3(-Horizontal, 0, -Vertical);

    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);

        OnPointerStateChanged(true);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);

        OnPointerStateChanged(false);
    }
}