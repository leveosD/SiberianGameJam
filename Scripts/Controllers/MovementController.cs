using Interfaces;
using UnityEngine;

public class MovementController : MonoBehaviour, IMovable
{
    //private CharacterController _characterController;
    private Rigidbody _rigidbody;
    
    [SerializeField] protected float speed;
    [SerializeField] protected float mouseSensitivity;

    private float _angle;
    
    delegate void FakeDestroy();

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _angle = transform.localRotation.y;
        //_characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 direction)
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0f; // Не двигаемся вверх/вниз
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection =  forward * direction.y + right * direction.x;
        _rigidbody.velocity = moveDirection * (speed * Time.fixedDeltaTime);
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
        //_characterController.Move(moveDirection * (speed * Time.fixedDeltaTime));
    }

    public void Rotate(Vector3 direction)
    {
        float xRotation = direction.x * mouseSensitivity;
        _angle += xRotation;
        transform.localRotation = Quaternion.Euler(new Vector3(0, _angle, 0));
        //transform.Rotate(new Vector3(0, xRotation, 0));
    }
    
    public void MileAttack()
    {
        
    }
}
