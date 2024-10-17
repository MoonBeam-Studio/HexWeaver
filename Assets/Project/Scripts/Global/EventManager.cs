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


    //Event Invoking
    public void OnChangeControlSchemeEvent(string ControlScheme)
    {
        OnChangeControlScheme?.Invoke(ControlScheme);
    }

    public void OnToggleObjectiveEvent(bool pointMouse)
    {
        OnToggleObjective?.Invoke(pointMouse);
    }
}
