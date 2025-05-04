using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    private static readonly int Moving = Animator.StringToHash("Moving");
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if(animator == null)
            animator = GetComponent<Animator>();
    }

    public bool IsMoving
    {
        set => animator.SetBool(Moving, value);
    }
    
    public void Shoot()
    {
        animator.Play("Shot");
    }

    public void Move()
    {
        Debug.Log("Move");
        animator.Play("Move");
    }

    public void Attack()
    {
        animator.Play("Attack");
    }

    public void Dead()
    {
        animator.Play("Dead");
    }
    public void Idle()
    {
        animator.Play("Idle");
    }
}
