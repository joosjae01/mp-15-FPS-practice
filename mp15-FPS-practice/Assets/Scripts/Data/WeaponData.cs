using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    // [ Player Weapon ]
    [field: SerializeField] public float CoolDown { get; set; }
    [field: SerializeField] public float ReloadTime { get; set; }
    [field: SerializeField] public float Range { get; set; }
    [field: SerializeField] public int Damage { get; set; }

    [SerializeField] private int _currentAmmo;
    public ObservableProperty<int> CurrentAmmo = new(0);

    [SerializeField] private int _maxAmmo;
    public ObservableProperty<int> MaxAmmo = new(0);

    private void Start()
    {
        CurrentAmmo.Value = _currentAmmo;
        MaxAmmo.Value = _maxAmmo;
    }
}
