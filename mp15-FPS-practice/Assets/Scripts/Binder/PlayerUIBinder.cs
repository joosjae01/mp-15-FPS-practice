using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIBinder : MonoBehaviour
{
    [SerializeField] private PlayerUIController _playerUI;
    private WeaponData _weaponData;
    private PlayerData _playerData;

    private void Awake() => CacheComponents();
    private void OnEnable() => BindPlayerUI();
    private void OnDisable() => UnBindPlayerUI();
    private void OnDestroy() => UnBindAll();

    private void BindPlayerUI()
    {
        _weaponData.CurrentAmmo.AddListener(_playerUI.RefreshAmmoUI);
        _playerData.CurrentHealth.AddListener(_playerUI.RefreshHealthUI);
    }

    private void UnBindPlayerUI()
    {
        _weaponData.CurrentAmmo.RemoveListener(_playerUI.RefreshAmmoUI);
        _playerData.CurrentHealth.RemoveListener(_playerUI.RefreshHealthUI);
    }

    private void UnBindAll()
    {
        _weaponData.CurrentAmmo.RemoveAllListeners();
        _playerData.CurrentHealth.RemoveAllListeners();
    }

    private void CacheComponents()
    {
        _weaponData = GetComponentInChildren<WeaponData>();
        _playerData = GetComponent<PlayerData>();
    }
}
