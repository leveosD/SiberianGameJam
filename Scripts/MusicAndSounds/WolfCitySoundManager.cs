using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WolfCitySoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] tracks;
    [SerializeField] private AudioClip awaking;
    [SerializeField] private AudioClip preMainMusic;
    [SerializeField] private AudioClip mainMusic;
    private AudioSource _audioSource;
    private AudioSource _audioSource2;

    private float _delay = 0;
    [SerializeField] private float mainMusicDelay;
    private bool _isHeAwake = false;

    private bool _isPaused;
    
    void Awake()
    {
        _audioSource = GetComponents<AudioSource>()[0];
        _audioSource2 = GetComponents<AudioSource>()[1];
    }

    private void OnEnable()
    {
        WolfSpawner.WolfAwake += WolfAwakes;
        InputController.PlayerDead += OnPlayerDead;
        InputController.GamePause += OnPause;
    }
    private void OnDisable()
    {
        WolfSpawner.WolfAwake -= WolfAwakes;
        InputController.PlayerDead -= OnPlayerDead;
        InputController.GamePause -= OnPause;
    }

    void Update()
    {
        if (_isHeAwake)
            return;

        if (!_audioSource.isPlaying && _delay <= 0)
        {
            _audioSource.clip = tracks[Random.Range(0, tracks.Length)];
            _audioSource.Play();
            _delay = tracks[Random.Range(0, tracks.Length)].length - 0.3f;
        }
        /*else if (!_audioSource.isPlaying)
            _audioSource.clip = null;*/

        if (_delay > 0)
            _delay -= Time.deltaTime;
    }

    private void WolfAwakes()
    {
        _isHeAwake = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(awaking);
        StartCoroutine(FightMusicPlay());
    }

    private IEnumerator FightMusicPlay()
    {
        yield return new WaitForSecondsRealtime(mainMusicDelay);
        yield return new WaitUntil(() => !_isPaused);

        _audioSource.volume = 0.4f;
        _audioSource.clip = preMainMusic;

        float length = _audioSource.clip.length;
        float delay = 0f;
        _audioSource.Play();
        while (delay < length)
        {
            if (!_isPaused)
            {
                delay += Time.deltaTime;
                //Debug.Log("Delay: " + delay);
            }
            yield return null;
        }
        
        _audioSource2.clip = mainMusic;
        _audioSource2.Play();
        
        _audioSource2.loop = true;
    }

    private void OnPlayerDead()
    {
        _audioSource.Stop();
        _audioSource2.Stop();
        _isHeAwake = false;
    }
    
    protected void OnPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            _audioSource.Pause();
            _audioSource2.Pause();
            _isPaused = true;
        }
        else
        {
            _isPaused = false;
            _audioSource.UnPause();
            _audioSource2.UnPause();
        }
    }
}