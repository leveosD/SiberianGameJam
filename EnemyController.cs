using System.Collections;
using Interfaces;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private GameObject _player;

    private MovementController _movementController;

    private AnimationController _animationController;

    private CharacterController _characterController;

    private float constY;

    public bool IsHunting
    {
        get;
        set;
    }
    
    private bool IsAttacking
    {
        get;
        set;
    }

    private bool isDead = false;
    
    private int health = 3;

    private Vector3 playerVector;
    
    private void Start()
    {
        _player = FindObjectOfType<InputController>().gameObject;
        _movementController = GetComponent<MovementController>();
        _animationController = GetComponent<AnimationController>();
        _characterController = GetComponent<CharacterController>();

        constY = transform.position.y;
    }
    
    private void FixedUpdate()
    {
        if(IsHunting && !IsAttacking && !isDead) Move();
        Rotate();
    }

    private void Update()
    {
        playerVector = (_player.transform.position - this.transform.position).normalized; 
    }

    private void Move()
    {
        _movementController.Move(new Vector2(1, -1));
        _animationController.Move();
    }
    
    private void Rotate()
    {
        transform.forward = -playerVector;
        //transform.localRotation = Quaternion.Euler(0, transform.localRotation.y, 0);
        transform.position = new Vector3(transform.position.x, constY, transform.position.z);
        //transform.localRotation = (-playerVector).;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy is taking damage");
        
        if (!IsHunting)
        {
            IsHunting = true;
        }

        health -= damage;
        Debug.Log("Health: " + health);
        
        if (health <= 0)
        {
            isDead = true;
            constY = 0f;
            _characterController.enabled = false;
            _animationController.Dead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDead)
        {
            IsAttacking = true;
            _animationController.Attack();
        }
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1.5f);
        IsAttacking = false;
    }
}
