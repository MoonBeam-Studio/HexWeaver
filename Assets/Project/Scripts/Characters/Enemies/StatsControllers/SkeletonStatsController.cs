using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SkeletonStatsController : EnemyStatsManager, IEnemy
{
    public int GetAttack()
    {
        return attack;
    }

}
