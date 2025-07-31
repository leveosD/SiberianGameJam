using System;
using DG.Tweening;
using UnityEngine;

public class DoorCloser : MonoBehaviour
{
    private AudioSource _audioSource;
    private Vector3 _startPosition;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        WolfSpawner.WolfAwake += Close;
        InputController.PlayerDead += Open;
    }
    private void OnDisable()
    {
        WolfSpawner.WolfAwake -= Close;
        InputController.PlayerDead -= Open;
    }

    private void Open()
    {
        transform.position = _startPosition;
    }
    
    private void Close()
    {
        transform.DOLocalMoveY(0, 1.5f).SetEase(Ease.Linear);
    }
}
