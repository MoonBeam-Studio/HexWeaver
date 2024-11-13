using UnityEngine;

public class IceMagicAbility2 : MonoBehaviour
{
    [Header("Settings")]
    public int lvl;
    [SerializeField] float explosionRadius = 5;
    [SerializeField] float damageScale = .75f;

    [Header("Enemy Detection Settings")]
    [SerializeField] LayerMask enemyLayer;

    private Transform enemyTransform;
    private StatusEnum enemyStatus;
    private EventManager EventManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventManager = FindFirstObjectByType<EventManager>();
        EventManager.OnEnemyDie += EnemyDied;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyDie -= EnemyDied; 
    }

    // Update is called once per frame
    void Update()
    {
        FindFirstObjectByType<IceMagicAutoAttack>().SetAbility2Lvl(lvl);
    }

    private void EnemyDied(Transform _enemyTransform, StatusEnum _enemyStatus)
    {
        Debug.Log("Enemy Died");
        enemyStatus = _enemyStatus;
        enemyTransform = _enemyTransform;

        switch (lvl)
        {
            case 3:
                Level3();
                break;
            case 4:
                Level4();
                break;
            case 5:
                Level5();
                break;
            default:
                break;
        }
    }

    private void Level1()
    {
        return;
    }
    private void Level2()
    {
        return;
    }
    private void Level3()
    {
        if (enemyStatus != StatusEnum.Frost) return;

        Collider[] hitEnemies = Physics.OverlapSphere(enemyTransform.position, explosionRadius, enemyLayer);
        Debug.Log("Explosion!!");
        Debug.Log($"Affected enemies: {hitEnemies.Length}");
        if (hitEnemies.Length <= 0) return;
        foreach (var enemy in hitEnemies)
        {
            IEnemy enemyController = enemy.GetComponent<IEnemy>();
            enemyController.GetHurt(CalculateDamage(false));
        }
    }
    private void Level4()
    {
        if (enemyStatus != StatusEnum.Frost) return;

        Collider[] hitEnemies = Physics.OverlapSphere(enemyTransform.position, explosionRadius, enemyLayer);
        Debug.Log("Explosion!!");
        Debug.Log($"Affected enemies: {hitEnemies.Length}");
        if (hitEnemies.Length <= 0) return;
        foreach (var enemy in hitEnemies)
        {
            IEnemy enemyController = enemy.GetComponent<IEnemy>();
            enemyController.GetHurt(CalculateDamage(true));
        }
    }
    private void Level5()
    {
        if (enemyStatus != StatusEnum.Frost) return;

        Collider[] hitEnemies = Physics.OverlapSphere(enemyTransform.position, explosionRadius, enemyLayer);
        Debug.Log("Explosion!!");
        Debug.Log($"Affected enemies: {hitEnemies.Length}");
        if (hitEnemies.Length <= 0) return;
        foreach (var enemy in hitEnemies)
        {
            IEnemy enemyController = enemy.GetComponent<IEnemy>();
            enemyController.GetHurt(CalculateDamage(true));
            enemyController.SetStatus(StatusEnum.Frost);
        }
    }

    private int CalculateDamage(bool CanCrit)
    {
        float damage = PlayerStatsController.Stats.attack;
        damage = damage * damageScale;
        Debug.Log(CanCrit);
        if (CanCrit)
        {
            int n = Random.Range(0, 100);
            if (n <= PlayerStatsController.Stats.critRate) damage *= PlayerStatsController.Stats.critDamage;
        }

        return (int)Mathf.Round(damage);
    }
}
