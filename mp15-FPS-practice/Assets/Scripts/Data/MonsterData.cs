using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterData : MonoBehaviour
{
    [field: SerializeField] public string Name { get; set; }

    // [ Monster IDamageable ]
    [SerializeField] private int _currentHealth;
    public ObservableProperty<int> CurrentHealth = new(0);
    [field: SerializeField] public int MaxHealth { get; set; }
    [field: SerializeField] public int AttackDamage { get; set; }

    private void Start() => Init();

    private void Init()
    {
        CurrentHealth.Value = _currentHealth;
    }
}
