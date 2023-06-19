using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class PlayerMobileInput : MonoBehaviour, IPlayerInput
{
    public Joystick joystick;
    public MobileButton dashButton;
    [Range(0, 1)]
    [SerializeField] private float distance = 0.4f;

    public bool IsMoveLeft()
    {
        return joystick.Horizontal <= -distance;
    }

    public bool IsMoveRight()
    {
        return joystick.Horizontal >= distance;
    }

    public bool IsMoveUp()
    {
        return joystick.Vertical >= distance;
    }

    public bool IsMoveDown()
    {
        return joystick.Vertical <= -distance;
    }

    public bool IsDash()
    {
        return dashButton.IsPressed;
    }
}
