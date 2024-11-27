using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static EventManager;

public class EnemyAttackController : MonoBehaviour
{
    private IEnemy enemyStats;
    private EventManager eventManager;
    private bool IsColliding;
    bool stopGame;

    private void Start()
    {
        enemyStats = GetComponent<IEnemy>();
        StartCoroutine(KeepAttacking());
    }

    private void OnEnable()
    {
        EventManager.Events.OnStopGame += OnStopGame;
    }

    private void OnDisable()
    {
        EventManager.Events.OnStopGame -= OnStopGame;
    }

    void OnStopGame(bool stop) => stopGame = stop;

    private void OnTriggerEnter(Collider other)
    {
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
            if (stopGame) continue;
            if (IsColliding)
            {
                yield return new WaitForSeconds(.5f);
                EventManager.Events.OnEnemyAttackEvent(enemyStats.GetAttack());
            }
            yield return null;
        }
    }
}
