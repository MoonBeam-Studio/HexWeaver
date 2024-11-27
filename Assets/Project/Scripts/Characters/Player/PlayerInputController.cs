using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] TargetController targetController;

    private PlayerInput playerInput;
    private PlayerActionsInput inputActions;
    private string previousControlScheme = null;
    private bool IsPointerToMouse = true, IsMoving;
    private IMagicBase magic;

    private bool stopGame;
    private EventManager Events;

    private void Awake()
    {
        
        inputActions = new PlayerActionsInput();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        magic = GameObject.Find("AttacksAndAbilities").GetComponent<IMagicBase>();
    }

    private void OnEnable()
    {
        Events = FindFirstObjectByType<EventManager>();
        Events.OnStopGame += OnStopGame;
    }

    private void OnDisable()
    {
        Events.OnStopGame -= OnStopGame;        
    }

    void OnStopGame(bool set) => stopGame = set;

    public void OnAbilty_1(InputAction.CallbackContext value)
    {
        if (!value.performed) return;
        if (stopGame) return;
        magic.Ability1();
    }
    public void OnAbilty_2(InputAction.CallbackContext value)
    {
        if (!value.performed) return ;
        if (stopGame) return;
        magic.Ability2();
    }

    public void OnUltimate(InputAction.CallbackContext value)
    {
        if (!value.performed) return;
        if (stopGame) return;
        magic.Ultimate();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        if (stopGame) return;
        gameObject.GetComponent<PlayerMovementController>().SetMovementInput(value.ReadValue<Vector2>());

        if (value.performed && !IsMoving)
        {
            EventManager.Events.OnRunningAnimationEvent();
            IsMoving = true;
        }
        else if (value.canceled && IsMoving)
        {
            EventManager.Events.OnRunningAnimationEvent();
            IsMoving = false;
        }
    }

    public void OnMoveObjetive(InputAction.CallbackContext value)
    {
        if (stopGame) return;
        targetController = GameObject.Find("Target").GetComponent<TargetController>();

        targetController.GetTargetMovement(value.ReadValue<Vector2>());
    }
    public void OnControlsChanged()
    {
        if (playerInput.currentControlScheme != previousControlScheme)
        { 
            EventManager.Events.OnChangeControlSchemeEvent(playerInput.currentControlScheme);
            previousControlScheme = playerInput.currentControlScheme;
        }
    }

    public void OnToggleObjective()
    {
        if (stopGame) return;
        IsPointerToMouse = !IsPointerToMouse;
        EventManager.Events.OnToggleObjectiveEvent(IsPointerToMouse);
    }
    
    public void OnStrafePlayer(InputAction.CallbackContext context)
    {
        if (stopGame) return;
        if (context.performed) EventManager.Events.OnStrafeEvent(true);
        else EventManager.Events.OnStrafeEvent(false);
    }

}
