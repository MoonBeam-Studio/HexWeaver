using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class IceMagicAbility1 : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Transform groundPointer;
    [SerializeField] GameObject VFX;
    [SerializeField][Tooltip("Percentage of the player attack this ability deals")]
    float baseDamage = .75f;
    [SerializeField] float effectRadius = 5f;
    [SerializeField] float debuffDuration = 2;
    [SerializeField] bool IsPassive = false;
    [SerializeField]private int maxCD;
    [SerializeField]
    LayerMask enemyLayer;
    public float currentCD { get; private set; }
        
    private int lvl;
    [SerializeField] private bool OnCooldown;

    void Start()
    {
        if (IsPassive) currentCD = 0;
    }

    public float GetMaxCD() => maxCD/PlayerStatsController.Stats.basicCooldown;

    public void Cast(int lvl)
    {
        if (OnCooldown) return;
        StartCoroutine(Cooldown());

        this.lvl = lvl;
        GameObject vfx = Instantiate(VFX,groundPointer.position, Quaternion.Euler(new Vector3(0,0,0)));
        StartCoroutine(DestroyVFX(vfx));
        if (lvl >= 4)
        {
            vfx.transform.localScale *= 2;
            effectRadius *= 2;
        }
        Collider[] enemies = Physics.OverlapSphere(groundPointer.position, effectRadius, enemyLayer);
        List<int> prevSpeeds = new List<int>();
        foreach (var enemy in enemies)
        {
            IEnemy enemyController = enemy.GetComponent<IEnemy>();
            enemyController.GetHurt((int)Mathf.Round(PlayerStatsController.Stats.attack * baseDamage));
            if (lvl == 1) continue;
            enemyController.SetStatus(StatusEnum.Frost);
            if (lvl < 3) continue;
            prevSpeeds.Add((int)enemyController.GetSpeed());
            enemyController.UpdateSpeed(.7f);
            if (lvl == 5) enemyController.UpdateSpeed(0);

        }
        if (lvl > 1) StartCoroutine(ResetEnemy(enemies, prevSpeeds.ToArray()));
    }

    private IEnumerator DestroyVFX(GameObject vfx)
    {
        yield return new WaitForSeconds(3);
        Destroy(vfx);
    }

    private IEnumerator ResetEnemy(Collider[] enemies, int[] prevSpeeds)
    {
        yield return new WaitForSeconds(debuffDuration);
        foreach (var enemy in enemies)
        {
            IEnemy enemyController = enemy.GetComponent<IEnemy>();
            enemyController.SetStatus(StatusEnum.None);
            if (prevSpeeds.Length >= 1) continue;
            enemyController.UpdateSpeed(prevSpeeds[Array.IndexOf(enemies, enemy)]);
        }
    }

    private IEnumerator Cooldown()
    {
        OnCooldown = true;
        currentCD = GetMaxCD();
        while (currentCD > 0f)
        {
            currentCD -= Time.deltaTime;
            if (currentCD < 0f) currentCD = 0f;
            yield return null;
        }
        OnCooldown = false;
    }
}
