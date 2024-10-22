using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Events { get; private set; }
    private void Awake() => Events = this;

    //Events
    public delegate void ChangeControlScheme(string ControlScheme);
    public event ChangeControlScheme OnChangeControlScheme;

    public delegate void ToggleObjective(bool PointMouse);
    public event ToggleObjective OnToggleObjective;

    public delegate void HoldStrafe(bool IsStrafe);
    public event HoldStrafe OnHoldStrafe;

    public delegate void Animations();
    public event Animations OnRunningAnimation;
    public event Animations OnAttackAnimation;
    public event Animations OnDieAnimation;

    public delegate void EnemyAttack(int damage);
    public event EnemyAttack OnEnemyAttack;

    //Event Invoking
    public void OnChangeControlSchemeEvent(string ControlScheme) => OnChangeControlScheme?.Invoke(ControlScheme);
    public void OnToggleObjectiveEvent(bool pointMouse) => OnToggleObjective?.Invoke(pointMouse);
    public void OnStrafeEvent(bool IsStrafe) => OnHoldStrafe?.Invoke(IsStrafe);
    public void OnRunningAnimationEvent() => OnRunningAnimation?.Invoke();
    public void OnAttackAnimationEvent() => OnAttackAnimation?.Invoke();
    public void OnDieAnimationEvent() => OnDieAnimation?.Invoke();
    public void OnEnemyAttackEvent(int damage) => OnEnemyAttack?.Invoke(damage);
}
