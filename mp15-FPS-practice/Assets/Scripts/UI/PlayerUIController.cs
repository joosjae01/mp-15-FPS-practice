using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammo;
    [SerializeField] private TextMeshProUGUI _health;

    private WeaponData _weaponData;
    private PlayerData _playerData;

    private void Awake() => CacheComponents();

    public void RefreshAmmoUI(int Ammo)
    {
        _ammo.text = $"{Ammo} / {_weaponData.MaxAmmo}";
    }

    public void RefreshHealthUI(int Health)
    {
        _health.text = $"{Health}";
    }

    private void CacheComponents()
    {
        _weaponData = GetComponentInChildren<WeaponData>();
        _playerData = GetComponent<PlayerData>();
    }
}