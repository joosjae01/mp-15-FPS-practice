using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUIBinder : MonoBehaviour
{
    [SerializeField] private MonsterData _monsterData;
    [SerializeField] private MonsterHealthGauge _healthGauge;

    private void OnEnable()
    {
        BindMonsterUI();
    }

    private void OnDisable()
    {
        UnBindMonsterUI();
    }

    private void BindMonsterUI()
    {
        _monsterData.CurrentHealth.AddListener(_healthGauge.RefreshHealthBar);
    }

    private void UnBindMonsterUI()
    {
        _monsterData.CurrentHealth.RemoveListener(_healthGauge.RefreshHealthBar);
    }

    private void OnDestroy()
    {
        _monsterData.CurrentHealth.RemoveAllListeners();
    }
}
