using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    private List<RectTransform> _circles;

    private AudioSource _audioSource;

    private int _angle;

    private float _lifeTime = 3.5f;

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
        _audioSource = GetComponent<AudioSource>();
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

        _lifeTime -= Time.deltaTime;
        if(_lifeTime <= 0)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        bool deadEnemy = true;
        if (tag == "Enemy")
            deadEnemy = other.gameObject.GetComponent<EnemyController>().IsDead;
        if (tag == "Player" || !deadEnemy)
        {
            _audioSource.Play();
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }

    IEnumerator PlayDamageSound()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
