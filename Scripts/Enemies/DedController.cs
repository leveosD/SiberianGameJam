using System.Collections;
using UnityEngine;

public class DedController : EnemyController
{
    private RaycastHit _hit;

    [SerializeField] private GameObject wave;
    private Vector3 _waveStartPosition = new Vector3(-0.11f, 0.2f);
    
    protected override void EnemyBehaviour()
    {
        if (!_isDead)
        {
            if (/*PlayerVector.magnitude <= 10f && */!_isDead && !IsAttacking)
            {
                Physics.Raycast(transform.position, -transform.forward, out _hit,
                    Mathf.Infinity, enemyMask, QueryTriggerInteraction.Ignore);
            
                var col = _hit.collider;
                if (col)
                {
                    if (col.CompareTag("Player"))
                    {
                        IsAttacking = true;
                        Attack();
                        StartCoroutine(AttackDelay());
                    }
                }
            }
        }
    }

    private void Attack()
    {
        _soundController.PlayClip(0);
        var newWave = Instantiate(wave);
        var waveComponent = newWave.GetComponent<Wave>();
        waveComponent.ParentName = gameObject.name;
        waveComponent.StartAngle = transform.localRotation.y;
        newWave.transform.localRotation = transform.rotation;
        newWave.transform.localPosition = transform.position + _waveStartPosition;
    }

    protected override IEnumerator AttackDelay()
    {
        if (!_isDead)
        {
            yield return new WaitForSecondsRealtime(2f);

            if (/*PlayerVector.magnitude <= 10f && */!_isDead)
            {
                _animationController.Attack();
                Attack();
                StartCoroutine(AttackDelay());
            }
            else
            {
                IsAttacking = false;
            }
        }
    }
}
