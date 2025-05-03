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
        animator.Play("PistolShoot");
    }

    public void Move()
    {
        animator.Play("Move");
    }

    public void Attack()
    {
        Debug.Log("Attack");
        animator.Play("Attack");
    }

    public void Dead()
    {
        animator.Play("Dead");
    }
}
