using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SkeletonStatsController : EnemyStatsManager, IEnemy
{

    public StatusEnum status = StatusEnum.None;

    public int GetAttack()
    {
        return attack;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void GetHurt(int damage)
    {
        Debug.Log($"{gameObject.name} got hurt: {damage}");
        if (health <= 0) Debug.LogWarning($"Enemy Already dead ({gameObject.name})");
        health = GetComponent<EnemyHealthController>().OnAttacked(damage,health);
    }

    public void UpdateAttack(int value) => attack = value;
    public void UpdateAttack(float percentage) => attack = (int)(attack*percentage);
    public void UpdateSpeed(int value) => speed = value;
    public void UpdateSpeed(float percentage) => speed = speed*percentage;


    public StatusEnum GetStatus() => status;

    public void SetStatus(StatusEnum newStatus)
    {
        status = newStatus;
    }
}
