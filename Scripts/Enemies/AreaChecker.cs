using Interfaces;
using UnityEngine;

public class AreaChecker : MonoBehaviour
{
    private BoxCollider _boxCollider;
    [SerializeField] private EnemySoundController soundController;

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
        if (other.CompareTag(Tag))
        {
            soundController.PlayClip(1);
            var damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);
        }
    }
}
