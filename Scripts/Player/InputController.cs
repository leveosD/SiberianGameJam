using System;
using System.Collections;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputController : MonoBehaviour, IDamageable, IHealable
{
    private MovementController _movementController;
    private AnimationController _animatotionController;
    private WeaponController _weaponController;
    private InputSystem gameInput;

    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject gameplayCanvas;

    private CameraMover _cameraMover;

    private Vector2 input_direction;
    private Vector2 input_mouse_direction;

    private float delay = 1f;

    [SerializeField] private int maxHealth;
    private int _health;
    public static event Action<int, int> OnStatsChanged;
    public int Health
    {
        get => _health;
        private set
        {
            _health = value;
            OnStatsChanged?.Invoke(_health, _armor);
        }
    }
    
    [SerializeField] private int maxArmor;
    private int _armor;

    public int Armor
    {
        get => _armor;
        private set
        {
            _armor = value;
            OnStatsChanged?.Invoke(_health, _armor);
        }
    }
    
    public static event Action PlayerDead;
    public static event Action PlayerFall;

    private Vector3 _startPosition;

    private bool _isPaused = false;
    public static event Action<bool> GamePause;

    private float _sensitivity = 1f;
    
    private void Awake()
    {
        gameInput = new InputSystem();
        gameInput.Enable();
        
        _movementController = GetComponent<MovementController>();
        _animatotionController = GetComponent<AnimationController>();
        _weaponController = GetComponent<WeaponController>();

        _cameraMover = GetComponentInChildren<CameraMover>();
        
        _startPosition = transform.position;

        Health = PlayerPrefs.GetInt("HealthValue"); 
        Armor = PlayerPrefs.GetInt("ArmorValue");

        _sensitivity = PlayerPrefs.GetFloat("SensitivityValue");
        
        if (_sensitivity == 0)
        {
            PlayerPrefs.SetFloat("SensitivityValue", 1f);
            _sensitivity = 1f;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        //DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        gameInput.Keyboard.Shot.performed += Shoot;
        gameInput.Keyboard.Reload.performed += Reload;
        gameInput.Keyboard.Pistol.performed += TakePistol;
        gameInput.Keyboard.Shotgun.performed += TakeShotgun;
        gameInput.Keyboard.Pause.performed += Pause;
        gameInput.Keyboard.Restart.performed += Restart;
    }

    private void OnDisable()
    {
        gameInput.Keyboard.Shot.performed -= Shoot;
        gameInput.Keyboard.Reload.performed -= Reload;
        gameInput.Keyboard.Pistol.performed -= TakePistol;
        gameInput.Keyboard.Shotgun.performed -= TakeShotgun;
        gameInput.Keyboard.Pause.performed -= Pause;
        gameInput.Keyboard.Restart.performed -= Restart;
    }

    // Update is called once per frame
    private void Update()
    {
        ReadRotation();
        ReadMovement();
    }

    private void FixedUpdate()
    {
        if (!_isPaused && Health > 0)
        {
            Move();
            Rotate();
        }
    }

    private void ReadMovement()
    {
        input_direction = gameInput.Keyboard.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        _cameraMover.Move(input_direction);
        _movementController.Move(input_direction);
    }

    private void ReadRotation()
    {
        input_mouse_direction = gameInput.Keyboard.Rotation.ReadValue<Vector2>();
    }

    private void Rotate()
    {
        _movementController.Rotate(input_mouse_direction * _sensitivity);
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        if (!_isPaused && Health > 0)
        {
            _weaponController.Shoot();
            _cameraMover.Shoot();
        }
    }

    private void Reload(InputAction.CallbackContext obj)
    {
        if(!_isPaused && Health > 0) _weaponController.Reload();
    }
    
    private void TakePistol(InputAction.CallbackContext obj)
    {
        if(!_isPaused && PlayerPrefs.GetInt("WeaponCounter") >= 0) _weaponController.ChooseWeapon(0);
    }
    
    private void TakeShotgun(InputAction.CallbackContext obj)
    {
        if(!_isPaused && PlayerPrefs.GetInt("WeaponCounter") >= 1) _weaponController.ChooseWeapon(1);
    }

    public void TakeDamage(int damage)
    {
        if(Health <= 0)
            return;
        if (Armor >= damage)
            Armor -= damage;
        else
        {
            int delta = damage - Armor;
            Armor = 0;
            Health -= delta;
        }

        if (Health <= 0)
        {
            PlayerFall?.Invoke();
            Health = 0;      
            transform.DOLocalRotate(new Vector3(-80, transform.position.y, 0), 0.6f).SetEase(Ease.InSine);
        }
    }

    private void Restart(InputAction.CallbackContext obj)
    {
        if (Health <= 0)
        {
            PlayerDead?.Invoke();
            transform.position = _startPosition;
            Health = maxHealth;
            Armor = 0;
        }
    }

    public void Heal(int health, int armor)
    {
        if (Health + health <= maxHealth)
            Health += health;
        else
            Health = maxHealth;
        
        if (Armor + armor <= maxArmor)
            Armor += armor;
        else
            Armor = maxArmor;
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        if (Health > 0)
        {
            if (Time.timeScale != 0f)
            {
                _isPaused = true;
                pause.SetActive(true);
                gameplayCanvas.SetActive(false);
                Time.timeScale = 0f;
                GamePause?.Invoke(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                _isPaused = false;
                pause.SetActive(false);
                gameplayCanvas.SetActive(true);
                Time.timeScale = 1f;
                GamePause?.Invoke(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                _sensitivity = PlayerPrefs.GetFloat("SensitivityValue");
            }
        }
    }
}
