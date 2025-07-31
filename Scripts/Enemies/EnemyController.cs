using System.Collections;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, IDamageable
{
    protected GameObject _player;

    private MovementController _movementController;

    protected EnemyAnimationController _animationController;

    private SpriteRenderer _spriteRenderer;

    protected EnemySoundController _soundController;

    private BoxCollider _collider;
    private BoxCollider _trigger;

    private float constY;

    [SerializeField] private bool _isHunting;
    [SerializeField] private bool _isAttacking;
    [SerializeField] private bool _isMoving;

    private Vector2 _commonDir = new Vector2(0, -1);
    private Vector2 _direction;

    private float _moanDelay;

    protected int enemyMask;

    public bool IsHunting
    {
        get => _isHunting;
        set
        {
            if (_isDead && value)
                return;
            _isHunting = value;
        }
    }

    public bool IsAttacking
    {
        get => _isAttacking;
        set
        {
            if (IsHunting && value)
            {
                IsMoving = false;
                _isAttacking = true;
                _animationController.Attack();
            }
            else
                _isAttacking = value;
        }
    }
    
    public bool IsMoving
    {
        get => _isMoving;
        set
        {
            if (IsHunting && value)
            {
                IsAttacking = false;
                _isMoving = true;
                _animationController.Move();
            }
            else
                _isMoving = value;
        }
    }

    [SerializeField] protected bool _isDead = false;

    public bool IsDead
    {
        get => _isDead;
        private set
        {
            if(value) StopAllCoroutines();
            _isDead = value;
        }
    }
    
    [SerializeField] protected int maxHealth;
    protected int _health;

    protected Vector3 PlayerVector;
    
    private Vector3 _startPosition;

    [SerializeField] private Vector2 huntingMoan;
    [SerializeField] private Vector2 commonMoan;

    protected void OnEnable()
    {
        InputController.PlayerDead += OnPlayerDeath;
        InputController.PlayerFall += OnPlayerFall;
    }
    protected void OnDisable()
    {
        InputController.PlayerDead -= OnPlayerDeath;
        InputController.PlayerFall -= OnPlayerFall;
    }

    protected void Start()
    {
        _player = FindObjectOfType<InputController>().gameObject;
        _movementController = GetComponent<MovementController>();
        _animationController = GetComponent<EnemyAnimationController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _soundController = GetComponent<EnemySoundController>();
        _collider = GetComponents<BoxCollider>()[0];
        _trigger = GetComponents<BoxCollider>()[1];
        
        constY = 0.5f;
        _startPosition = transform.position;
        _health = maxHealth;

        _moanDelay = Random.Range(5f, 12f);
        
        enemyMask = ~(1 << LayerMask.NameToLayer("Enemy"));
    }
    
    protected virtual void EnemyBehaviour()
    {
        
    }
    
    private void FixedUpdate()
    {
        if(IsHunting) Move();
        Rotate();
    }

    protected void Update()
    {
        PlayerVector = _player.transform.position - this.transform.position;
        if (!_isDead && _moanDelay <= 0)
        {
            if (!IsHunting)
            {
                _soundController.Moan();
                _moanDelay = Random.Range(3f, 10f);
            }
            else
            {
                _soundController.AggressiveMoan();
                _moanDelay = Random.Range(3f, 7f);
            }
        }

        _moanDelay -= Time.deltaTime;
        
        if(IsHunting) EnemyBehaviour();
    }

    private void Move()
    {
        if (IsMoving && !_isDead && PlayerVector.magnitude >= 0.6f)
        {
            _movementController.Move(_commonDir);
        }
        else
        {
            _movementController.Move(Vector2.zero);
        }
    }
    
    private void Rotate()
    {
        transform.forward = -PlayerVector.normalized;
    }

    public virtual void TakeDamage(int damage)
    {
        if (!IsHunting)
        {
            IsHunting = true;
        }

        _health -= damage;
        
        if (_health <= 0)
        {
            IsDead = true;
            //constY = 0f;
            _animationController.Dead();
            _collider.enabled = false;
            _trigger.enabled = false;
            _soundController.PlayClip(3);
            //StartCoroutine(DownBody());
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
        transform.localPosition += new Vector3(0, -0.25f, 0);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 1, 0) * transform.localRotation.y);
        //_collider.enabled = false;
    }

    protected virtual IEnumerator AttackDelay()
    {
        yield return null;
    }

    private void OnPlayerFall()
    {
        IsHunting = false;
        IsMoving = false;
        IsAttacking = false;
    }

    private void OnPlayerDeath()
    {
        IsDead = true;
        _collider.enabled = true;
        _trigger.enabled = true;
        IsHunting = false;
        IsAttacking = false;
        IsMoving = false;
        _animationController.Idle();
        transform.position = _startPosition;
        _health = maxHealth;
        _spriteRenderer.sortingOrder = 0;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        IsDead = false;
    }
}
