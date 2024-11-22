using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public static PlayerStatsController Stats { get; private set; }
    private void Awake() => Stats = this;

    public int speed;
    public int size;
    public float attack, attackSpeed;
    public int critRate;
    [Range(1, 2)] public float critDamage;
    [Range(.5f, 1)] public float basicCooldown, ultimateCooldown;
    public float pickUpRange;
    public int maxHealth, currrentHealth;
    public float healthRegen;
    public float _EXPGain;
    public float dodgeProb;

    [SerializeField] PlayerStats playerBaseStats;

    private int savedSpeed;

    private void Start()
    {
        speed = playerBaseStats.Speed;
        size = playerBaseStats.Size;
        attack = playerBaseStats.Attack;
        attackSpeed = playerBaseStats.AttackSpeed;
        critRate = playerBaseStats.CritRate;
        critDamage = playerBaseStats.CritDamage;
        basicCooldown = playerBaseStats.BasicCooldown;
        ultimateCooldown = playerBaseStats.UltimateCooldown;
        pickUpRange = playerBaseStats.PickUpRange;
        maxHealth = playerBaseStats.MaxHealth;
        currrentHealth = playerBaseStats.MaxHealth;
        healthRegen = playerBaseStats.HealthRegen;
        _EXPGain = playerBaseStats.EXPGain;
        dodgeProb = playerBaseStats.DodgeProb;

        savedSpeed = speed;
    }

    public void DisablePlayerMove()
    {
        savedSpeed = speed;
        speed = 0;
    }

    public void AllowPlayerMove()
    {
        speed = savedSpeed;
    }

}
