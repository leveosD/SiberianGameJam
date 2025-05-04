using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private float speed;

    private List<RectTransform> _circles;

    private int _angle;

    private int Angle
    {
        get => _angle;
        set
        {
            _angle = value;
            if (_angle > 360)
                _angle -= 360;
        }
    }

    private void Awake()
    {
        _circles = GetComponentsInChildren<RectTransform>().ToList();
    }

    private void FixedUpdate()
    {
        transform.position += -transform.forward * (speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        foreach (var circle in _circles)
        {
            circle.localRotation = Quaternion.Euler(new Vector3(0, 0, Angle++));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(3);
        }
    }
}
