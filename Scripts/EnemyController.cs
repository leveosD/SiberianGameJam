using System.Collections;
using Interfaces;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private GameObject _player;

    private MovementController _movementController;

    private AnimationController _animationController;

    private CharacterController _characterController;

    private SpriteRenderer _spriteRenderer;

    private float constY;

    private bool _isHunting;

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
    
    private int _health = 3;

    private Vector3 _playerVector;

    private AreaChecker _areaChecker;
    
    private void Start()
    {
        _player = FindObjectOfType<InputController>().gameObject;
        _movementController = GetComponent<MovementController>();
        _animationController = GetComponent<AnimationController>();
        _characterController = GetComponent<CharacterController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _areaChecker = GetComponentInChildren<AreaChecker>();
        _areaChecker.Tag = "Player";

        constY = 0.5f;
    }
    
    private void FixedUpdate()
    {
        if(IsHunting && !IsAttacking && !_isDead) Move();
        Rotate();
    }

    private void Update()
    {
        _playerVector = (_player.transform.position - this.transform.position).normalized; 
    }

    private void Move()
    {
        _movementController.Move(new Vector2(1, -1));
    }
    
    private void Rotate()
    {
        transform.forward = -_playerVector;
        //transform.localRotation = Quaternion.Euler(0, transform.localRotation.y, 0);
        transform.position = new Vector3(transform.position.x, constY, transform.position.z);
        //transform.localRotation = (-_playerVector).;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy is taking damage");
        
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
            _characterController.enabled = false;
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
        yield return new WaitForSecondsRealtime(2);
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
}
