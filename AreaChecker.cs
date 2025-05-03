using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class AreaChecker : MonoBehaviour
{
    private BoxCollider _boxCollider;
    
    public string Tag
    {
        get;
        set;
    }

    public bool IsEnabled
    {
        set => _boxCollider.enabled = value;
    }

    [SerializeField] private int damage;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag))
        {
            var damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);
        }
    }
}
