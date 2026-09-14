using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretData : MonsterData
{
    [SerializeField] public float TurretRange;
    [SerializeField] public float RotateSpeed;
    [SerializeField] public float Cooldown;
    [SerializeField] public ObjectPool BulletPool;
}
