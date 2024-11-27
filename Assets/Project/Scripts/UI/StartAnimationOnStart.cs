using Lean.Transition;
using UnityEngine;

public class StartAnimationOnStart : MonoBehaviour
{
    [SerializeField] LeanManualAnimation transition;
    void OnEnable()
    {
        transition.BeginTransitions();
    }

}
