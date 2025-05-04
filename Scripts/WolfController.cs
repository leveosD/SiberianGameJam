using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class WolfController : EnemyController
{
    [SerializeField] private GameObject projectile;
    private Vector3 projectileOffset = new Vector3(-0.24f, 0.06f, 0.24f);

    private bool _isDelayed = false;

    /*protected void Start()
    {
        base.Start();
        IsHunting = true;
    }*/

    [SerializeField] private GameObject music;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject sound;
    
    protected override void EnemyBehaviour()
    {
        float pV = 0f;
        if(!_isDead)
            pV = Vector3.Distance(_player.transform.position, transform.position);
        //Debug.Log(!_isDelayed + " " + pV + " " + !_isDead);
        if (!_isDelayed && pV <= 6 && !_isDead)
        {
            _isDelayed = true;
            IsAttacking = true;
            StartCoroutine(Attack());
        }

        if (_isDead)
        {
            StartCoroutine(GoToMenu());
        }
    }

    private IEnumerator GoToMenu()
    {
        music.SetActive(false);
        canvas.SetActive(false);
        sound.SetActive(false);
        Destroy(this);
        
        var video = GetComponent<VideoPlayer>();
        video.Play();

        yield return new WaitForSecondsRealtime(16);
        SceneManager.LoadScene(0);
    }

    /*protected void Update()
    {
        base.Update();

        transform.localRotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, 0));
    }*/
    
    private IEnumerator Attack()
    {
        _animationController.Attack();
        yield return new WaitForSeconds(0.5f);

        _soundController.PlayClip(4);
        var p1 = Instantiate(projectile);
        p1.transform.localRotation = transform.localRotation;
        p1.transform.position = transform.position + new Vector3(projectileOffset.x, projectileOffset.y, projectileOffset.z);

        yield return new WaitForSeconds(0.5f);
        
        _soundController.PlayClip(4);
        var p2 = Instantiate(projectile);
        p2.transform.localRotation = transform.localRotation;
        p2.transform.position = transform.position + new Vector3(-projectileOffset.x, projectileOffset.y, projectileOffset.z);
        
        yield return new WaitForSeconds(0.5f);
        IsAttacking = false;
        
        yield return new WaitForSeconds(2f);
        _isDelayed = false;
        IsHunting = true;
    }

    public void WolfStep()
    {
        _soundController.PlayClip(0);
    }
    
    public void WolfPreStep()
    {
        _soundController.PlayClip(1);
    }
}
