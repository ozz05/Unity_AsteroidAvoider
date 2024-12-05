using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotationSpeed;

    private Rigidbody rigidBody;
    private Camera mainCamera;

    private Vector3 movementDirection;
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
        rigidBody = GetComponent<Rigidbody>();
        mainCamera = Camera.main; 
    }
    private void Update()
    {
        ProcessInput();
        KeepPlayerOnScreen();
        RotateToFaceVelocity();
    }

    private void FixedUpdate()
    {
        if (movementDirection == Vector3.zero) return;
        rigidBody.AddForce(movementDirection * forceMagnitude, ForceMode.Force);
        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxVelocity);
        
    }

    private void ProcessInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);

            movementDirection = transform.position - worldPos;
            movementDirection.z = 0;
            movementDirection.Normalize();

        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }
    private void RotateToFaceVelocity ()
    {
        if (rigidBody.velocity == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(rigidBody.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;

        Vector2 viewPort = mainCamera.WorldToViewportPoint(newPosition);
        if(viewPort.x > 1)
        {
            newPosition.x = -newPosition.x + 0.1f;
        }
        else if(viewPort.x < 0)
        {
            newPosition.x = -newPosition.x - 0.1f;
        }
        if (viewPort.y > 1)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }
        else if (viewPort.y < 0)
        {
            newPosition.y = -newPosition.y - 0.1f;
        }
        transform.position = newPosition;
    }
    private Vector3 GetMultitouch()
    {
        //Multi touch
        UnityEngine.InputSystem.EnhancedTouch.Touch touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0];
        Vector2 screenPosition = touch.screenPosition;

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPosition);

        return worldPos;
    }
}
