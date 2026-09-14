using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthGauge : MonoBehaviour
{
    [SerializeField] private Image _healthImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private float _duration;

    private MonsterData _monsterData;
    private Camera _camera;

    private float _elapsed;
    private bool _isFinished => _elapsed >= _duration;
    private bool _isActivated = true;

    private void Awake() => CacheComponents();
    private void Start()
    {
        HideSelf();
    }
    private void Update()
    {
        RotateToPlayer();
        RefreshTimer();
    }

    public void RefreshHealthBar(int currentHealth)
    {
        if (_isActivated)
        {
            _isActivated = false;
            return;
        }

        gameObject.SetActive(true);
        SetTimer();
        _healthImage.fillAmount = (float)currentHealth / (float)_monsterData.MaxHealth;
        _nameText.text = _monsterData.Name;
    }

    private void RotateToPlayer()
    {
        transform.LookAt(_camera.gameObject.transform.position);
    }

    private void SetTimer()
    {
        _elapsed = 0;
    }

    private void RefreshTimer()
    {
        _elapsed += Time.deltaTime;
        if (_isFinished)
        {
            HideSelf();
        }
    }

    private void CacheComponents()
    {
        _camera = Camera.main;
        _monsterData = GetComponentInParent<MonsterData>();
    }

    private void HideSelf()
    {
        gameObject.SetActive(false);
    }
}
