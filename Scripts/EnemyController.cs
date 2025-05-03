using System;
using System.Collections;
using Interfaces;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private GameObject _player;

    private MovementController _movementController;

    private AnimationController _animationController;

    private SpriteRenderer _spriteRenderer;

    private float constY;

    [SerializeField] private bool _isHunting;

    private Vector2 _commonDir = new Vector2(0, -1);
    private Vector2 _direction;

    public bool IsHunting
    {
        get => _isHunting;
        set
        {
            if (_isDead && value)
                return;
            _isHunting = value;
            _animationController.IsMoving = value;
            _animationController.Move();
        }
    }

    public bool IsAttacking
    {
        get;
        set;
    }

    [SerializeField] private bool _isPlayerNear = false;

    private bool _isDead = false;
    
    [SerializeField] private int maxHealth;
    private int _health;

    private Vector3 _playerVector;

    private AreaChecker _areaChecker;
    
    private Vector3 _startPosition;

    private void OnEnable()
    {
        InputController.PlayerDead += OnPlayerDeath;
    }
    private void OnDisable()
    {
        InputController.PlayerDead -= OnPlayerDeath;
    }

    private void Start()
    {
        _player = FindObjectOfType<InputController>().gameObject;
        _movementController = GetComponent<MovementController>();
        _animationController = GetComponent<AnimationController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _areaChecker = GetComponentInChildren<AreaChecker>();
        _areaChecker.Tag = "Player";
        Physics.IgnoreCollision(_areaChecker.BoxCollider, GetComponent<Collider>());

        constY = 0.5f;
        _startPosition = transform.position;
        _health = maxHealth;
    }
    
    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Update()
    {
        _playerVector = (_player.transform.position - this.transform.position).normalized; 
    }

    private void Move()
    {
        if (IsHunting && !IsAttacking && !_isDead && !_isPlayerNear)
        {
            /*_direction = _commonDir;
        else
            _direction = Vector2.zero;*/
            _movementController.Move(_commonDir);
        }
        else
        {
            _movementController.Move(Vector2.zero);
        }
    }
    
    private void Rotate()
    {
        transform.forward = -_playerVector;
        //transform.localRotation = Quaternion.Euler(0, transform.localRotation.y, 0);
        //transform.position = new Vector3(transform.position.x, constY, transform.position.z);
        //transform.localRotation = (-_playerVector).;
    }

    public void TakeDamage(int damage)
    {
        if (!IsHunting)
        {
            IsHunting = true;
        }

        _health -= damage;
        Debug.Log("_health: " + _health);
        
        if (_health <= 0)
        {
            _isDead = true;
            constY = 0f;
            _animationController.Dead();
            _spriteRenderer.sortingOrder = -1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isDead)
        {
            _isPlayerNear = true;
            _animationController.Attack();
            StartCoroutine(AttackDelay());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !_isDead)
        {
            _isPlayerNear = false;
        }
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSecondsRealtime(1.7f);
        if (_isPlayerNear)
        {
            _animationController.Attack();
            StartCoroutine(AttackDelay());
        }
    }

    public void CheckerOn()
    {
        _areaChecker.IsEnabled = true;
    }
    
    public void CheckerOff()
    {
        _areaChecker.IsEnabled = false;
    }

    private void OnPlayerDeath()
    {
        IsHunting = false;
        _animationController.IsMoving = false;
        _animationController.Idle();
        IsAttacking = false;
        _isDead = false;
        transform.position = _startPosition;
        _health = maxHealth;
        _spriteRenderer.sortingOrder = 0;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
