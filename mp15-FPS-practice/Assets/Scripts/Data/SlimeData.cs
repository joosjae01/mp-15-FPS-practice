using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeData : MonsterData
{
    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public float AttackRange { get; set; }
    [field: SerializeField] public float AttackCoolDown { get; set; }
}
