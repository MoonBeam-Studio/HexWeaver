using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class IceMagicBase : MonoBehaviour,IMagicBase
{
    EventManager events;
    Transform pointer;
    [Header("Levels")]
    [SerializeField] GameObject proyectile;
    public int AttackLvl = 1, Ability1LvL = 0, Ability2LvL = 0, UltimateLvL = 0;
    [Header("Other Data")]
    public int attackCount;
    private float attackRate;
    private bool AbilitiesActivated = true;

    private void Start()
    {
        pointer = GameObject.Find("PointerPos").transform;
        StartCoroutine(AutoAttack());
    }

    private void Update()
    {
        attackRate = 1/PlayerStatsController.Stats.attackSpeed;
        FindFirstObjectByType<IceMagicAbility2>().lvl = Ability2LvL;
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
        GetComponent<IceMagicAbility1>().Cast(Ability1LvL);
        EventManager.Events.OnAttackAnimationEvent();
        Debug.Log("Abilty 1");
    }
    public void Ability2()
    {
        if (Ability2LvL == 0) return;
        Debug.Log("Ability2 is passive only");
    }
    public void Ultimate()
    {
        if (UltimateLvL == 0 || !AbilitiesActivated) return;
        GetComponent<IceMagicUltimate>().Cast(UltimateLvL);
        EventManager.Events.OnAttackAnimationEvent();
        Debug.Log("Ultimate");
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

}
