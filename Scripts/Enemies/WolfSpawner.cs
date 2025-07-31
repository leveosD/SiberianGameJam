using System;
using System.Collections;
using UnityEngine;

public class WolfSpawner : MonoBehaviour
{
    private BoxCollider _boxCollider;
    public static Action WolfAwake;

    [SerializeField] private GameObject wolf;

    private bool _checker = false; 

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        InputController.PlayerDead += ResetChecker;
    }
    
    private void OnDisable()
    {
        InputController.PlayerDead -= ResetChecker;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_checker)
        {
            _checker = true;
            _boxCollider.enabled = false;
            WolfAwake?.Invoke();
        }
    }

    private void ResetChecker()
    {
        _checker = false;
        _boxCollider.enabled = true;
    }
}
