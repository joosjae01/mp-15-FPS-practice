using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCombatUIController : MonoBehaviour
{
    [SerializeField] private Image _currentReload;
    [SerializeField] private TextMeshProUGUI _currentMessage;
    [SerializeField] private float _dotUpdatePeriod;
    private WeaponData _weaponData;
    private float _elpased = 0;
    private float _attackTime = 0;
    private bool _isDotUpdated => _elpased >= _dotUpdatePeriod;

    int dotCount = 0;
    string dot;

    private void OnDisable()
    {
        _attackTime = 0;
    }

    private void Update()
    {
        RefreshDot();
        RefreshMessage();
        RefreshReloadProgress();
    }

    private void RefreshMessage()
    {
        dot = "";
        for(int i = 0; i < dotCount; i++)
        {
            dot += '.';
        }
        _currentMessage.text = "재장전 중" + dot;
    }

    private void RefreshReloadProgress()
    {
        _currentReload.fillAmount = _attackTime / _weaponData.ReloadTime;
        _attackTime += Time.deltaTime;
    }

    private void RefreshDot()
    {
        if (_isDotUpdated)
        {
            if (dotCount >= 3) dotCount = 0;

            dotCount++;
            _elpased = 0;
        }
        _elpased += Time.deltaTime;
    }

    public void SetWeaponData(WeaponData weaponData)
    {
        _weaponData = weaponData;
    }
}