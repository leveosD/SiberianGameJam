using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "MyAssets/Items/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private int damage;
    [SerializeField] private int ammo;
    [SerializeField] private int maxAmmo;
    [SerializeField] private int bps;
    [SerializeField] private float shotDelay;
    [SerializeField] private float reloadDelay;
    [SerializeField] private int distance;
    [SerializeField] private GameObject uiWeapon;
    [SerializeField] private GameObject uiClip;
    [SerializeField] private GameObject soundController;
    
    public int Id
    {
        get => id;
    }

    public int Damage
    {
        get => damage;
    }

    public int Ammo
    {
        get => ammo;
        set
        {
            ammo = value;
        }
    }

    public int MaxAmmo
    {
        get => maxAmmo;
    }

    public int BPS
    {
        get => bps;
    }

    public float ShotDelay
    {
        get => shotDelay;
    }
    
    public float ReloadDelay
    {
        get => reloadDelay;
    }
    
    public int Distance
    {
        get => distance;
    }
    
    
    public GameObject UIWeapon
    {
        get => uiWeapon;
    }
    
    public GameObject UIClip
    {
        get => uiClip;
    }
    
    public GameObject SoundController
    {
        get => soundController;
    }
}
