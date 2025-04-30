using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

public class InputReader : MonoBehaviour, IPlayerActions
{
    public bool Touching;
    public Vector2 MousePosition;
    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        //This hooks up the calls of the events in the Controls class
        controls.Player.SetCallbacks(this);
        //Actually enable the class
        controls.Player.Enable();  
    }

     private void OnDestroy()
     {
        //Disable the class
        controls.Player.Disable();
    }
    public void OnTouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Touching = true;
        }
        else if (context.canceled)
        {
            Touching = false;
        }
    }

    public void OnPosition(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }
}
