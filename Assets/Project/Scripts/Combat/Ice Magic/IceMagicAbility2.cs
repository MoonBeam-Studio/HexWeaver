using System.Collections;
using UnityEngine;

public class IceMagicAbility2 : MonoBehaviour
{
    [Header("Settings")]
    public int lvl;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float damageScale = .75f;
    [SerializeField] float explosionRadius = 5;
    [SerializeField] bool IsPassive = false;
    [SerializeField] int maxCD = 0;
    [SerializeField] LayerMask enemyLayer;

    private Transform enemyTransform;
    private StatusEnum enemyStatus;
    private EventManager EventManager;
    public float currentCD { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(IsPassive) currentCD = 0;
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

    public float GetMaxCD() => maxCD/PlayerStatsController.Stats.basicCooldown;

    private void EnemyDied(Transform _enemyTransform, StatusEnum _enemyStatus)
    {
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

        GameObject vfx = Instantiate(explosionVFX, enemyTransform.position, Quaternion.Euler(Vector3.zero));
        StartCoroutine(destroyVFX(vfx));
        Collider[] hitEnemies = Physics.OverlapSphere(enemyTransform.position, explosionRadius, enemyLayer);
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

        GameObject vfx = Instantiate(explosionVFX, enemyTransform.position, Quaternion.Euler(Vector3.zero));
        StartCoroutine(destroyVFX(vfx));
        Collider[] hitEnemies = Physics.OverlapSphere(enemyTransform.position, explosionRadius, enemyLayer);
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

        GameObject vfx = Instantiate(explosionVFX, enemyTransform.position, Quaternion.Euler(Vector3.zero));
        StartCoroutine(destroyVFX(vfx));
        Collider[] hitEnemies = Physics.OverlapSphere(enemyTransform.position, explosionRadius, enemyLayer);
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
        if (CanCrit)
        {
            int n = Random.Range(0, 100);
            if (n <= PlayerStatsController.Stats.critRate) damage *= PlayerStatsController.Stats.critDamage;
        }

        return (int)Mathf.Round(damage);
    }

    private IEnumerator destroyVFX(GameObject vfx)
    {
        yield return new WaitForSeconds(4);
        Destroy(vfx);
    }


}
