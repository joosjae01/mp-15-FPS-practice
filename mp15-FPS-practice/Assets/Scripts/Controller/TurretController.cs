using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TurretController : MonoBehaviour, IDamageable
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private PlayerTrigger _trigger;
    [SerializeField] private float _rayShotDelay;

    [Header("Bullet")]
    [SerializeField] private BulletController _bulletPrefab;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletReturnDelay;
    [SerializeField] private EffectManager _destroyEffect;
    private TurretData _turretData;
    private WaitForSeconds _waitCoolDown;
    private WaitForSeconds _waitRayShot;

    private bool _isOnFire = true;
    private bool _canRayShot = true;
    private bool _isPlayerInTrigger => _trigger._playerTransform != null;

    public GameObject GameObject { get => gameObject; }

    private bool _isPlayerInSight = false;

    private void Awake()
    {
        _turretData = GetComponent<TurretData>();
        _waitCoolDown = new WaitForSeconds(_turretData.Cooldown);
        _waitRayShot = new WaitForSeconds(_rayShotDelay);
    }

    private void Update()
    {
        RayShotToPlayer();
        Rotate();
        Fire();
    }

    private void RayShotToPlayer()
    {
        _isPlayerInSight = false;
        if (!_isPlayerInTrigger || !_canRayShot) return;

        Vector3 from = new Vector3(
            transform.position.x,
            transform.position.y + _muzzlePoint.position.y,
            transform.position.z
            );

        Vector3 to = new Vector3(
            _trigger._playerTransform.position.x,
            _trigger._playerTransform.position.y + _muzzlePoint.position.y,
            _trigger._playerTransform.position.z
            );

        Ray ray = new Ray(from, (to - from).normalized);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _trigger._sphereCollider.radius, _targetMask))
        {
            if (_playerMask.Contains(hit.transform))
            {
                _isPlayerInSight = true;
            }
        }

        StartCoroutine(RayShotRoutine());
    }

    private IEnumerator RayShotRoutine()
    {
        _canRayShot = false;
        yield return _waitRayShot;
        _canRayShot = true;
    }

    private void Fire()
    {
        if (!_isPlayerInSight || !_isPlayerInTrigger) return;

        Vector3 look = new Vector3(
        _trigger._playerTransform.position.x,
        _headTransform.position.y,
        _trigger._playerTransform.position.z
        );
        _headTransform.LookAt(look);

        if (_isOnFire)
        {
            SpawnBullet();
            _isOnFire = false;
            StartCoroutine(FireRoutine());
        }
    }

    private IEnumerator FireRoutine()
    {
        yield return _waitCoolDown;
        _isOnFire = true;
    }

    private void Rotate()
    {
        if (_isPlayerInSight) return;

        _headTransform.Rotate(Vector3.up * _turretData.RotateSpeed * Time.deltaTime);
    }

    private void SpawnBullet()
    {
        IPoolable bullet = _turretData.BulletPool.Take();
        bullet.tr.position = _muzzlePoint.position;
        bullet.tr.rotation = _muzzlePoint.rotation;
        bullet.tr.gameObject.SetActive(true);
        (bullet as BulletController).SetData(_bulletDamage, _bulletSpeed, _bulletReturnDelay);
    }

    public void TakeDamage(int damage)
    {
        if (_turretData.CurrentHealth.Value > damage)
        {
            _turretData.CurrentHealth.Value -= damage;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}