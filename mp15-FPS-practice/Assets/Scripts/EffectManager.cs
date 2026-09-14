using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private bool _isDestroy = false;
    [SerializeField] private bool _playInStart = false;
    private float _elapsed;

    private void Start() => gameObject.SetActive(_playInStart);

    private void Update()
    {
        UpdateCurrentDelay();
        Deactivate();
    }

    public void PlayAnimation()
    {
        ResetElapsed();
    }

    private void ResetElapsed()
    {
        _elapsed = 0;
    }

    private void UpdateCurrentDelay()
    {
        _elapsed += Time.deltaTime;
    }

    private void Deactivate()
    {
        if (_elapsed < _delay) return;

        gameObject.SetActive(false);

        if (_isDestroy) Destroy(gameObject);
    }
}
