using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask _playerMask;
    private int _damage;
    private float _speed;

    private float _returnDelay;
    private float _elapsed;

    public ObjectPool Pool { get; set; }

    public Transform tr { get => transform; }

    private bool _isTimeover => _elapsed >= _returnDelay;

    private void OnTriggerEnter(Collider other)
    {
        if (_playerMask.Contains(other))
        {
            other.GetComponent<IDamageable>().TakeDamage(_damage);
        }

        ReturnToPool();
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    private void Start()
    {
        Debug.Log("Start");
    }

    private void Update()
    {
        MoveForward();
        RefreshElapsed();
    }

    public void SetData(int damage, float speed, float destroyDelay)
    {
        _damage = damage;
        _speed = speed;
        _returnDelay = destroyDelay;
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void RefreshElapsed()
    {
        _elapsed += Time.deltaTime;

        if (_isTimeover)
        {
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        Pool.Return(this);
        _elapsed = 0;
    }
}
