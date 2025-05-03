using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private List<AudioClip> clips = new List<AudioClip>();
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(int index)
    {
        _audioSource.PlayOneShot(clips[index]);
    }
}
