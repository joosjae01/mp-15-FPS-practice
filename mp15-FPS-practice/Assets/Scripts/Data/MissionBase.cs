public abstract class MissionBase
{
    public int Id;
    public string Name;
    public string Descryption;
    public MissionType Type;
    public int CurrentProgress;
    public int TargetProgress;
    public bool IsClear;

    public MissionBase(int id, string name, string desc, int targetProgress)
    {
        Id = id;
        Name = name;
        Descryption = desc;
        TargetProgress = targetProgress;
        CurrentProgress = 0;
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
}
