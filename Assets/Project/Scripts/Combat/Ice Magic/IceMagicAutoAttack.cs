using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class IceMagicAutoAttack : AutoAttack
{
    [SerializeField] GameObject additionalParticles;

    private GameObject lastEnemy;
    private int Ability2Lvl;

    public override void Level1(GameObject enemy)
    {
        IEnemy enemyController = enemy.GetComponent<IEnemy>();
        enemyController.GetHurt(CalculateDamage(enemyController));
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
            enemyController.UpdateSpeed((1f-.3f));
            StartCoroutine(ResetEnemySpeed(enemyController, 1f, previousSpeed));
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
        enemyController.GetHurt(CalculateDamage(enemyController));
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
        enemyController.GetHurt(CalculateDamage(enemyController));
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
        enemy.tag = "Frosted";
        StartCoroutine(ResetEnemySpeed(enemyController, 2.0f, previousSpeed));
        enemyController.GetHurt(CalculateDamage(enemyController));
        lastEnemy = enemy;
    }

    private IEnumerator ResetEnemySpeed(IEnemy enemyController, float duration, float value)
    {
        yield return new WaitForSeconds(duration);
        enemyController.UpdateSpeed((int)value);
        enemyController.SetStatus(StatusEnum.None);
    }

    private int CalculateDamage(IEnemy enemyController)
    {
        int TotalDamage = 0, damage = (int)PlayerStatsController.Stats.attack;
        float bonusDamage = 0f;
        int critRate = PlayerStatsController.Stats.critRate;
        if (enemyController.GetStatus() == StatusEnum.Frost)
        {
            switch (Ability2Lvl)
            {
                case 1:
                    bonusDamage = PlayerStatsController.Stats.attack * .25f;
                    break;
                case 2:
                    bonusDamage = PlayerStatsController.Stats.attack * .25f;
                    critRate += 25;
                    break;
                case 3:
                    bonusDamage = PlayerStatsController.Stats.attack * .25f;
                    critRate += 25;
                    break;
                case 4:
                    bonusDamage = PlayerStatsController.Stats.attack * .25f;
                    critRate += 25;
                    break;
                case 5:
                    bonusDamage = PlayerStatsController.Stats.attack * .25f;
                    critRate += 25;
                    break;
                default:
                    break;
            }
        }

        damage += (int)bonusDamage;

        int n = Random.Range(0, 100);
        if (n <= critRate) TotalDamage = (int)Mathf.Round(PlayerStatsController.Stats.critDamage * damage);
        else TotalDamage = damage;
        return TotalDamage;
    }

    public void SetAbility2Lvl(int lvl)
    {
        Ability2Lvl = lvl;
    }
}
