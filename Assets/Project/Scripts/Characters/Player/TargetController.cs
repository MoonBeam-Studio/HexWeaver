using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetController : MonoBehaviour
{
    public bool IsGamepad;
    [Range(1,10)]public float Sensibility = 3.5f;
    [SerializeField] private EventManager eventManager;
    private Vector3 mousePosition;
    private Vector2 targetMovement;

    private void OnEnable()
    {
        eventManager = GameObject.FindFirstObjectByType<EventManager>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(.5f,0);
        eventManager.OnChangeControlScheme += ControlSchemeChanged;
        //Cursor.visible = false;
    }

    private void OnDisable()
    {
        eventManager.OnChangeControlScheme -= ControlSchemeChanged;
    }
    private void Update()
    {
        if (!IsGamepad) GoToMouse();
        else ControlTarget();
    }

    private void ControlSchemeChanged(string controlScheme)
    {
        if (controlScheme == "Gamepad")
        {
            IsGamepad = true;
            Vector3 halfScreenPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            transform.position = halfScreenPos;
        }
        else IsGamepad = false;
    }

    public void GetTargetMovement(Vector2 position)
    {
        if (IsGamepad) targetMovement = position;

        mousePosition = position;
    }

    private void GoToMouse()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Plane planeXZ = new Plane(Vector3.up, new Vector3(0, 5, 0));
        //Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        transform.position = mousePosition;
    }

    private void ControlTarget()
    {
        float speed = Sensibility * 100 * Time.deltaTime;
        Vector3 postion = new Vector3(targetMovement.x, targetMovement.y, 0) * speed ;
        transform.position += postion;
        postion.Set(0, 0, 0);
    }
}
