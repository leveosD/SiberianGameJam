using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;
public class Wave : MonoBehaviour
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

    private void FixedUpdate()
    {
        if(!gotHit) transform.position += -transform.forward * (speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        for (int i = 1; i < _circles.Count; i++)
        {
            int k = i % 2 == 0 ? 1 : -1;
            _circles[i].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, k * Angle++));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        string tag = other.tag;
        bool deadEnemy = true;
        if (tag == "Enemy")
            deadEnemy = other.gameObject.GetComponent<EnemyController>().IsDead;
        if (tag == "Player" || !deadEnemy && !other.gameObject.name.Contains(ParentName))
        {
            Debug.Log("Got player");
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
        else if(tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator PlayDamageSound()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
