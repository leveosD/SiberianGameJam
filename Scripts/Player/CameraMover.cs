using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraMover : MonoBehaviour
{
    private Transform _camera;
    [SerializeField] private bool _isMoving = false;
    [SerializeField] private bool _isLocked = false;
    [SerializeField] private bool _isStarted = false;
    private Coroutine _coroutine;

    private Vector3 _startPosition;

    private Tween _tween;
    [SerializeField] private float shortTime;
    [SerializeField] private float longTime;
    [SerializeField] private float xMinAmplitude;
    [SerializeField] private float xMaxAmplitude;
    [SerializeField] private float yMinAmplitude;
    [SerializeField] private float yMaxAmplitude;

    private Vector3[] _points;
    private Sequence _firstTween;
    private Sequence _sequence;

    private float _delay;
    
    private void Awake()
    {
        _startPosition = transform.localPosition;
        
        _camera = GetComponent<Camera>().transform;
        
        _points = new[]
        {            
            new Vector3(xMaxAmplitude, yMaxAmplitude, 0),
            new Vector3(-xMinAmplitude, -yMinAmplitude, 0),
            new Vector3(-xMaxAmplitude, yMaxAmplitude, 0),
            new Vector3(xMinAmplitude, -yMinAmplitude, 0)
        };

        _sequence = DOTween.Sequence().Pause();

        for (int i = 0; i < _points.Length; i++)
        {
            float t;
            if (i % 2 == 0)
                t = shortTime;
            else
                t = longTime;
            _sequence.Append(_camera.DOLocalMove(_points[i], t)
                .SetEase(Ease.InOutSine));
        }

        _sequence.SetLoops(-1);

        _delay = 0f;
    }

    private void OnEnable()
    {
        InputController.PlayerFall += PauseSequence;
    }
    private void OnDisable()
    {
        InputController.PlayerFall -= PauseSequence;
    }

    private void PauseSequence()
    {
        _isMoving = false;
        _sequence.Pause();
    }

    private bool ShouldIShake()
    {
        return _isMoving && !_isLocked;
    }

    private void Update()
    {
        if (ShouldIShake() && !_isStarted)
        {
            _isStarted = true;
            _camera.DOLocalMove(new Vector3(xMinAmplitude, -yMinAmplitude, 0), longTime / 2)
                .SetEase(Ease.OutSine).OnComplete(() =>
                {
                    if(ShouldIShake()) _sequence.Restart();
                    else
                    {
                        _camera
                            .DOLocalMove(_startPosition, 0.1f)
                            .SetEase(Ease.OutSine);
                        _isStarted = false;
                    }
                });
        }
        else if (ShouldIShake())
        {
            
        }
        else
        {
            _isStarted = false;
            _sequence.Pause();
            _camera
                .DOLocalMove(_startPosition, 0.1f)
                .SetEase(Ease.OutSine);
        }
    }

    public void Move(Vector2 direction)
    {
        //Debug.Log(direction);
        /*Debug.Log(direction);
        if (direction != Vector2.zero)
        {
            IsMoving = true;
        }
        else if(IsMoving)
            IsMoving = false;*/
        if (direction != Vector2.zero)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }

    public void Shoot()
    {
        if(_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(PauseOn(1.2f));
    }

    private IEnumerator PauseOn(float time)
    {
        _isLocked = true;
        yield return new WaitForSecondsRealtime(time);
        _isLocked = false;
    }
}
