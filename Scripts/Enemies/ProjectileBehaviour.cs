using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    private List<Transform> _circles;

    private AudioSource _audioSource;

    private int _angle;

    private float _lifeTime = 3.5f;

    private bool gotHit = false;

    private float startAngle;

    public string ParentName
    {
        get;
        set;
    }

    public float StartAngle { get; set; }

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
        _circles = GetComponentsInChildren<Transform>().ToList();
        _audioSource = GetComponent<AudioSource>();
    }
    
    protected void OnEnable()
    {
        InputController.PlayerDead += OnPlayerDead;
    }

    private void OnDisable()
    {
        InputController.PlayerDead -= OnPlayerDead;
    }

    private void Start()
    {
        int k = Random.Range(0, 2) == 0 ? -1 : 1;
        transform
            .DORotate(
                new Vector3(0, 0, k * 360), // на 360° по Y
                1.5f,                     // за 2 секунды
                RotateMode.LocalAxisAdd // именно ДОБАВЛЕНИЕ крутящего вектора
            )
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }

    private void FixedUpdate()
    {
        float y = transform.position.y > 1f ? 0.2f : 0;
        if(!gotHit) transform.position += - new Vector3(transform.forward.x, y, transform.forward.z) * (speed * Time.fixedDeltaTime);
    }

    /*private void Update()
    {
        var rot = transform.rotation;
        transform.rotation = Quaternion.Euler(new Vector3(rot.x, rot.y, Angle++));
    }*/

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        bool deadEnemy = true;
        if (tag == "Enemy")
            deadEnemy = other.gameObject.GetComponent<EnemyController>().IsDead;
        if (tag == "Player" || !deadEnemy && !other.gameObject.name.Contains(ParentName))
        {
            _audioSource.Play();
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
            gotHit = true;

            for (int i = 1; i < _circles.Count; i++)
            {
                _circles[i].gameObject.SetActive(false);
            }
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
        else if(tag == "Builds")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator PlayDamageSound()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    private void OnPlayerDead()
    {
        Destroy(gameObject);
    }
}
