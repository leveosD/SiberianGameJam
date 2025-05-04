using System;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour, IDamageable
{
    private MovementController _movementController;
    private AnimationController _animatotionController;
    private WeaponController _weaponController;
    private InputSystem gameInput;

    private Vector2 input_direction;
    private Vector2 input_mouse_direction;

    private float delay = 1f;

    [SerializeField] private int maxHealth;
    private int health;
    
    public static event Action PlayerDead;

    private Vector3 _startPosition;
    
    private void Awake()
    {
        gameInput = new InputSystem();
        gameInput.Enable();
        
        _movementController = GetComponent<MovementController>();
        _animatotionController = GetComponent<AnimationController>();
        _weaponController = GetComponent<WeaponController>();

        _startPosition = transform.position;

        health = maxHealth;
    }

    private void OnEnable()
    {
        gameInput.Keyboard.Shot.performed += Shoot;
        gameInput.Keyboard.Reload.performed += Reload;
        //gameInput.Keyboard.TouchPress.started += OnTouchPressStarted;
    }

    private void OnDisable()
    {
        gameInput.Keyboard.Shot.performed -= Shoot;
        gameInput.Keyboard.Reload.performed -= Reload;
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
        /*if (input_direction == Vector2.zero)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                input_direction.y += 1;
            }if (Input.GetKeyDown(KeyCode.S))
            {
                input_direction.y -= 1;
            }if (Input.GetKeyDown(KeyCode.A))
            {
                input_direction.y -= 1;
            }if (Input.GetKeyDown(KeyCode.D))
            {
                input_direction.y += 1;
            }   
        }*/
        //Debug.Log(input_direction);
    }

    private void Move()
    {
        //Debug.Log(input_direction);
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
        _weaponController.Shoot();
        //_animatotionController.Shoot();
    }

    private void Reload(InputAction.CallbackContext obj)
    {
        _weaponController.Reload();
    }
    
    public void FakeDestroy()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            PlayerDead?.Invoke();
            transform.position = _startPosition;
            health = maxHealth;
        }
    }
}
