using System.Collections;
using System.Collections.Generic;
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

    private IEnumerator AutoAttack()
    {
        while (PlayerStatsController.Stats.currrentHealth > 0)
        {
            yield return new WaitForSeconds(attackRate);
            Attack();
        }
    }

    public void ActivateAutoAttack() => StartCoroutine(AutoAttack());
    public void StopAutoAttack() => StopCoroutine(AutoAttack());

}
