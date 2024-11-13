using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    public float MinPlayerDistance = 5f;
    [SerializeField] GameObject EnemySpawner;
    [SerializeField] List<GameObject> Enemies = new List<GameObject>();
    [SerializeField] private bool CanSpawn; //
    [SerializeField] private Transform floor;//
    [SerializeField]Vector2 mapSize; //
    // Start is called before the first frame update
    void Start()
    {
        floor = GameObject.Find("Floor").transform;
        mapSize = new Vector2(floor.localScale.x, floor.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSpawn)
        {
            Vector3 spawnPos = SpawnPos();
            GameObject enemySpawner = Instantiate(EnemySpawner, spawnPos, Quaternion.identity);
            enemySpawner.GetComponent<EnemySpawner>().enemyToSpawn = Enemies[SelectEnemy()];
            CanSpawn = false;
        }
    }

    private int SelectEnemy()
    {
        return Random.Range(0, Enemies.Count); //Not desired way, prefer to select an enemy depending on the game state
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
}
