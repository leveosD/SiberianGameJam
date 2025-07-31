using UnityEngine;

public class ObjectRotator : MonoBehaviour, IRotatable
{
    private Transform _playerTransform;
    
    private void Awake()
    {
        _playerTransform = FindObjectOfType<InputController>().transform;
    }

    public void Rotate(Vector3 direction)
    {
        transform.forward = -direction;
    }

    private void Update()
    {
        var playerVector = (_playerTransform.position - transform.position).normalized;
        Rotate(playerVector);
    }
}
