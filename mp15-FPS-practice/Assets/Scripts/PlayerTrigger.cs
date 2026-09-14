using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    public SphereCollider _sphereCollider;
    public Transform _playerTransform;
    public bool _isPlayerOnTrigger;
    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        _isPlayerOnTrigger = false;
        _playerTransform = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_targetMask.Contains(other))
        {
            _isPlayerOnTrigger = true;
            _playerTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_targetMask.Contains(other))
        {
            _isPlayerOnTrigger = false;
            _playerTransform = null;
        }
    }
}
