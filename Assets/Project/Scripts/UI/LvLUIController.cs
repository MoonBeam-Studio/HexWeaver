using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Lean.Transition;
using UnityEditor.PackageManager;
using TMPro;

public class LvLUIController : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private List<StatsUI> stats = new();
    [SerializeField] Transform[] statsUI;
    [SerializeField] Transform abilityUI;
    private PlayerLvlController playerLvlController;
    private EventManager Events;

    private void Start()
    {
        playerLvlController = GetComponent<PlayerLvlController>();
    }

    private void OnEnable()
    {
        Events = FindFirstObjectByType<EventManager>();
        Events.OnLevelUp += ShowUI;
    }

    private void OnDisable()
    {
       Events.OnLevelUp -= ShowUI;
    }

    public void AddStatToUI(StatsUI stat)
    {
        stats.Add(stat);
    }

    void ShowUI()
    {
        //EventManager.Events.OnStopGameEvent(true);
        GameObject.Find("MousePointer").transform.Find("Target").gameObject.SetActive(false);
        EventManager.Events.OnStopGameEvent(false);
        StartCoroutine(SetUIStat());
        UI.SetActive(true);
    }

    public void HideUI()
    {
        UI.transform.Find("[EndTransition]").GetComponent<LeanManualAnimation>().BeginTransitions();
        StartCoroutine(DisableUI());
    }

    private IEnumerator DisableUI()
    {
        yield return new WaitForSeconds(.4f);
        yield return null;
        EventManager.Events.OnStopGameEvent(false);
        UI.transform.Find("Background").GetComponent<CanvasGroup>().alpha = 0;
        UI.transform.Find("LevelUps").GetComponent<RectTransform>().localScale = Vector3.zero;
        UI.SetActive(false);
        GameObject.Find("MousePointer").transform.Find("Target").gameObject.SetActive(true);
    }

    IEnumerator SetUIStat()
    {
        yield return null;
        for (int i = 0; i < stats.Count; i++)
        {
            string description = $"{stats[i].Name}\n +{stats[i].value}";
            statsUI[i].Find("Description").GetComponent<TextMeshProUGUI>().text = description;
        }
    }
}
