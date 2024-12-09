using UnityEngine;

public class HealthPackController : MonoBehaviour
{
    [SerializeField] int value = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameObject.FindAnyObjectByType<PlayerHealthController>().Heal(value);
        Destroy(gameObject);
    }
}
