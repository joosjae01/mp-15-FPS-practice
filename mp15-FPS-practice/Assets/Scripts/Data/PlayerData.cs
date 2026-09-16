using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // [ Player IDamageable ]
    [field:SerializeField] public int MaxHealth { get; set; }
    [SerializeField] private int _currentHealth;
    public ObservableProperty<int> CurrentHealth = new(0);

    // [ Player Movement ]
    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public float JumpPower { get; set; }

    private void Start() => Init();

    private void Init()
    {
        CurrentHealth.Value = _currentHealth;
    }
}
