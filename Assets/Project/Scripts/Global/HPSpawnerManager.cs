using System.Collections;
using UnityEngine;

public class HPSpawnerManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private GameObject HPPrefab;
    [SerializeField]
    private float MinPlayerDistance;
    [SerializeField]
    private float TimeBetweenSpawns = 3f;
    [Header("Debugging")]
    [SerializeField]
    private Transform floor;//
    [SerializeField]
    private Vector2 mapSize; //
    [SerializeField]
    private bool CanSpawn = true;

    private GameObject HPFather;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        floor = GameObject.Find("Floor").transform;
        mapSize = new Vector2(floor.localScale.x, floor.localScale.z);
        HPFather = GameObject.Find("HP");
        StartCoroutine(SpawnHP());
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanSpawn) return;

    }

    private IEnumerator SpawnHP()
    {
        while (true)
        {
            Vector3 pos = new Vector3(
                x: Random.Range(-mapSize.x / 2, mapSize.x / 2),
                y: 0,
                z: Random.Range(-mapSize.y / 2, mapSize.y / 2)
                );
            Instantiate(HPPrefab, pos, Quaternion.identity, HPFather.transform);   
            yield return new WaitForSeconds(TimeBetweenSpawns);
        }
    }
}
