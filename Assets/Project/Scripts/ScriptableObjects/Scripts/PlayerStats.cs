using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/Player", order = 0)]
public class PlayerStats : ScriptableObject
{
    public float Speed = 5;
    public int Size = 1;
    public float Attack = 5, AttackSpeed = .5f;
    public int CritRate = 25;
    [Range(1,2)]public float CritDamage = 1.25f;
    public float BasicCooldown = 1, UltimateCooldown = 1;
    public float PickUpRange = 3;
    public int MaxHealth = 70;
    public float HealthRegen = 0;
    public float EXPGain = 1;
    public float DodgeProb = 0;
}
