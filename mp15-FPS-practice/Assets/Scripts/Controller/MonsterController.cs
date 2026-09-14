using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerTrigger _trigger;
    [SerializeField] private MonsterData _monsterData;
    public GameObject GameObject { get => gameObject; }
    public LayerMask _playerMask;

    public void TakeDamage(int damage)
    {
        if( _monsterData.CurrentHealth.Value > damage)
        {
            _monsterData.CurrentHealth.Value -= damage;
        } 
        
        else if(_monsterData.CurrentHealth.Value <= damage)
        {
            Destroy(gameObject);
        }
    }
}