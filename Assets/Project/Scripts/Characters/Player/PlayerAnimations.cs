using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animatorController;
    private EventManager eventManager;
    private bool isRunning;

    private void Start()
    {
        animatorController = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        eventManager = FindAnyObjectByType<EventManager>();
        eventManager.OnRunningAnimation += RunningAnimation;
        eventManager.OnAttackAnimation += AttackAnimation;
        eventManager.OnDieAnimation += DieAnimation;
    }

    private void OnDisable()
    {
        eventManager.OnRunningAnimation -= RunningAnimation;
        eventManager.OnAttackAnimation -= AttackAnimation;
        eventManager.OnDieAnimation -= DieAnimation;
    }

    private void RunningAnimation()
    {
        isRunning = !isRunning;
        animatorController.SetBool("IsRunning", isRunning);
    }

    private void AttackAnimation()
    {
        animatorController.SetTrigger("Attack");
    }

    private void DieAnimation()
    {
        animatorController.SetTrigger("Die");
    }
}
