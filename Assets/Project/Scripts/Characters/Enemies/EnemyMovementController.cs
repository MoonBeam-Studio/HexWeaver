using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static EventManager;

public class EnemyMovementController : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    bool stopGame;
    private EventManager Events;

    void Start()
    {
        player = GameObject.FindFirstObjectByType<PlayerInput>().transform;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(GoToPlayer());
    }

    private void OnEnable()
    {
        Events = FindFirstObjectByType<EventManager>();
        Events.OnStopGame += OnStopGame;
    }

    private void OnDisable()
    {
        Events.OnStopGame -= OnStopGame;
    }

    void OnStopGame(bool stop) => stopGame = stop;

    private IEnumerator GoToPlayer()
    {
        while (true)
        {
            if (stopGame)
            {
                agent.SetDestination(transform.position);
            }
            agent.SetDestination(player.position);
            yield return new WaitForSeconds(.1f);
        }
    }
}
