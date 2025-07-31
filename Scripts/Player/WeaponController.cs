using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private const int GUNS = 2;
    
    /*private List<Weapon> _weapons = new List<Weapon>(new Weapon[GUNS]);
    private List<BulletManager> _bulletManagers = new List<BulletManager>(new BulletManager[GUNS]);
    private List<WeaponAnimationController> _animators = new List<WeaponAnimationController>(new WeaponAnimationController[GUNS]);
    private List<SoundController> _soundControllers = new List<SoundController>(new SoundController[GUNS]);*/

    [SerializeField] private List<Weapon> _weapons;
    private List<BulletManager> _bulletManagers = new List<BulletManager>();
    private List<WeaponAnimationController> _animators = new List<WeaponAnimationController>();
    private List<SoundController> _soundControllers = new List<SoundController>();
    
    [SerializeField] private Transform playerCanvas;
    [SerializeField] private Transform mainCanvas;

    private SoundController _currentSoundController;

    private Weapon _currentWeapon;
    private WeaponAnimationController _currentAnimator;
    private BulletManager _currentBulletManager;
    
    private bool _isDelayed = false;

    private int _count;
    private string _weaponCounter = "WeaponCounter";
    public Weapon CurrentWeapon
    {
        get => _currentWeapon;
        set
        {
            if (_currentWeapon != null)
            {
                _currentBulletManager.gameObject.SetActive(false);
            }

            StartCoroutine(SwitchAnimators(_animators[value.Id]));
            _currentWeapon = value;
            _currentBulletManager = _bulletManagers[value.Id];
            _currentSoundController = _soundControllers[value.Id];
            
            _currentBulletManager.gameObject.SetActive(true);
        }
    }

    [SerializeField] private string targetTag;

    private void Awake()
    {
        Debug.Log("PlayerPrefs.HasKey: " + PlayerPrefs.HasKey(_weaponCounter));
        if (PlayerPrefs.HasKey(_weaponCounter))
        {
            _count = PlayerPrefs.GetInt(_weaponCounter);
            Debug.Log("Weapon counter: " + _count);
            if (_count != -1)
            {
                for (int i = 0; i <= _count; i++)
                {
                    Debug.Log("Add weapon: " + i);
                    AddWeapon(_weapons[i]);
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt(_weaponCounter, -1);
            _count = -1;
        }
    }

    private void OnEnable()
    {
        InputController.PlayerDead += Reset;
    }

    private void OnDisable()
    {
        foreach (var weapon in _weapons)
        {
            weapon.Ammo = weapon.MaxAmmo;
        }

        InputController.PlayerDead -= Reset;
    }

    public void AddWeapon(Weapon weapon)
    {
        int id = weapon.Id;
        if (_count < weapon.Id)
        {
            PlayerPrefs.SetInt(_weaponCounter, weapon.Id);
            Debug.Log("Wepon counter is set to " + PlayerPrefs.GetInt(_weaponCounter) + " ID: " + weapon.Id);
            _count = weapon.Id;
        }

        //_weapons.Add(weapon);
        _bulletManagers.Add(Instantiate(weapon.UIClip, mainCanvas).GetComponent<BulletManager>());
        _animators.Add(Instantiate(weapon.UIWeapon,playerCanvas).GetComponent<WeaponAnimationController>());
        _soundControllers.Add(Instantiate(weapon.SoundController.GetComponent<SoundController>(), transform));
        
        CurrentWeapon = weapon;
    }

    public void Shoot()
    {
        if (_isDelayed || _currentWeapon == null)
            return;

        _isDelayed = true;
        StartCoroutine(ShotDelay());
        
        if (_currentWeapon.Ammo == 0)
        {
            _currentAnimator.EmptyClip();
            _currentSoundController.PlayClip(1);
            return;
        }
        
        _currentAnimator.Shot();
        _currentSoundController.PlayClip(0);
        
        _currentWeapon.Ammo -= 1;
        _currentBulletManager.Shot(_currentWeapon.BPS);
        
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _currentWeapon.Distance);
        var col = hit.collider;
        if (col != null)
        {
            if (col.isTrigger && col.CompareTag(targetTag))
            {
                Debug.Log("It's got a hit");
                col.gameObject.GetComponentInParent<IDamageable>().TakeDamage(_currentWeapon.Damage);
            }
            else if(col.CompareTag("FragileWall") && _currentWeapon.Id == 1)
            {
                col.gameObject.GetComponent<FragileWall>().Push(transform.forward);
            }
        }
    }

    private IEnumerator ShotDelay()
    {
        yield return new WaitForSecondsRealtime(_currentWeapon.ShotDelay);
        _isDelayed = false;
    }

    public void Reload()
    {
        if( _currentWeapon == null || _isDelayed)
            return;
        if(_currentWeapon.Ammo == _currentWeapon.MaxAmmo)
            return;
        _isDelayed = true;
        _currentAnimator.Reload();
        _currentSoundController.PlayClip(2);
        _currentWeapon.Ammo = _currentWeapon.MaxAmmo;
        _currentBulletManager.Reload();
        StartCoroutine(ReloadDelay());
    }
    
    private IEnumerator ReloadDelay()
    {
        yield return new WaitForSecondsRealtime(_currentWeapon.ReloadDelay);
        _isDelayed = false;
    }

    public void ChooseWeapon(int index)
    {
        if(_currentWeapon == null || index == _currentWeapon.Id || index >= _weapons.Count || _weapons[index] == null)
            return;
        CurrentWeapon = _weapons[index];
    }

    private void Reset()
    {
        for(int i = 0; i < _count; i++)
        {
            _bulletManagers[i].Reload();
            _weapons[i].Ammo = _weapons[i].MaxAmmo;
        }
    }

    private IEnumerator SwitchAnimators(WeaponAnimationController weaponAnimationController)
    {
        if (_currentAnimator)
        {
            _currentAnimator.TakeOff();
            yield return new WaitUntil(() => !_currentAnimator.gameObject.activeSelf);
        }
        _currentAnimator = weaponAnimationController;
        _currentAnimator.gameObject.SetActive(true);
    }
}
