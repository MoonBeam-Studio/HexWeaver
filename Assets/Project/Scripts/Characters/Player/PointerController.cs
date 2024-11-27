using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

public class PointerController : MonoBehaviour
{
    [SerializeField] private bool PointCursor = true;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private Vector3 cursorPostion;
    [SerializeField] private GameObject cursor; 
    [SerializeField] private GameObject groundCursor; //
    [SerializeField] private Transform targetPosition;
    [SerializeField] private LayerMask floorLayer; //
    [SerializeField] private Transform Player;
    [SerializeField] private bool resetPointer;
    [SerializeField] private bool IsStrafe;

    bool stopGame;

    private void Start()
    {
        cursor = GameObject.FindFirstObjectByType<TargetController>().gameObject;
        Player = GameObject.FindFirstObjectByType<PlayerInputController>().transform;
        targetPosition = GameObject.Find("TargetPosition").transform;
    }

    private void OnEnable()
    {
        eventManager = GameObject.FindFirstObjectByType<EventManager>();
        eventManager.OnToggleObjective += SetPointCursor;
        eventManager.OnToggleObjective += ResetPoint;

        eventManager.OnHoldStrafe += Strafe;
    }

    private void OnDisable()
    {
        eventManager.OnToggleObjective -= SetPointCursor;        
        eventManager.OnToggleObjective -= ResetPoint;

        eventManager.OnHoldStrafe -= Strafe;
    }

    private void Update()
    {
        if (stopGame) return;

        FollowPlayer();
        SetTargetGround();
        if (PointCursor) PointToCursor();
        else PointToFoward();
    }
    void OnStopGame(bool stop) => stopGame = stop;

    private void SetPointCursor(bool value)
    {
        PointCursor = value;
    }

    private void SetTargetGround()
    {
        cursorPostion = Camera.main.ScreenToWorldPoint(cursor.transform.position);
        targetPosition.position = cursorPostion;

        Ray ray = new Ray(cursorPostion, targetPosition.forward);
        RaycastHit hit;
        if (Physics.Raycast(cursorPostion, targetPosition.forward, out hit, 45, floorLayer))
        {
            groundCursor.transform.position = hit.point;
        }
    }

    private void PointToCursor()
    {
        transform.SetParent(null);
        resetPointer = true;
        transform.LookAt(groundCursor.transform.position);
        float ZRotation = -transform.rotation.eulerAngles.y;
        //Debug.Log(transform.rotation.ToString());
        transform.rotation = Quaternion.Euler(90, 0, ZRotation);
    }

    private void PointToFoward()
    {
        transform.SetParent(null);
        if (IsStrafe) return;
        transform.rotation = Quaternion.Euler(90, 0, -Player.rotation.eulerAngles.y);
        if (resetPointer)
        {
            resetPointer = false;
        }
    }

    private void FollowPlayer()
    {
        transform.position = Player.position;
    }

    private void ResetPoint(bool NaN)
    {
        transform.rotation = Quaternion.Euler(90,0,0);
    }

    private void Strafe(bool value)
    {
        IsStrafe = value;
    }
}
