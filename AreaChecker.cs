using Interfaces;
using UnityEngine;

public class AreaChecker : MonoBehaviour
{
    private BoxCollider _boxCollider;

    public BoxCollider BoxCollider
    {
        get => _boxCollider;
    }
    
    public string Tag
    {
        get;
        set;
    }

    public bool IsEnabled
    {
        set => _boxCollider.enabled = value;
    }

    [SerializeField] private int damage;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Area checker: " + other.tag);
        if (other.CompareTag(Tag))
        {
            var damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);
        }
    }
}
