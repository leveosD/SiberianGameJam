using System;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour, IDamageable
{
    private MovementController _movementController;
    private AnimationController _animatotionController;
    private InputSystem gameInput;

    private Vector2 input_direction;
    private Vector2 input_mouse_direction;

    private float delay = 1f;

    private int health = 10;
    
    public static event Action PlayerDead;

    private bool isDesktop;
    
    private void Awake()
    {
        gameInput = new InputSystem();
        gameInput.Enable();
        
        _movementController = GetComponent<MovementController>();
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
        ReadRotation();
        ReadMovement();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void ReadMovement()
    {
        input_direction = gameInput.Keyboard.Movement.ReadValue<Vector2>();
        //Debug.Log(input_direction);
    }

    private void Move()
    {
        Debug.Log(input_direction);
        _movementController.Move(input_direction);
    }

    private void ReadRotation()
    {
        input_mouse_direction = gameInput.Keyboard.Rotation.ReadValue<Vector2>();
    }

    private void Rotate()
    { 
        _movementController.Rotate(input_mouse_direction);
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        _movementController.Shoot();
        _animatotionController.Shoot();
    }

    public void FakeDestroy()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Player is DAMAGED!");
        health -= damage;
    }
}
