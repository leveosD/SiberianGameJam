using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons = new List<Weapon>();
    [SerializeField] private Animator animator;
    [SerializeField] private BulletManager bulletManager;

    private SoundController _soundController;

    private Weapon _currentWeapon;
    private int _currentAmmo;

    private bool _isDelayed = false;

    public Weapon CurrentWeapon
    {
        get => _currentWeapon;
        set
        {
            _currentWeapon = value;
            animator.runtimeAnimatorController = value.Anim;
        }
    }

    [SerializeField] private string targetTag;

    private void Start()
    {
        _currentWeapon = _weapons[0];
        _currentAmmo = _currentWeapon.Ammo;

        _soundController = GetComponentInChildren<SoundController>();
    }

    public void AddWeapon(Weapon weapon)
    {
        _weapons.Add(weapon);
    }

    public void Shoot()
    {
        if (_isDelayed)
            return;

        _isDelayed = true;
        StartCoroutine(ShotDelay());
            
        if (_currentAmmo == 0)
        {
            animator.Play("EmptyClip");
            _soundController.PlayClip(4);
            return;
        }
        
        animator.Play("Shot");
        _soundController.PlayClip(0);
        
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit);
        var col = hit.collider;
        if (col != null)
        {
            if (col.CompareTag(targetTag))
            {
                col.gameObject.GetComponentInParent<IDamageable>().TakeDamage(_currentWeapon.Damage);
            }
        }

        _currentAmmo -= 1;
        bulletManager.Shot();
    }

    private IEnumerator ShotDelay()
    {
        yield return new WaitForSecondsRealtime(_currentWeapon.Delay);
        _isDelayed = false;
    }

    public void Reload()
    {
        animator.Play("Reload");
        _currentAmmo = _currentWeapon.Ammo;
        bulletManager.Reload();
    }
}
