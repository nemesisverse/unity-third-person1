using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour , Controls.IPlayerActions
{
    public bool isAttacking { get; private set; }
    public Vector2 MovementValue { get; private set; }
    

    private Controls controls;

    public event Action TargetEvent;
    public event Action CancelEvent;

    // Start is called before the first frame update
    void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();

    }
    void OnDestroy()
    {
       controls.Player.Disable(); 
    }


   
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
       
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        TargetEvent?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        CancelEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isAttacking = true;
        }
        else if (context.canceled)
        {
            isAttacking = false;
        }
    }
}
