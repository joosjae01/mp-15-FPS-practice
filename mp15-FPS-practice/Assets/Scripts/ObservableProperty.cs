using System;

public class ObservableProperty<T>
{
    private Action<T> _onValueChanged;
    private T _value;
    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            Notify();
        }
    }

    public ObservableProperty(T initialValue)
    {
        _value = initialValue;
    }

    public void AddListener(Action<T> listener)
    {
        _onValueChanged += listener;
    }

    public void RemoveListener(Action<T> listener)
    {
        _onValueChanged -= listener;
    }

    // 해당 델리게이트를 사용하고 있던 객체가 파괴 되었을 경우 등을 상정하여, 델리게이트 초기화. 
    public void RemoveAllListeners()
    {
        _onValueChanged = null;
    }

    public void Notify()
    {
        _onValueChanged?.Invoke(_value);
    }
}
