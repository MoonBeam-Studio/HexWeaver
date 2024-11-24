using UnityEngine;

public class XPController : MonoBehaviour
{
    [SerializeField] int value;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        other.GetComponent<PlayerLvlController>().AddXP(value);
        Destroy(gameObject);
    }
}
