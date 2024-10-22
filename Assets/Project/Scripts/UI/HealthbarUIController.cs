using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUIController : MonoBehaviour
{
    private Slider healthBar;
    private TextMeshProUGUI healthText;

    private void Start()
    {
        healthBar = GetComponent<Slider>();
        healthText = GetComponentInChildren<TextMeshProUGUI>();
        healthBar.minValue = 0;
    }

    private void Update()
    {
        healthBar.maxValue = PlayerStatsController.Stats.maxHealth;
        healthBar.value = PlayerStatsController.Stats.currrentHealth;
        healthText.text = $"{PlayerStatsController.Stats.currrentHealth} / {PlayerStatsController.Stats.maxHealth}";
    }
}
