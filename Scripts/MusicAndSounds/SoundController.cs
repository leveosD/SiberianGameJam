using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    protected AudioSource _mainAudioSource;

    [SerializeField] protected List<AudioClip> clips = new List<AudioClip>();

    protected bool _isPaused = false;
    
    protected void Start()
    {
        _mainAudioSource = GetComponentsInChildren<AudioSource>()[0];
    }

    private void OnEnable()
    {
        InputController.GamePause += OnPause;
    }

    private void OnDisable()
    {
        InputController.GamePause -= OnPause;
    }

    public void PlayClip(int index)
    {
        _mainAudioSource.PlayOneShot(clips[index]);
    }

    public void Stop()
    {
        _mainAudioSource.Stop();
    }

    protected virtual void OnPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            _isPaused = true;
            _mainAudioSource.Pause();
        }
        else
        {
            _isPaused = false;
            _mainAudioSource.UnPause();
        }
    }
}
