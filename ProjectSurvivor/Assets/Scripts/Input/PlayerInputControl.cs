using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControl : MonoBehaviour
{
    public Vector2 move;

    public Controls control;

    private void OnEnable()
    {
        control = new Controls();
        control.Enable();

        control.PlayerControls.Move.performed += ctx => OnMove(ctx);
        control.PlayerControls.Move.canceled += ctx => OnMove(ctx);
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>();
    }
}
