using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : MonoBehaviour
{
    [SerializeField] EnemyStats baseStats;
    [SerializeField] int level = 1;
    public int maxHealth, health, attack;

    private void Start()
    {
        maxHealth = baseStats.MaxHealth;
        attack = baseStats.Attack;
        if (level != 1) SetStats();
        health = maxHealth;
    }

    private void SetStats()
    {
        int levelBonus = Mathf.RoundToInt((float)(18.20 * Mathf.Log(level)));

        attack += levelBonus/5;
        maxHealth += levelBonus;
    }

}
