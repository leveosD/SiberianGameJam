using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private CharacterController _characterController;
    private AnimationController _animatotionController;
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
        
        _characterController = GetComponent<CharacterController>();
        _animatotionController = GetComponent<AnimationController>();
    }

    private void OnEnable()
    {
        gameInput.Keyboard.Shot.performed += Shoot;
        //gameInput.Keyboard.TouchPress.started += OnTouchPressStarted;
    }

    private void OnDisable()
    {
        gameInput.Keyboard.Shot.performed -= Shoot;
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
            _characterController.Shoot();
            delay = 0.5f;
        }
    }

    private void ReadMovement()
    {
        input_direction = gameInput.Keyboard.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        _characterController.Move(input_direction);
    }

    private void ReadRotation()
    {
        input_mouse_direction = gameInput.Keyboard.Rotation.ReadValue<Vector2>();
    }

    private void Rotate()
    { 
        _characterController.Rotate(input_mouse_direction);
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        _animatotionController.Shoot();
    }

    public void FakeDestroy()
    {
        gameObject.SetActive(false);
    }
}
