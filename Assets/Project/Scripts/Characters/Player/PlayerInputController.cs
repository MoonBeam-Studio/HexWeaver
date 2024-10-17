using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] TargetController targetController;

    private PlayerInput playerInput;
    private string previousControlScheme = null;
    private bool IsPointerToMouse = true;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    //private void OnEnable()
    //{
    //    inputActions.Default.Movement.performed += OnMove();
    //    inputActions.Default.Movement.canceled += OnMove();

    //}

    //private void OnDisable()
    //{
    //    inputActions.Default.Movement.performed -= OnMove();
    //    inputActions.Default.Movement.canceled -= OnMove();
    //}

    public void OnMovement(InputValue value)
    {
        gameObject.GetComponent<PlayerMovementController>().SetMovementInput(value.Get<Vector2>());
    }

    public void OnMoveObjetive(InputValue value)
    {
        
        targetController = GameObject.Find("Target").GetComponent<TargetController>();

        targetController.GetTargetMovement(value.Get<Vector2>());
    }
    private void OnControlsChanged()
    {
        if (playerInput.currentControlScheme != previousControlScheme)
        { 
            EventManager.Events.OnChangeControlSchemeEvent(playerInput.currentControlScheme);
            previousControlScheme = playerInput.currentControlScheme;
        }
    }

    private void OnToggleObjective()
    {
        IsPointerToMouse = !IsPointerToMouse;
        EventManager.Events.OnToggleObjectiveEvent(IsPointerToMouse);
    }
    
}
