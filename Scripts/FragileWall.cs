using UnityEngine;

public class FragileWall : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void Push(Vector3 direction)
    {
        _boxCollider.size = new Vector3(3.9f, 2, 0.1f);
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(direction * 5, ForceMode.Impulse);
    }
}
