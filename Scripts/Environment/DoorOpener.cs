using System.Collections;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private bool _isClosed = true;

    private Vector3 startPosition;
    private Quaternion startRotation;

    [SerializeField] private float angle;

    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    
    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.localRotation;

        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        InputController.PlayerDead += Reset;
    }

    private void OnDisable()
    {
        InputController.PlayerDead -= Reset;
    }

    private void Reset()
    {
        _boxCollider.enabled = true;
        transform.position = startPosition;
        transform.localRotation = startRotation;
        _isClosed = true;
        _rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationY;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player") && _isClosed)
        {
            _isClosed = false;
            Quaternion delta = Quaternion.Euler(0, 40 * Time.fixedDeltaTime, 0);
            _rigidbody.MoveRotation(_rigidbody.rotation * delta);
            StartCoroutine(Rotate());
        }
    }

    private IEnumerator Rotate()
    {
        yield return new WaitUntil(() => Mathf.Abs(angle - transform.rotation.eulerAngles.y) <= 2);
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.constraints |= RigidbodyConstraints.FreezeRotationY;
        _boxCollider.enabled = false;
    }
}
