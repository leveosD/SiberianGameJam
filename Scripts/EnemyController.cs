using System.Collections;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, IDamageable
{
    protected GameObject _player;

    private MovementController _movementController;

    protected AnimationController _animationController;

    private SpriteRenderer _spriteRenderer;

    protected SoundController _soundController;

    private BoxCollider _collider;
    private BoxCollider _trigger;

    private float constY;

    private bool _isHunting;

    private Vector2 _commonDir = new Vector2(0, -1);
    private Vector2 _direction;

    private float _moanDelay;

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

    protected bool _isPlayerNear = false;

    protected bool _isDead = false;
    
    [SerializeField] protected int maxHealth;
    protected int _health;

    private Vector3 _playerVector;
    
    private Vector3 _startPosition;

    [SerializeField] private Vector2 huntingMoan;
    [SerializeField] private Vector2 commonMoan;

    private void OnEnable()
    {
        InputController.PlayerDead += OnPlayerDeath;
    }
    private void OnDisable()
    {
        InputController.PlayerDead -= OnPlayerDeath;
    }

    protected void Start()
    {
        _player = FindObjectOfType<InputController>().gameObject;
        _movementController = GetComponent<MovementController>();
        _animationController = GetComponent<AnimationController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _soundController = GetComponentInChildren<SoundController>();
        _collider = GetComponents<BoxCollider>()[0];
        _trigger = GetComponents<BoxCollider>()[1];
        
        constY = 0.5f;
        _startPosition = transform.position;
        _health = maxHealth;

        _moanDelay = Random.Range(5f, 12f);
    }
    
    protected virtual void EnemyBehaviour()
    {
        
    }
    private void FixedUpdate()
    {
        Move();
        Rotate();
        EnemyBehaviour();    
    }

    private void Update()
    {
        _playerVector = (_player.transform.position - this.transform.position).normalized;
        if (!_isDead && _moanDelay <= 0)
        {
            if (!IsHunting)
            {
                _soundController.PlayClip(Random.Range((int)commonMoan.x, (int)commonMoan.y));
                _moanDelay = Random.Range(5f, 12f);
            }
            else
            {
                _soundController.PlayClip(Random.Range((int)huntingMoan.x, (int)huntingMoan.y));
                _moanDelay = Random.Range(3f, 9f);
            }
        }

        _moanDelay -= Time.deltaTime;
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
        
        if (_health <= 0)
        {
            _isDead = true;
            constY = 0f;
            _animationController.Dead();
            _collider.enabled = false;
            _soundController.PlayClip(3);
            StartCoroutine(DownBody());
        }
        else
        {
            _soundController.PlayClip(2);
        }
    }

    private IEnumerator DownBody()
    {
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.sortingOrder = -1;
        transform.position = new Vector3(transform.position.x, -0.25f, transform.position.z);
    }

    protected virtual IEnumerator AttackDelay()
    {
        yield return null;
    }

    private void OnPlayerDeath()
    {
        _collider.enabled = true;
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
