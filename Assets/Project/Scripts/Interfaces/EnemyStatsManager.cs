using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatsManager : MonoBehaviour
{
    [SerializeField] EnemyStats baseStats;
    [SerializeField] int level = 1;

    private NavMeshAgent agent;

    public int maxHealth, health, attack;
    public float speed;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        maxHealth = baseStats.MaxHealth;
        attack = baseStats.Attack;
        if (level != 1) SetStats();
        health = maxHealth;
        speed = baseStats.Speed;
    }

    private void SetStats()
    {
        int levelBonus = Mathf.RoundToInt((float)(18.20 * Mathf.Log(level)));

        attack += levelBonus/5;
        maxHealth += levelBonus;
    }

    private void Update()
    {
        agent.speed = speed;
    }

}
