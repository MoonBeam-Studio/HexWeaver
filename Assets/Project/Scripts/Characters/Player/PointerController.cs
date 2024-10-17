using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    [SerializeField] private bool PointCursor = true;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private Vector3 cursorPostion;
    [SerializeField] private GameObject cursor; 
    [SerializeField] private GameObject _showSphere; //
    [SerializeField] private Transform targetPosition;
    [SerializeField] private LayerMask floorLayer; //
    [SerializeField] private Transform Player;
    [SerializeField] private bool resetPointer;

    private void Start()
    {
        cursor = GameObject.FindObjectOfType<TargetController>().gameObject;
        Player = GameObject.FindAnyObjectByType<PlayerInputController>().transform;
        targetPosition = GameObject.Find("TargetPosition").transform;
    }

    private void OnEnable()
    {
        eventManager = GameObject.FindObjectOfType<EventManager>();
        eventManager.OnToggleObjective += SetPointCursor;
        eventManager.OnToggleObjective += ResetPoint;
    }

    private void OnDisable()
    {
        eventManager.OnToggleObjective -= SetPointCursor;        
        eventManager.OnToggleObjective -= ResetPoint;        
    }

    private void Update()
    {
        SetTargetGround();
        if (PointCursor) PointToCursor();
        else
        {
            
            PointToFoward();
        } 
    }

    private void SetPointCursor(bool value)
    {
        PointCursor = value;
    }

    private void SetTargetGround()
    {
        cursorPostion = Camera.main.ScreenToWorldPoint(cursor.transform.position);
        targetPosition.position = cursorPostion;
        FollowPlayer();

        //_showSphere.transform.position = new Vector3(cursorPostion.x, 0, cursorPostion.z+20);
        Ray ray = new Ray(cursorPostion, targetPosition.forward);
        RaycastHit hit;
        if (Physics.Raycast(cursorPostion, targetPosition.forward, out hit, 45, floorLayer))
        {
            _showSphere.transform.position = hit.point;
        }
    }

    private void PointToCursor()
    {
        transform.SetParent(null);
        resetPointer = true;
        transform.LookAt(_showSphere.transform.position);
        float ZRotation = -transform.rotation.eulerAngles.y;
        //Debug.Log(transform.rotation.ToString());
        transform.rotation = Quaternion.Euler(90, 0, ZRotation);
    }

    private void PointToFoward()
    {
        transform.SetParent(null);
        FollowPlayer();
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
}