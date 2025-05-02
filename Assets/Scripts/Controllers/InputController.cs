using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private CharacterController characterController;
    private InputSystem gameInput;

    private Vector2 input_direction;
    private Vector2 input_mouse_direction;

    private float delay = 1f;

    
    public static event Action PlayerDead;

    private bool isDesktop;
    
    private void Awake()
    {
        gameInput = new InputSystem();
        gameInput.Enable();
        
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        //gameInput.Keyboard.Shoot.performed += Shoot;
        //gameInput.Keyboard.TouchPress.started += OnTouchPressStarted;
    }

    private void OnDisable()
    {
        PlayerDead?.Invoke();
        //gameInput.Keyboard.Shoot.performed -= Shoot;
        //gameInput.Keyboard.TouchPress.performed -= OnTouchPressStarted;
    }

    // Update is called once per frame
    private void Update()
    {
        if (delay > 0)
            delay -= Time.deltaTime;
        ReadRotation();
        ReadMovement();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        if (!isDesktop && delay <= 0)
        {
            characterController.Shoot();
            delay = 0.5f;
        }
    }

    private void ReadMovement()
    {
        input_direction = gameInput.Keyboard.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        characterController.Move(input_direction);
    }

    private void ReadRotation()
    {
        input_mouse_direction = gameInput.Keyboard.Rotation.ReadValue<Vector2>();
    }

    private void Rotate()
    { 
        characterController.Rotate(input_mouse_direction);
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        if(delay <= 0)
        {
            characterController.Shoot();
            delay = 0.75f;
        }
    }

    public void FakeDestroy()
    {
        gameObject.SetActive(false);
    }
}
