using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealState : MonoBehaviour, IState
{
    PlayerData _playerData;
    private int _healAmount;
    public void SetPlayerData(PlayerData playerData, int amount)
    {
        _playerData = playerData;
        _healAmount = amount;
        StartState();
        Destroy(gameObject);
    }
    public void StartState()
    {
        _playerData.CurrentHealth.Value += _healAmount;
    }
    public void EndState()
    {
        Debug.Log($"HealState : 플레이어의 체력 회복 (현재 체력 : {_playerData.CurrentHealth.Value})");
    }
}
