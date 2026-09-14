using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StimPack : MonoBehaviour, IInteractable
{
    [SerializeField] private float _boostSpeed;
    [SerializeField] private float _boostCoolDown;
    [SerializeField] private float _boostDuration;
    [SerializeField] private BoostState _boostStatePrefab;

    public GameObject GameObject { get => gameObject; }

    private Outline _ountline;

    private void Awake()
    {
        _ountline = GetComponent<Outline>();
    }

    private void Start()
    {
        _ountline.enabled = false;
    }
    public void Interact(IInteractor owner)
    {
        PlayerData playerData = owner.GameObject.GetComponent<PlayerData>();
        WeaponData weaponData = owner.GameObject.GetComponentInChildren<WeaponData>();
        BoostState boostState = Instantiate(_boostStatePrefab);

        boostState.SetPlayerData(playerData, weaponData, _boostSpeed, _boostCoolDown, _boostDuration);

        (owner as IDamageable).TakeDamage(10);
        Destroy(gameObject);
    }

    public void Targeting()
    {
        _ountline.enabled = true;
    }

    public void Untargeting()
    {
        _ountline.enabled = false;
    }
}
