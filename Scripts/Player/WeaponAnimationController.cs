using System.Collections;
using UnityEngine;

public class WeaponAnimationController : AnimationController
{
    private RectTransform _rectTransform;
    [SerializeField] private Vector2 defaultPos;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        _rectTransform = GetComponent<RectTransform>();
    }
    
    private void OnEnable()
    {
        TakeUp();
    }

    public void Shot()
    {
        animator.Play("Shot");
    }
    
    public void EmptyClip()
    {
        animator.Play("EmptyClip");
    }
    
    public void Reload()
    {
        animator.Play("Reload");
    }

    public void TakeOff()
    {
        Debug.Log(gameObject.name + " TakeOff");
        animator.Play("TakeOff");
    }

    public void TakeUp()
    {
        animator.Play("TakeUp");
    }

    private void TurnOff()
    {
        //_rectTransform.anchoredPosition = defaultPos;
        gameObject.SetActive(false);
    }
}
