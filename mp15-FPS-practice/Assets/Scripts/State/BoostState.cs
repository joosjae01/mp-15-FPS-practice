using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostState : MonoBehaviour, IState
{
    private float _boostSpeed;
    private float _boostCoolDown;
    private PlayerData _playerData;
    private WeaponData _weaponData;

    private void OnDestroy()
    {
        EndState();
    }

    public void SetPlayerData(PlayerData playerData, WeaponData weaponData ,float boostSpeed, float boostCoolDown, float duration)
    {
        _playerData = playerData;
        _weaponData = weaponData;
        _boostSpeed = boostSpeed;
        _boostCoolDown = boostCoolDown;

        StartState();
        Destroy(gameObject, duration);
    }

    public void StartState()
    {
        _playerData.MoveSpeed += _boostSpeed;
        _weaponData.CoolDown *= _boostCoolDown;
    }

    public void EndState()
    {
        _playerData.MoveSpeed -= _boostSpeed;
        _weaponData.CoolDown /= _boostCoolDown;
    }
}
