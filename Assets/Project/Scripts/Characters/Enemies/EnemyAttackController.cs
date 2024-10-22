using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAttackController : MonoBehaviour
{
    private IEnemy enemyStats;
    private EventManager eventManager;
    private bool IsColliding;

    private void Start()
    {
        enemyStats = GetComponent<IEnemy>();
        StartCoroutine(KeepAttacking());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collide: {other.name}");
        if (other.CompareTag("Player"))
        {
            IsColliding = true;
            EventManager.Events.OnEnemyAttackEvent(enemyStats.GetAttack());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsColliding = false;
        }
    }

    IEnumerator KeepAttacking()
    {
        while (true)
        {
            if (IsColliding)
            {
                yield return new WaitForSeconds(.5f);
                EventManager.Events.OnEnemyAttackEvent(enemyStats.GetAttack());
            }
            yield return null;
        }
    }
}
