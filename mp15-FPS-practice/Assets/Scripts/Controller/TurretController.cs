using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour, IDamageable
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private PlayerTrigger _trigger;

    [Header("Bullet")]
    [SerializeField] private BulletController _bulletPrefab;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletReturnDelay;
    [SerializeField] private EffectManager _destroyEffect;
    private TurretData _turretData;
    private WaitForSeconds _waitCoolDown;
    private Coroutine _fireRoutine;

    private bool _isPlayerInTrigger => _trigger._playerTransform != null;

    public GameObject GameObject { get => gameObject; }

    private bool _isPlayerInSight = false;

    private void Awake()
    {
        _turretData = GetComponent<TurretData>();
        _waitCoolDown = new WaitForSeconds(_turretData.Cooldown);
    }

    private void Start()
    {

    }

    private void Update()
    {
        RayShotToPlayer();
        Rotate();
        RotateToPlayer();
    }

    private void RunFireRoutine()
    {
        if (_fireRoutine != null) return;
        _fireRoutine = StartCoroutine(FireRoutine());
    }

    private void StopFireRoutine()
    {
        if (_fireRoutine == null) return;
        StopCoroutine(_fireRoutine);
        _fireRoutine = null;
    }

    private void RayShotToPlayer()
    {
        _isPlayerInSight = false;
        if (!_isPlayerInTrigger) return;

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
    }
    private void RotateToPlayer()
    {
        if (_isPlayerInSight && _isPlayerInTrigger)
        {
            Vector3 look = new Vector3(
            _trigger._playerTransform.position.x,
            _headTransform.position.y,
            _trigger._playerTransform.position.z
            );
            _headTransform.LookAt(look);

            RunFireRoutine();
        }
        else
        {
            StopFireRoutine();
        }
    }

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            yield return _waitCoolDown;
            SpawnBullet();
        }
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