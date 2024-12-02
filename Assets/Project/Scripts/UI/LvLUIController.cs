using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Lean.Transition;
using UnityEditor.PackageManager;
using TMPro;
using UnityEngine.UI;

public class LvLUIController : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private List<StatsUI> stats = new();
    [SerializeField] private AbilityCardUI ability;
    [SerializeField] Transform[] statsUI;
    [SerializeField] Transform abilityUI;
    private PlayerLvlController playerLvlController;
    private EventManager Events;
    private int level;
    private IMagicBase magicBase;

    private void Start()
    {
        playerLvlController = GetComponent<PlayerLvlController>();
        magicBase = GameObject.Find("AttacksAndAbilities").GetComponent<IMagicBase>();
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

    public void AddAbilityToUI(AbilityCardUI _ability)
    {
        ability = _ability;
    }

    /*
     * shark attack 🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈
     * 🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈
     * 🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈🦈
     */

    void ShowUI()
    {
        //EventManager.Events.OnStopGameEvent(true);
        GameObject.Find("MousePointer").transform.Find("Target").gameObject.SetActive(false);
        EventManager.Events.OnStopGameEvent(false);
        StartCoroutine(SetUIStat());
        StartCoroutine(SetAbilityUI());
        UI.SetActive(true);
    }

    public void HideUI()
    {
        UI.transform.Find("[EndTransition]").GetComponent<LeanManualAnimation>().BeginTransitions();
        stats.Clear();
        StartCoroutine(DisableUI());
    }

    private IEnumerator DisableUI()
    {
        yield return new WaitForSeconds(.4f);
        yield return null;
        EventManager.Events.OnStopGameEvent(false);
        UI.transform.Find("Background").GetComponent<CanvasGroup>().alpha = 0;
        UI.transform.Find("LevelUps").GetComponent<RectTransform>().localScale = Vector3.zero;
        abilityUI.gameObject.SetActive(true);
        UI.SetActive(false);
        GameObject.Find("MousePointer").transform.Find("Target").gameObject.SetActive(true);
    }

    IEnumerator SetUIStat()
    {
        yield return null;
        if (level == 3 || level == 5 || level == 7) abilityUI.gameObject.SetActive(false);
        Debug.LogWarning($"Stats: (1){stats[0].ID} (2){stats[1].ID} (3){stats[2].ID}");
        for (int i = 0; i < stats.Count; i++)
        {
            string description = $"{stats[i].Name}\n +{stats[i].value}";
            statsUI[i].Find("Description").GetComponent<TextMeshProUGUI>().text = description;
        }
    }

    IEnumerator SetAbilityUI()
    {
        yield return null;
        abilityUI.Find("Icon").GetComponent<Image>().sprite = ability.icon;
        abilityUI.Find("AbilityLVL").GetComponent<TextMeshProUGUI>().text = (ability.Lvl + 1).ToString();

    }

    public void SelectStat(int index)
    {
        Debug.Log($"Stat selected: {stats[index].ID}");
        switch (stats[index].ID)
        {
            case "Speed": PlayerStatsController.Stats.speed += stats[index].value;break;
            case "Attack": PlayerStatsController.Stats.attack += stats[index].value;break;
            case "AttackSpeed": PlayerStatsController.Stats.attackSpeed += stats[index].value;break;
            case "CritRate": PlayerStatsController.Stats.critRate += (int)stats[index].value;break;
            case "CritDamage": PlayerStatsController.Stats.critDamage += stats[index].value;break;
            case "BasicCooldown": PlayerStatsController.Stats.basicCooldown += stats[index].value;break;
            case "UltimateCooldown": PlayerStatsController.Stats.ultimateCooldown += stats[index].value;break;
            case "MaxHealth": PlayerStatsController.Stats.maxHealth += (int)stats[index].value;break;
            case "HealthRegen": PlayerStatsController.Stats.healthRegen += stats[index].value;break;
            case "EXPGain": PlayerStatsController.Stats._EXPGain += stats[index].value;break;
        }
        HideUI();
    }

    public void SelectAbility()
    {
        switch (ability.ID)
        {
            case "Attack": magicBase.LevelUp(0); break;
            case "Ability1": magicBase.LevelUp(1); break;
            case "Ability2": magicBase.LevelUp(2); break;
            case "Ultimate": magicBase.LevelUp(3); break;
            default: break;
        }
        HideUI();
    }

    public void SetLevel(int lvl) => level = lvl;

  
}
