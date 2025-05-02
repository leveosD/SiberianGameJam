using Interfaces;
using UnityEngine;

public class MovementController : MonoBehaviour, IMovable
{
    private CharacterController _characterController;
    [SerializeField] protected float speed;
    [SerializeField] protected float mouseSensitivity;

    [SerializeField] private int damage;
    
    delegate void FakeDestroy();

    private FakeDestroy fakeDestroy;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
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
        if(gameObject.tag == "Enemy")
            Debug.Log(moveDirection);
        _characterController.Move(moveDirection * (speed * Time.fixedDeltaTime));
    }

    public void Rotate(Vector3 direction)
    {
        if(direction == Vector3.zero)
            return;
        float xRotation = direction.x * mouseSensitivity;
        transform.Rotate(new Vector3(0, xRotation, 0));
    }

    public void Shoot()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit);
        Debug.Log(transform.position + " " + transform.forward);
        var col = hit.collider;
        Debug.Log(col);
        if (col != null)
        {
            if (col.CompareTag("Player"))
            {
                col.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
            }
        }
    }
    
    public void MileAttack()
    {
        
    }
}
