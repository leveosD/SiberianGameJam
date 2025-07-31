using System.Collections;
using UnityEngine;

public class BabkaController : EnemyController
{
    private AreaChecker _areaChecker;
    
    protected void Start()
    {
        base.Start();
        
        _areaChecker = GetComponentInChildren<AreaChecker>();
        Physics.IgnoreCollision(_areaChecker.BoxCollider, GetComponent<Collider>());
        _areaChecker.Tag = "Player";
    }
    
    protected override void EnemyBehaviour()
    {
        if (!_isDead)
        {
            if (PlayerVector.magnitude <= 0.6f && !_isDead && !IsAttacking)
            {
                IsAttacking = true;
                StartCoroutine(AttackDelay());
                _soundController.PlayClip(0);
            }
            else if (!IsMoving && !IsAttacking)
            {
                IsMoving = true;
            }
        }
    }

    protected override IEnumerator AttackDelay()
    {
        if (!_isDead)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            _soundController.PlayClip(10);
            yield return new WaitForSecondsRealtime(1.2f);

            if (PlayerVector.magnitude <= 0.6f && !_isDead)
            {
                _animationController.Attack();
                _soundController.PlayClip(0);
                StartCoroutine(AttackDelay());
            }
            else
            {
                IsAttacking = false;
            }
        }
    }

    public void CheckerOn()
    {
        _areaChecker.IsEnabled = true;
    }
    
    public void CheckerOff()
    {
        _areaChecker.IsEnabled = false;
    }
}
