using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthController : MonoBehaviour
{
    public int OnAttacked(int damage, int currentHealth)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Collider>().enabled = false;
            EventManager.Events.OnEnemyDieEvent(transform, GetComponent<IEnemy>().GetStatus());
        }
        return currentHealth;
    }

    public void StartDieIE() => StartCoroutine(Die());

    private IEnumerator Die()
    {
        GetComponent<EnemyXPController>().SpawnXp();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
