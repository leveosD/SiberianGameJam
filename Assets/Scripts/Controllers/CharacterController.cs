using System;
using UnityEngine;

public class CharacterController : MonoBehaviour, IMovable
{
    private BoxCollider box;
    private Rigidbody rb;
    [SerializeField] protected float speed;
    [SerializeField] protected float mouseSensitivity;

    private float yRotation = 0f;

    private Transform cameraTransform;
    
    delegate void FakeDestroy();

    private FakeDestroy fakeDestroy;

    private void Awake()
    {
        box = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    public void Move(Vector2 direction)
    {
        var correctDirection = new Vector3(direction.y, 0, direction.x);
        
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f; // Не двигаемся вверх/вниз
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection =  forward * direction.y + right * direction.x;
        Debug.Log(moveDirection);
        rb.velocity = moveDirection.normalized * speed + new Vector3(0, rb.velocity.y, 0);
    }

    public void Rotate(Vector3 direction)
    {
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float mouseX = direction.x * mouseSensitivity;
        float mouseY = direction.y * mouseSensitivity;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        //cameraTransform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
        
        Debug.Log(mouseX + "  " + mouseY);
    }

    public void Shoot()
    {
        
    }
    
    public void MileAttack()
    {
        
    }
}
