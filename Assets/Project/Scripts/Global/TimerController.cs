using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public string minuteStr = "00", secondStr = "00";
    public int minute = 0, second = 0;

    bool stopGame;
    private EventManager Events;

    public static TimerController Timer;
    private void Awake() => Timer = this;

    private void Start() => StartCoroutine(TimerControl());

    private void OnEnable()
    {
        Events = FindFirstObjectByType<EventManager>();
        Events.OnStopGame += OnStopGame;
    }

    private void OnDisable()
    {
        Events.OnStopGame -= OnStopGame;
    }

    void OnStopGame(bool stop) => stopGame = stop;

    private IEnumerator TimerControl()
    {
        while (true)
        {
            if (stopGame) continue;
            yield return new WaitForSeconds(1);
            second++;
            if (second >59)
            {
                second = 0;
                minute++;
            }
            if (minute/10 < 1) minuteStr = "0"+minute;
            else minuteStr = minute.ToString();

            if (second/10 < 1) secondStr = "0"+second;
            else secondStr = second.ToString();
        }
    }
}
