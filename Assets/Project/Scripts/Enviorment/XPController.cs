using UnityEngine;

public class XPController : MonoBehaviour
{
    [SerializeField] int value;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        GameObject.FindAnyObjectByType<PlayerLvlController>().AddXP(value);
        Destroy(gameObject);
    }
}
