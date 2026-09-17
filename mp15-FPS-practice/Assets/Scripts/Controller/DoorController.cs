using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    private Animator _animator;

    private void Awake() => CacheComponents();

    private void OnTriggerEnter(Collider other)
    {
        //true
        if (_targetLayer.Contains(other))
        {
            _animator.SetBool("IsOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //false
        if (_targetLayer.Contains(other))
        {
            _animator.SetBool("IsOpen", false);
        }
    }

    private void CacheComponents()
    {
        _animator = GetComponent<Animator>();
    }
}
