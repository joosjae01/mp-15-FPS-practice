using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentAmmo;
    [SerializeField] private TextMeshProUGUI _maxAmmo;
    [SerializeField] private TextMeshProUGUI _health;

    public void RefreshCurrentAmmoUI(int Ammo)
    {
        _currentAmmo.text = $"{Ammo}";
    }

    public void RefreshMaxAmmoUI(int MaxAmmo)
    {
        _maxAmmo.text = $"{MaxAmmo}";
    }

    public void RefreshHealthUI(int Health)
    {
        _health.text = $"{Health}";
    }
}