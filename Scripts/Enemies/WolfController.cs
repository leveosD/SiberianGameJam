using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class WolfController : EnemyController
{
    private ProjectileSpawner[] _spawners;
    private bool _isDelayed = false;

    [SerializeField] private GameObject music;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Healthbar healthBar;
    [SerializeField] private VideoPlayer video;

    private Rigidbody _rigidbody;

    private float pV;

    private float fallingTime;

    private void Awake()
    {
        _spawners = GetComponentsInChildren<ProjectileSpawner>();
        _rigidbody = GetComponent<Rigidbody>();

        healthBar.maxHealth = maxHealth;
    }

    protected void OnEnable()
    {
        base.OnEnable();
        WolfSpawner.WolfAwake += WakeUp;
        InputController.PlayerDead += OnPlayerDead;
    }

    private void OnDisable()
    {
        base.OnDisable();
        WolfSpawner.WolfAwake -= WakeUp;
        InputController.PlayerDead -= OnPlayerDead;
    }

    protected override void EnemyBehaviour()
    {
        if (!_isDead)
        {
            if (!_isDelayed)
            {
                pV = Vector3.Distance(_player.transform.position, transform.position);
                if (pV <= 14)
                {
                    _isDelayed = true;
                    IsAttacking = true;
                    StartCoroutine(Attack());
                }
                else if (!IsMoving)
                {
                    IsMoving = true;
                }
            }
        }
        
        else
        {
            StartCoroutine(GoToMenu());
        }
    }

    private IEnumerator GoToMenu()
    {
        music.SetActive(false);
        canvas.SetActive(false);
        Destroy(_soundController);
        Destroy(_player.GetComponent<InputController>());
        //Destroy(this);
        
        video.Play();

        yield return new WaitForSecondsRealtime(15f);
        SceneManager.LoadScene(0);
    }
    
    private IEnumerator Attack()
    {
        _animationController.Attack();
        yield return new WaitForSeconds(1.5f);
        
        IsAttacking = false;
        
        yield return new WaitForSeconds(0.5f);
        _isDelayed = false;
    }

    private void Spawn(int index)
    {
        _spawners[index].Spawn();
    }

    private void ShootSound(int index)
    {
        _soundController.PlayClip(index == 0 ? 4 : 13);
    }

    public void WolfPreStep()
    {
        _soundController.PlayClip(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            _soundController.Stop();
            _soundController.PlayClip(11);
            IsHunting = true;
        }
    }

    private void WakeUp()
    {
        StartCoroutine(StartFalling());
    }

    private IEnumerator StartFalling()
    {
        yield return new WaitForSecondsRealtime(3.85f);
        yield return new WaitUntil(() => _soundController.PlayInLoop(12));
        //GetComponentsInChildren<AudioSource>()[0].PlayLoop()
        _rigidbody.useGravity = true;
        fallingTime = Time.time;
    }

    private void OnPlayerDead()
    {
        _isDelayed = false;
        _rigidbody.useGravity = false;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthBar.TakeDamage(damage);
    }
}
