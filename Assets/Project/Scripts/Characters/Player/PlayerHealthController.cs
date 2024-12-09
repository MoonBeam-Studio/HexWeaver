using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    private PlayerStatsController playerStatsController;
    private EventManager eventManager;
    bool dead;

    private void Start()
    {
        playerStatsController = GetComponent<PlayerStatsController>();
    }

    private void OnEnable()
    {
        eventManager = FindAnyObjectByType<EventManager>();
        eventManager.OnEnemyAttack += HurtPlayer;
    }

    private void OnDisable()
    {
        eventManager.OnEnemyAttack -= HurtPlayer;
    }

    private void HurtPlayer(int damage)
    {
        if (!dead)
        {
            int dodge = (int)(Random.Range(0f, 1f) * 10);
            if (dodge < playerStatsController.dodgeProb) return;

            playerStatsController.currrentHealth -= damage;

            if (playerStatsController.currrentHealth <= 0f) PlayerDie();
        }
        
    }

    void PlayerDie()
    {
        dead = true;
        eventManager.OnEnemyAttack -= HurtPlayer;
        eventManager.OnDieAnimationEvent();
        PlayerMovementController playerMovement = GetComponent<PlayerMovementController>();
        playerMovement.enabled = false;
    }

    public void Heal(int healthValue)
    {
        playerStatsController.currrentHealth += healthValue;

        if(playerStatsController.currrentHealth > playerStatsController.maxHealth) playerStatsController.currrentHealth = playerStatsController.maxHealth;
    }
}
