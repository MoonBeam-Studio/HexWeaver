using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyMovementController : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    
    void Start()
    {
        player = GameObject.FindAnyObjectByType<PlayerInput>().transform;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(GoToPlayer());
    }

    private IEnumerator GoToPlayer()
    {
        while (true)
        {
            agent.SetDestination(player.position);
            yield return new WaitForSeconds(.1f);
        }
    }
}
