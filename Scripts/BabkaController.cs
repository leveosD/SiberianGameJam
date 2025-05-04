using System.Collections;
using System.Collections.Generic;
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isDead)
        {
            _isPlayerNear = true;
            _animationController.Attack();
            StartCoroutine(AttackDelay());
            _soundController.PlayClip(0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !_isDead)
        {
            _isPlayerNear = false;
        }
    }
    
    protected override IEnumerator AttackDelay()
    {
        if (!_isDead)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            _soundController.PlayClip(10);
            yield return new WaitForSecondsRealtime(1.2f);
            if (_isPlayerNear && !_isDead)
            {
                _animationController.Attack();
                _soundController.PlayClip(0);
                StartCoroutine(AttackDelay());
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
