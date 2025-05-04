using System.Collections;
using UnityEngine;

public class DedController : EnemyController
{
    private bool _attckDelay = false;
    private RaycastHit _hit;

    [SerializeField] private GameObject wave;
    private Vector3 _waveStartPosition = new Vector3(-0.11f, 0.2f, 0);
    
    protected override void EnemyBehaviour()
    {
        if (base.IsHunting && !_attckDelay && !_isDead)
        {
            _attckDelay = true;
            StartCoroutine(AttackDelay());
            
            int layerMask = ~(1 << LayerMask.NameToLayer("Enemy"));
            
            Physics.Raycast(transform.position, -transform.forward, out _hit,
                10, layerMask, QueryTriggerInteraction.Ignore);
            
            var col = _hit.collider;
            if (col)
            {
                if (col.CompareTag("Player"))
                {
                    base.IsAttacking = true;
                    Attack();
                }
            }
        }
    }

    private void Attack()
    {
        _animationController.Attack();
        _soundController.PlayClip(0);
        var newWave = Instantiate(wave);
        newWave.transform.localRotation = transform.localRotation;
        newWave.transform.localPosition = transform.position + _waveStartPosition;
    }

    protected override IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(2f);
        _attckDelay = false;
        IsAttacking = false;
    }
}
