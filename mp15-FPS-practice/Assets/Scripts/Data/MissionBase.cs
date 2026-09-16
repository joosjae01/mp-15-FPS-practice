using System;

public abstract class MissionBase
{
    private Action<int> _onProgressUpdated;
    public int Id;
    public string Name;
    public string Descryption;
    public MissionType Type;
    private int _currentProgress;
    public int CurrentProgress
    {
        get => _currentProgress;
        set
        {
            _currentProgress = value;
            Notify();
        }
    }
    public int TargetProgress;
    public bool IsClear;

    public MissionBase(int id, string name, string desc, int targetProgress)
    {
        Id = id;
        Name = name;
        Descryption = desc;
        TargetProgress = targetProgress;
        _currentProgress = 0;
        IsClear = false;
    }

    public void RefreshProgress()
    {
        CurrentProgress++;
        if(TargetProgress == CurrentProgress)
        {
            IsClear = true;
        }
    }

    public void AddListener(Action<int> listener)
    {
        _onProgressUpdated += listener;
    }

    public void RemoveListener(Action<int> listener)
    {
        _onProgressUpdated -= listener;
    }

    public void RemoveAllListener()
    {
        _onProgressUpdated = null;
    }

    public void Notify()
    {
        _onProgressUpdated?.Invoke(_currentProgress);
    }
}
