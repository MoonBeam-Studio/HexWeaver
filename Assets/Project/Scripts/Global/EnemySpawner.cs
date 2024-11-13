using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    private EnemySpawnerManager spawnerManager;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerInputController>().transform;
        spawnerManager = FindAnyObjectByType<EnemySpawnerManager>();
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        yield return null;
        while (Vector3.Distance(player.position, transform.position) <= spawnerManager.MinPlayerDistance)
        {
            transform.position = spawnerManager.SpawnPos();
            yield return null;
        }
        transform.LookAt(player.position);
        yield return null;
        Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(.3f,2f));
        spawnerManager.EndSpawn();
        Destroy(gameObject);
    }
}
