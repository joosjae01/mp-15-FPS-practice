public class KillMonsterMission : MissionBase
{
    public MonsterType TargetMonster;
    public KillMonsterMission(int id, string name, string desc, int targetProgress, MonsterType targetMonster) : base(id, name, desc, targetProgress)
    {
        Type = MissionType.DestroyMonster;
        TargetMonster = targetMonster;
    }

    public void CheckProgress(IDamageable target)
    {
        if (IsClear) return;

        MonsterData _monsterData = target.GameObject.GetComponent<MonsterData>();
        if (_monsterData.MonsterType == TargetMonster)
        {
            RefreshProgress();
        }
    }
}
