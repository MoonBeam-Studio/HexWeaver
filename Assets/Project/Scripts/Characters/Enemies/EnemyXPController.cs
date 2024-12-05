using UnityEngine;

public class EnemyXPController : MonoBehaviour
{
    [SerializeField] GameObject xpOrb;
    public void SpawnXp()
    {
        Debug.Log("XP Spawned");
        Instantiate(xpOrb, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity, GameObject.Find("XP").transform);
    }
}
