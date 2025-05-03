using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    private static readonly int Moving = Animator.StringToHash("Moving");
    [SerializeField] private Animator animator;

    public bool IsMoving
    {
        set => animator.SetBool(Moving, value);
    }
    
    public void Shoot()
    {
        animator.Play("Shoot");
    }

    public void Move()
    {
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
