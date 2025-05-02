using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Shoot()
    {
        animator.Play("PistolShoot");
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
}
