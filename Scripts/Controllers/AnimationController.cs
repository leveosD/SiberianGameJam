using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Shoot()
    {
        animator.Play("PistolShoot");
    }
}
