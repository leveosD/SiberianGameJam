using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "MyAssets/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private int ammo;
    [SerializeField] private float delay;
    [SerializeField] private Animator animator;
    
    public int Damage
    {
        get => damage;
    }

    public int Ammo
    {
        get => ammo;
    }

    public float Delay
    {
        get => delay;
    }
    
    public Animator Anim
    {
        get => animator;
    }
}
