using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicTest : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> clips = new List<AudioClip>() ;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        // Воспроизвести первый клип
        audioSource.PlayOneShot(clips[0]);

        // Запланировать проигрывание второго после окончания первого
        Invoke(nameof(PlaySecondClip), 1.7f);
    }

    void PlaySecondClip()
    {
        audioSource.PlayOneShot(clips[1]);
    }
}