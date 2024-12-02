using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class IceMagicBase : MonoBehaviour, IMagicBase
{
    public string Name() => "IceMagic";
    EventManager events;
    Transform pointer;
    [Header("Levels")]
    [SerializeField] GameObject proyectile;
    public int AttackLvl = 1, Ability1LvL = 0, Ability2LvL = 0, UltimateLvL = 0;
    [Header("Other Data")]
    public int attackCount;
    private float attackRate;
    private bool AbilitiesActivated = true;
    private IceMagicAbility1 ability1;
    private IceMagicAbility2 ability2;
    private IceMagicUltimate ultimate;
    private float ability1CD, ability2CD, ultimateCD;
    private float ability1MaxCD, ability2MaxCD, ultimateMaxCD;


    private void Start()
    {
        pointer = GameObject.Find("PointerPos").transform;
        StartCoroutine(AutoAttack());

        ability1 = GetComponent<IceMagicAbility1>();
        ability2 = GetComponent<IceMagicAbility2>();
        ultimate = GetComponent<IceMagicUltimate>();
    }

    private void Update()
    {
        attackRate = 1/PlayerStatsController.Stats.attackSpeed;
        FindFirstObjectByType<IceMagicAbility2>().lvl = Ability2LvL;

        ability1CD = ability1.currentCD;
        ability2CD = ability2.currentCD;
        ultimateCD = ultimate.currentCD;
        ability1MaxCD = ability1.GetMaxCD();
        ability2MaxCD = ability2.GetMaxCD();
        ultimateMaxCD = ultimate.GetMaxCD();
    }

    public float[] GetCDs()
    {
        float[] cds = { ability1CD, ability2CD, ultimateCD };
        return cds;
    }
    public float[] GetMaxCDs()
    {
        float[] cds = { ability1MaxCD, ability2MaxCD, ultimateMaxCD };
        return cds;
    }

    public int[] GetLevels()
    {
        int[] levels = {AttackLvl, Ability1LvL, Ability2LvL, UltimateLvL };
        return levels;
    }

    public void SetAttackCount() => attackCount++;
    public void SetAttackCount(int value) => attackCount = value;
    public int GetAttackCount() => attackCount;

    private void Attack()
    {
        Vector3 proyectileRotation = pointer.rotation.eulerAngles;
        proyectileRotation = new Vector3(0,proyectileRotation.y,0);
        GameObject _proyectile = Instantiate(proyectile, pointer.position, Quaternion.Euler(proyectileRotation), null);
        _proyectile.GetComponent<IceMagicAutoAttack>().Level = AttackLvl;
    }

    public void Ability1()
    {
        if (Ability1LvL == 0 || !AbilitiesActivated) return;
        ability1.Cast(Ability1LvL);
        EventManager.Events.OnAttackAnimationEvent();
    }
    public void Ability2()
    {
        if (Ability2LvL == 0) return;
    }
    public void Ultimate()
    {
        if (UltimateLvL == 0 || !AbilitiesActivated) return;
        ultimate.Cast(UltimateLvL);
        EventManager.Events.OnAttackAnimationEvent();
    }

    private IEnumerator AutoAttack()
    {
        while (PlayerStatsController.Stats.currrentHealth > 0)
        {
            yield return new WaitForSeconds(attackRate);
            Attack();
            //EventManager.Events.OnAttackAnimationEvent();
            EventManager.Events.OnAttackEvent();
        }
    }

    public void ActivateAutoAttack() => StartCoroutine(AutoAttack());
    public void StopAutoAttack() => StopCoroutine(AutoAttack());
    public void SetAbilitiesEnabled(bool enabled)
    {
        AbilitiesActivated = enabled;
    }
    
    public void UnlockAbility1() => Ability1LvL = 1;
    public void UnlockAbility2() => Ability2LvL = 1;
    public void UnlockUltimate() => UltimateLvL = 1;

    public List<int> GetLvls()
    {
        List<int> lvls = new();
        lvls.Add(AttackLvl);
        lvls.Add(Ability1LvL);
        lvls.Add(Ability2LvL);
        lvls.Add(UltimateLvL);
        return lvls;
    }

   public void LevelUp(int id)
    {
        switch (id)
        {
            case 0: AttackLvl++; break;
            case 1: Ability1LvL++; break;
            case 2: Ability2LvL++; break;
            case 3: UltimateLvL++; break;
            default: break;
        }
    }
}
