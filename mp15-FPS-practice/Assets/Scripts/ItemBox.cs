using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour, IInteractable
{
    public GameObject GameObject { get => gameObject; }
    private Outline _ountline;

    private void Awake() => CacheComponents();
    private void Start() => Initialize();

    public void Targeting()
    {
        _ountline.enabled = true;
    }

    public void Untargeting()
    {
        _ountline.enabled = false;
    }

    public void Interact(IInteractor owner)
    {
        Destroy(gameObject);
    }

    private void CacheComponents()
    {
        _ountline = GetComponent<Outline>();
    }

    private void Initialize()
    {
        _ountline.enabled = false;
    }
}
