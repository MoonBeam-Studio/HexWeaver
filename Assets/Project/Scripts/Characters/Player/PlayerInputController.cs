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

    private void Awake()
    {
        
        inputActions = new PlayerActionsInput();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        magic = GameObject.Find("AttacksAndAbilities").GetComponent<IMagicBase>();
    }

    public void OnAbilty_1(InputAction.CallbackContext value)
    {
        if (!value.performed) return;
        magic.Ability1();
    }
    public void OnAbilty_2(InputAction.CallbackContext value)
    {
        if (!value.performed) return ;
        magic.Ability2();
    }

    public void OnUltimare(InputAction.CallbackContext value)
    {
        if (!value.performed) return;
        magic.Ultimate();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
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
        IsPointerToMouse = !IsPointerToMouse;
        EventManager.Events.OnToggleObjectiveEvent(IsPointerToMouse);
    }
    
    public void OnStrafePlayer(InputAction.CallbackContext context)
    {
        if (context.performed) EventManager.Events.OnStrafeEvent(true);
        else EventManager.Events.OnStrafeEvent(false);
    }

}
