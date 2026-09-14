using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    private IPoolable[] _pool;

    [field : SerializeField] public int Size { get; private set; }
    public int Count { get; private set; }

    public bool IsEmpty => Count == 0;
    public bool IsFull => Count >= Size;

    private void Awake() => Init();

    private void Init()
    {
        _pool = new IPoolable[Size];

        for (int i = 0; i < _pool.Length; i++)
        {
            GameObject go = Instantiate(_prefab);
            _pool[i] = go.GetComponent<IPoolable>();
            _pool[i].Pool = this;
            go.SetActive(false);
        }

        Count = Size;
    }

    public IPoolable Take()
    {
        if (IsEmpty) return null;

        Count--;
        IPoolable poolable = _pool[Count];
        _pool[Count] = null;
        return poolable;
    }

    public void Return(IPoolable poolable)
    {
        if (IsFull) return;

        poolable.tr.gameObject.SetActive(false);
        _pool[Count] = poolable;
        Count++;

    }
}
