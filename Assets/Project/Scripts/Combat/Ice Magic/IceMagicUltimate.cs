using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static EventManager;

public class IceMagicUltimate : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] GameObject iceCubePrefab;
    [SerializeField] GameObject iceAreaPrefab;
    [SerializeField] float baseDamagePercentage = 1, healing = 10;
    [SerializeField] int outerRadius = 20, innerRadius = 10;
    [SerializeField] float duration = 3f, slowDuration = 5f;
    [SerializeField] bool IsPassive = false;
    [SerializeField] private int maxCD;
    [SerializeField] LayerMask enemyLayer;
    public float currentCD { get; private set; }

    GameObject iceCube, iceArea;
    float activationTime;
    Transform player;
    [SerializeField] bool OnCooldown = false;

    void Start()
    {
        if (IsPassive) currentCD = 0;
    }

    private void Update()
    {
        player = FindAnyObjectByType<PlayerInputController>().transform;
    }

    public float GetMaxCD() => maxCD/PlayerStatsController.Stats.ultimateCooldown;

    public void Cast(int lvl)
    {
        if (OnCooldown) return;
        switch (lvl)
        {
            case 1: 
                Level1(); 
                break;
            case 2: 
                Level2(); 
                break;
            case 3: 
                Level3(); 
                break;
            case 4: 
                Level4(); 
                break;
            case 5: 
                Level5(); 
                break;

            default: break;
        }
    }

    private void Level1()
    {
        activationTime = Time.time;
        PlayerStatsController.Stats.DisablePlayerMove();
        FindFirstObjectByType<IceMagicBase>().SetAbilitiesEnabled(false);
        SetEffects(true);
        Collider[] enemies = Physics.OverlapSphere(player.position, outerRadius, enemyLayer);
        List<float> enemySpeed = new();
        foreach (var enemy in enemies)
        {
            IEnemy enemyController = enemy.GetComponent<IEnemy>();
            enemySpeed.Add(enemyController.GetSpeed());
            enemyController.UpdateSpeed((.60f));
            enemyController.SetStatus(StatusEnum.Frost);
        }
        StartCoroutine(EndUltimate(enemies, enemySpeed.ToArray()));
        StartCoroutine(EnemyDoT(enemies));
    }

    private void Level2()
    {
        Level1();
        StartCoroutine(PlayerHeal());
    }

    private void Level3()
    {
        activationTime = Time.time;
        PlayerStatsController.Stats.DisablePlayerMove();
        FindFirstObjectByType<IceMagicBase>().SetAbilitiesEnabled(false);
        SetEffects(true);
        Collider[] enemies = Physics.OverlapSphere(player.position, outerRadius, enemyLayer);
        List<float> enemySpeed = new();
        foreach (var enemy in enemies)
        {
            IEnemy enemyController = enemy.GetComponent<IEnemy>();
            enemySpeed.Add(enemyController.GetSpeed());
            enemyController.UpdateSpeed((.60f));
            enemyController.SetStatus(StatusEnum.Frost);
        }
        StartCoroutine(EndUltimate(enemies, enemySpeed.ToArray()));
        StartCoroutine(EnemyDoT(enemies));
        StartCoroutine(PlayerHeal());
    }

    private void Level4()
    {
        activationTime = Time.time;
        PlayerStatsController.Stats.DisablePlayerMove();
        SetEffects(true);
        Collider[] enemies = Physics.OverlapSphere(player.position, outerRadius, enemyLayer);
        List<float> enemySpeed = new();
        foreach (var enemy in enemies)
        {
            IEnemy enemyController = enemy.GetComponent<IEnemy>();
            enemySpeed.Add(enemyController.GetSpeed());
            enemyController.UpdateSpeed(0);
            enemyController.SetStatus(StatusEnum.Frost);
        }
        StartCoroutine(EndUltimate(enemies, enemySpeed.ToArray()));
        StartCoroutine(EnemyDoT(enemies));
        StartCoroutine(PlayerHeal());
    }

    private void Level5()
    {
        Level4();
        Collider[] enemies = Physics.OverlapSphere(player.position, innerRadius, enemyLayer);
        List<float> enemySpeed = new();
        foreach (var enemy in enemies)
        {
            IEnemy enemyController = enemy.GetComponent<IEnemy>();
            enemyController.GetHurt((int)Mathf.Round(PlayerStatsController.Stats.attack * (baseDamagePercentage * 3)));
        }
    }

    private void SetEffects(bool set)
    {
        if (set)
        {
            

            iceArea = Instantiate(iceAreaPrefab, player);
            iceCube = Instantiate(iceCubePrefab, player);
        }
        else
        {
            Destroy(iceCube);
            Destroy(iceArea);
        }
    }

    private IEnumerator EndUltimate(Collider[] enemies, float[] previousSpeed)
    {
        
        activationTime = Time.time;
        yield return new WaitForSeconds(duration);
        yield return null;
        StartCoroutine(Cooldown());

        SetEffects(false);
        FindFirstObjectByType<IceMagicBase>().SetAbilitiesEnabled(true);
        PlayerStatsController.Stats.AllowPlayerMove();

        yield return new WaitForSeconds(slowDuration - duration);
        for(int i = 0; i <= enemies.Length; i++)
        {
            IEnemy enemyController = enemies[i].GetComponent<IEnemy>();
            enemyController.UpdateSpeed(previousSpeed[i]);
        }

        Collider[] n = { };
        //StopCoroutine(EnemyDoT(n));
        //StopCoroutine(PlayerHeal());
    }

    private IEnumerator EnemyDoT(Collider[] enemies)
    {
        while (Time.time <= duration+activationTime)
        {
            foreach (var enemy in enemies)
            {
                IEnemy enemyController = enemy.GetComponent<IEnemy>();
                enemyController.GetHurt((int)Mathf.Round(PlayerStatsController.Stats.attack * baseDamagePercentage + .01f));
            }
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator PlayerHeal()
    {
        while (Time.time <= duration + activationTime)
        {
            yield return new WaitForSeconds(.5f);

            PlayerStatsController.Stats.currrentHealth += (int)healing;

            yield return new WaitForSeconds(.5f);
        }
    }

    private IEnumerator Cooldown()
    {
        OnCooldown = true;
        currentCD = maxCD;
        while (currentCD > 0f)
        {
            currentCD -= Time.deltaTime;
            yield return null;
        }
        OnCooldown = false;
    }
}
