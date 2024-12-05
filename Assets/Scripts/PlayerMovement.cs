using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMovement : MonoBehaviour
{
    private Camera camera;
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
    private void Start()
    {
        camera = Camera.main; 
    }
    private void Update()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPos = camera.ScreenToWorldPoint(screenPos);
        }

        
    }

    private Vector3 GetMultitouch()
    {
        //Multi touch
        UnityEngine.InputSystem.EnhancedTouch.Touch touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0];
        Vector2 screenPosition = touch.screenPosition;

        Vector3 worldPos = camera.ScreenToWorldPoint(screenPosition);

        return worldPos;
    }
}
