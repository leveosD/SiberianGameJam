using UnityEngine;

public class EnemySoundController : SoundController
{
    private AudioSource _moanAudioSource;

    [SerializeField] private Vector2 moanRange;
    [SerializeField] private Vector2 aggressiveMoanRange;
    
    private void Start()
    {
        base.Start();
        _moanAudioSource = GetComponentsInChildren<AudioSource>()[1];
    }
    
    private void OnEnable()
    {
        InputController.GamePause += OnPause;
    }

    private void OnDisable()
    {
        InputController.GamePause -= OnPause;
    }
    
    public void Moan()
    {
        if (!_mainAudioSource.isPlaying)
        {
            int r = Random.Range((int)moanRange.x, (int)moanRange.y);
            _moanAudioSource.clip = clips[r];
            _moanAudioSource.Play();
        }
    }

    public void AggressiveMoan()
    {
        if (!_mainAudioSource.isPlaying)
        {
            int r = Random.Range((int)aggressiveMoanRange.x, (int)aggressiveMoanRange.y);
            _moanAudioSource.clip = clips[r];
            _moanAudioSource.Play();
        }
    }

    protected override void OnPause(bool pauseStatus)
    {
        base.OnPause(pauseStatus);
        if(pauseStatus)
            _moanAudioSource.Pause();
        else
            _moanAudioSource.UnPause();
    }

    public bool PlayInLoop(int index)
    {
        if (_isPaused)
            return false;
        _mainAudioSource.clip = clips[index];
        _mainAudioSource.loop = true;
        _mainAudioSource.Play();
        return true;
    }
}