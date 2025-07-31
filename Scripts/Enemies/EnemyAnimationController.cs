using UnityEngine;

public class EnemyAnimationController : AnimationController
{
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    private static readonly int Moving = Animator.StringToHash("Moving");
    
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
}
