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
        }
        return currentHealth;
    }

    public void StartDieIE() => StartCoroutine(Die());

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
