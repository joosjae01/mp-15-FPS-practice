using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private PlayerCombatUIController _combatUI;

    [SerializeField] private KeyCode _grenadeKey = KeyCode.Alpha3;
    [SerializeField] private KeyCode _fireKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;
    
    [SerializeField] private EffectManager _bulletEffectPrefab;
    [SerializeField] private EffectManager _flameEffect;
    
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private Grenade _grenadePrefab;
    [SerializeField] private float _grenadeDuration;
    [SerializeField] private float _maxGrenadeForce;
    [SerializeField] private float _grenadeRange;
    [SerializeField] private int _grenadeDamage;

    private WaitForSeconds _WaitCoolDown;


    private WeaponData _weaponData;
    private Transform _cameraTransform;
    private Outline _outline;

    private float _grenadeForce;

    private bool _isDetachedGrenade => Input.GetKeyUp(_grenadeKey);
    private bool _isPressedReload => Input.GetKeyDown(_reloadKey);
    private bool _isPressedGrenade => Input.GetKey(_grenadeKey);
    private bool _isPressedFire => Input.GetKey(_fireKey);
    private bool _isOnFire = true;

    private bool _canFire => _isPressedFire && _isAmmoEnough && !_isReloading && _isOnFire;
    private bool _isAmmoEnough => _weaponData.CurrentAmmo.Value > 0;

    
    private bool _canReload => _isPressedReload && !_isReloading;
    private bool _isReloading = false;


    private void Awake() => CacheComponents();
    private void Start() => Init();
    private void Update() {
        ChargeGrenade();
    }

    public void ThrowGrenade()
    {
        if(!_isDetachedGrenade) return;

        Grenade grenade = Instantiate(_grenadePrefab, transform.position + transform.forward * 1.5f , transform.rotation);
        grenade.SetData(_grenadeRange, _grenadeDamage, _grenadeDuration);

        grenade.Throw(transform.forward * _grenadeForce + transform.up * _grenadeForce);
        _grenadeForce = 0;
    }

    private IEnumerator FireRoutine()
    {
        yield return _WaitCoolDown;
        _isOnFire = true;
    }

    public void Fire()
    {
        if (!_canFire) return;

        _weaponData.CurrentAmmo.Value--;
        PlayFlameEffect();
        IDamageable target = GetDamageable();

        if (target != null) target.TakeDamage(_weaponData.Damage);
        _isOnFire = false;
        StartCoroutine(FireRoutine());
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

    public void Reload()
    {
        if(_canReload) StartCoroutine(ReloadRoutine());
    }

    public IEnumerator ReloadRoutine()
    {
        _isReloading = true;
        _combatUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(_weaponData.ReloadTime);

        _isReloading = false;
        _combatUI.gameObject.SetActive(false);
        _weaponData.CurrentAmmo.Value = _weaponData.MaxAmmo;

        yield break;

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

    private void Init()
    {
        _outline.enabled = true;
        _combatUI.SetWeaponData(_weaponData);
        _combatUI.gameObject.SetActive(false);
        _weaponData.CurrentAmmo.Value = _weaponData.MaxAmmo;
        _WaitCoolDown = new WaitForSeconds(_weaponData.CoolDown);
    }
}