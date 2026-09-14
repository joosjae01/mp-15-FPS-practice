using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private EffectManager _grenadeImpactEffect;
    private Rigidbody _rigidBody;
    private float _explodeRange;
    private int _explodeDamage;
    private float _duration;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    private void OnDestroy()
    {
        PlayGrenadeEffect();
        Explode();
    }

    public void SetData(float range, int damage, float duration)
    {
        _explodeRange = range;
        _explodeDamage = damage;
        _duration = duration;
    }

    public void Throw(Vector3 force)
    {
        _rigidBody.AddForce(force);
        Destroy(gameObject, _duration);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explodeRange);
        foreach (Collider col in colliders)
        {
            IDamageable damageable = col.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_explodeDamage);
            }
        }
    }
    private void PlayGrenadeEffect()
    {
        Transform effectTransform = Instantiate(_grenadeImpactEffect.transform);
        effectTransform.position = transform.position;
    }
}
