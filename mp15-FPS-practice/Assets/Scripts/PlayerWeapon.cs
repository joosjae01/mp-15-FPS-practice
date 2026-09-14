using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private KeyCode _fireKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;
    [SerializeField] private KeyCode _grenadeKey = KeyCode.Alpha3;
    [SerializeField] private PlayerCombatUIController _combatUI;
    [SerializeField] private EffectManager _flameEffect;
    [SerializeField] private EffectManager _bulletEffectPrefab;
    [SerializeField] private Grenade _grenadePrefab;
    [SerializeField] private float _grenadeRange;
    [SerializeField] private int _grenadeDamage;
    [SerializeField] private float _grenadeDuration;
    [SerializeField] private float _maxGrenadeForce;
    [SerializeField] private LayerMask _targetMask;

    private WeaponData _weaponData;
    
    private Outline _outline;
    private Transform _cameraTransform;
    private float _currentCooldown;
    private float _currentReload;
    private float _grenadeForce;    
    private bool _isPressedReload => Input.GetKeyDown(_reloadKey);
    private bool _isPressedGrenade => Input.GetKey(_grenadeKey);
    private bool _isDetachedGrenade => Input.GetKeyUp(_grenadeKey);
    private bool _isOnTime => _currentCooldown >= _weaponData.CoolDown;
    private bool _isPressedFire => Input.GetKey(_fireKey);
    private bool _isAmmoEnough => _weaponData.CurrentAmmo.Value > 0;
    private bool _canFire => _isPressedFire && _isAmmoEnough && _isOnTime && !_isReloading;

    private bool _isReloadOnTime => _currentReload >= _weaponData.ReloadTime;
    private bool _isReloading = false;
    private bool _canReload => _isPressedReload && !_isReloading;

    private void Awake() => CacheComponents();
    private void Start()
    {
        _outline.enabled = true;
        _combatUI.SetWeaponData(_weaponData);
        _combatUI.gameObject.SetActive(false);
        _weaponData.CurrentAmmo.Value = _weaponData.MaxAmmo;
    }
    private void Update() {
        UpdateCurrentCooldown();
        ChargeGrenade();
    }

    public void Fire()
    {
        if (!_canFire) return;

        _weaponData.CurrentAmmo.Value--;
        _currentCooldown = 0;
        PlayFlameEffect();

        IDamageable target = GetDamageable();

        if (target == null) return;

        target.TakeDamage(_weaponData.Damage);
    }

    public void ThrowGrenade()
    {
        if(!_isDetachedGrenade) return;

        Grenade grenade = Instantiate(_grenadePrefab, transform.position + transform.forward * 1.5f , transform.rotation);
        grenade.SetData(_grenadeRange, _grenadeDamage, _grenadeDuration);

        grenade.Throw(transform.forward * _grenadeForce + transform.up * _grenadeForce);
        _grenadeForce = 0;
    }

    private void ChargeGrenade()
    {
        if (!_isPressedGrenade) return;

        _grenadeForce += 500f * Time.deltaTime;

        if (_maxGrenadeForce <= _grenadeForce)
        {
            _grenadeForce = _maxGrenadeForce;
        }
    }

    public void StartReload()
    {
        if (!_canReload) return;

        _isReloading = true;
        _combatUI.gameObject.SetActive(true);
    }

    public void EndReload()
    {
        if (!_isReloading) return;

        _currentReload += Time.deltaTime;

        if(_isReloadOnTime)
        {
            _currentReload = 0;
            _isReloading = false;
            _combatUI.gameObject.SetActive(false);

            _weaponData.CurrentAmmo.Value = _weaponData.MaxAmmo;
        }
    }

    private IDamageable GetDamageable()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;

        IDamageable damageable = null;
        

        if (Physics.Raycast(ray, out hit, _weaponData.Range, _targetMask))
        {
            PlayBulletEffect(hit);
            damageable = hit.transform.GetComponent<IDamageable>();
        }

        return damageable;
    }

    private void UpdateCurrentCooldown()
    {
        if (_isOnTime) return;
        _currentCooldown += Time.deltaTime;
    }

    private void PlayFlameEffect()
    {
        _flameEffect.gameObject.SetActive(true);
        _flameEffect.PlayAnimation();
    }

    private void PlayBulletEffect(RaycastHit hit)
    {
        Transform effectTransform = Instantiate(_bulletEffectPrefab.transform);
        effectTransform.position = hit.point;
        effectTransform.forward = hit.normal;
    }

    private void CacheComponents()
    {
        _cameraTransform = Camera.main.transform;
        _weaponData = GetComponent<WeaponData>();
        _outline = GetComponentInChildren<Outline>();
    }
}