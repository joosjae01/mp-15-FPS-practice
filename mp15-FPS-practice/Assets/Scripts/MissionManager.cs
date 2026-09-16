using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : SingletonBehaviour<MissionManager>
{
    public List<MissionBase> PlayerMissions = new List<MissionBase>();

    public void Init()
    {
        KillMonsterMission missionA = new KillMonsterMission(1, "터렛 파괴", "터렛을 3기 파괴한다", 3, MonsterType.Turret);
        KillMonsterMission missionB = new KillMonsterMission(2, "슬라임 처치", "슬라임을 5체 사냥한다", 5, MonsterType.Slime);

        AddMission(missionA);
        AddMission(missionB);
    }

    public void AddMission(MissionBase mission)
    {
        PlayerMissions.Add(mission);
    }
}
