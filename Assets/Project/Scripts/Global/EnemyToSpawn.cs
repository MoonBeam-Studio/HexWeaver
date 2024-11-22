using UnityEngine;

[System.Serializable]
public class EnemyToSpawn
{
    [Tooltip("Prefab/model of the enemy to spawn")]
    public GameObject enemyPrefab;
    [Tooltip("Enemies to spawn at the same time"), Range(1, 10)]
    public int spawnRate;
    [Tooltip("Seconds to wait to spawn a new wave of this enemy")]
    public float spawnDelay;
    [Tooltip("Minutes and seconds the game must be in in order to start to spawn this enemy")]
    public MinutesSeconds timeToSpawn = new();
}