using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMagicAutoAttack : AutoAttack
{
    private GameObject lastEnemy;

    public override void Level1(GameObject enemy)
    {
        enemy.GetComponent<IEnemy>().GetHurt((int)PlayerStatsController.Stats.attack);
        DestroyProyectile();
    }
    public override void Level2(GameObject enemy)
    {
        IEnemy enemyController = enemy.GetComponent<IEnemy>();
        if (magicBase.GetAttackCount() == 2)
        {
            magicBase.SetAttackCount(0);
            enemyController.SetStatus(StatusEnum.Frost);
            float previousSpeed = enemyController.GetSpeed();
            enemyController.UpdateSpeed((100-.3f));
            StartCoroutine(ResetEnemySpeed(enemyController, 0.5f, previousSpeed));
        }
        else magicBase.SetAttackCount();
        Level1(enemy);

    }
    public override void Level3(GameObject enemy)
    {
        HasImpacted = false;
        IEnemy enemyController = enemy.GetComponent<IEnemy>();
        if (magicBase.GetAttackCount() == 2 && enemy != lastEnemy && enemyController.GetStatus() != StatusEnum.Frost)
        {
            magicBase.SetAttackCount(0);
            enemyController.SetStatus(StatusEnum.Frost);
            float previousSpeed = enemyController.GetSpeed();
            enemyController.UpdateSpeed((1.00f - 0.30f));
            StartCoroutine(ResetEnemySpeed(enemyController, 1.0f, previousSpeed));
        }
        else magicBase.SetAttackCount();
        enemy.GetComponent<IEnemy>().GetHurt((int)PlayerStatsController.Stats.attack);
        lastEnemy = enemy;
    }
    public override void Level4(GameObject enemy)
    {
        HasImpacted = false;
        IEnemy enemyController = enemy.GetComponent<IEnemy>();
        if (magicBase.GetAttackCount() == 2 && enemy != lastEnemy && enemyController.GetStatus() != StatusEnum.Frost)
        {
            magicBase.SetAttackCount(0);
            enemyController.SetStatus(StatusEnum.Frost);
            float previousSpeed = enemyController.GetSpeed();
            enemyController.UpdateSpeed((1.00f - 0.50f));
            StartCoroutine(ResetEnemySpeed(enemyController, 2.0f, previousSpeed));
        }
        else magicBase.SetAttackCount();
        enemy.GetComponent<IEnemy>().GetHurt((int)PlayerStatsController.Stats.attack);
        lastEnemy = enemy;
    }
    public override void Level5(GameObject enemy)
    {
        HasImpacted = false;
        IEnemy enemyController = enemy.GetComponent<IEnemy>();
        if (enemy == lastEnemy || enemyController.GetStatus() == StatusEnum.Frost) return;
        enemyController.SetStatus(StatusEnum.Frost);
        float previousSpeed = enemyController.GetSpeed();
        enemyController.UpdateSpeed((1.00f - 0.50f));
        StartCoroutine(ResetEnemySpeed(enemyController, 2.0f, previousSpeed));
        enemy.GetComponent<IEnemy>().GetHurt((int)PlayerStatsController.Stats.attack);
        lastEnemy = enemy;
    }

    private IEnumerator ResetEnemySpeed(IEnemy enemyController, float duration, float value)
    {
        yield return new WaitForSeconds(duration);
        enemyController.UpdateSpeed((int)value);
        enemyController.SetStatus(StatusEnum.None);
    }
}
