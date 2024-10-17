using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private CharacterController characterController;
    private Vector2 movementInput;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float speed = PlayerStatsController.Stats.speed * Time.deltaTime;
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y);

        if(movement != new Vector3(0,0,0)) transform.rotation = Quaternion.LookRotation(movement);
        characterController.Move(movement * speed);
    }

    public void SetMovementInput(Vector2 input) => movementInput = input;
}
