using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class SpawnerEnemyManager : MonoBehaviour
{

    [Header("Enemies")]
    [SerializeField]
    private List<EnemyToSpawn> basicEnemies = new();
    [SerializeField]
    private List<EnemyToSpawn> miniBoss = new();
    [SerializeField]
    private List<EnemyToSpawn> mediumBoss = new();
    [SerializeField]
    private List<EnemyToSpawn> FinalBoss = new();

    [Header("Settings")]
    [SerializeField]
    private GameObject EnemySpawner;
    [SerializeField]
    private float MinPlayerDistance;

    [Header("Debugging")]
    [SerializeField]
    private Transform floor;//
    [SerializeField]
    private Vector2 mapSize; //
    [SerializeField]
    private bool CanSpawn = true;

    public int i = 0;

    void Start()
    {
        floor = GameObject.Find("Floor").transform;
        mapSize = new Vector2(floor.localScale.x, floor.localScale.z);

        SortEnemies();
        FinalBoss[0].timeToSpawn = GameController.gameController.GetFinalBossSpawnTime();
    }

    void Update()
    {
        if (!CanSpawn || i >= basicEnemies.Count) return;

        int timerMin = TimerController.Timer.minute;
        int timerSec = TimerController.Timer.second;

        if(timerMin == basicEnemies[i].timeToSpawn.Min && timerSec == basicEnemies[i].timeToSpawn.Sec)
        {
            StartCoroutine(SpawnEnemy(i));
            i++;
        }
    }

    private void SortEnemies()
    {
        basicEnemies = basicEnemies.OrderBy(o => o.timeToSpawn.Min).ThenBy(o => o.timeToSpawn.Sec).ToList();
        //miniBoss = miniBoss.OrderBy(o => o.timeToSpawn.Min).ThenBy(o => o.timeToSpawn.Sec).ToList();
        //mediumBoss = mediumBoss.OrderBy(o => o.timeToSpawn.Min).ThenBy(o => o.timeToSpawn.Sec).ToList();
    }

    public Vector3 SpawnPos()
    {
        Vector3 pos = new Vector3(
                x: Random.Range(-mapSize.x / 2, mapSize.x / 2),
                y: 0,
                z: Random.Range(-mapSize.y / 2, mapSize.y / 2)
                );
        return pos;
    }

    public void EndSpawn() => CanSpawn = true;

    public float GetMinPlayerDistance() => MinPlayerDistance;

    IEnumerator SpawnEnemy(int index)
    {
        while (true)
        {
            for (int n = 0; n <= Random.Range(1, basicEnemies[index].spawnRate); n++)
            {
                Vector3 spawnPos = SpawnPos();
                GameObject enemySpawner = Instantiate(EnemySpawner, spawnPos, Quaternion.identity);
                enemySpawner.GetComponent<EnemySpawner>().enemyToSpawn = basicEnemies[index].enemyPrefab;
            }
            yield return new WaitForSeconds(basicEnemies[index].spawnDelay);
        }
    }
}
