using UnityEngine;

public class AnimationController : MonoBehaviour
{
    /*[SerializeField] */protected Animator animator;

    private void Awake()
    {
        if(animator == null)
            animator = GetComponent<Animator>();
    }
    
    public void Idle()
    {
        animator.Play("Idle");
    }
}
