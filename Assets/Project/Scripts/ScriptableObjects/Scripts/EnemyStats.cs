using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/Enemy", order = 1)]
public class EnemyStats : ScriptableObject
{
    public int MaxHealth;
    public int Attack;
}
