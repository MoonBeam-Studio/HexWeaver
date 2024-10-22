using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUIController : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    private string timerText;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        timerText = $"{TimerController.Timer.minuteStr}:{TimerController.Timer.secondStr}";
        tmp.text = timerText ;
    }
}
