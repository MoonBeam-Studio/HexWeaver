using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsUI
{
    public string ID;
    public string Name;
    public float value;
}

[System.Serializable]
public class AbilityCardUI
{
    public string ID;
    public int Lvl;
    public Sprite icon;
}
public class PlayerLvlController : MonoBehaviour
{
    List<int> prevStat = new();
    LvLUIController UIController;

    public List<StatsUI> Stats = new();
    public List<AbilityCardUI> Abilities = new();
    [SerializeField] int playerLvL, currentXP, XPtoLevelUp;

    private IMagicBase magicBase;

    private void Start()
    {
        XPtoLevelUp = ((int)Mathf.Pow(playerLvL, 2)) + 5;
        UIController = GetComponent<LvLUIController>();
        magicBase = GameObject.Find("AttacksAndAbilities").GetComponent<IMagicBase>();
    }

    public void AddXP(int value)
    {
        currentXP += (int)(value*(1+(PlayerStatsController.Stats._EXPGain)));
        if (currentXP >= XPtoLevelUp) LevelUP(currentXP-XPtoLevelUp);
    }

    private void LevelUP(int residualXP)
    {
        playerLvL++;
        switch (playerLvL)
        {
            case 3:
                magicBase.UnlockAbility1();
                break;
            case 5:
                magicBase.UnlockAbility2();
                break;
            case 7:
                magicBase.UnlockUltimate();
                break;
            default: break;
        }
        EventManager.Events.OnLevelUpEvent();
        SetStat();
        GetAbilityLvl();
        currentXP = residualXP;
        XPtoLevelUp = ((int)Mathf.Pow(playerLvL, 2)) + 5;
        if (currentXP >= XPtoLevelUp) LevelUP(currentXP - XPtoLevelUp);
        UIController.SetLevel(playerLvL);
    }

    private void SetStat()
    {
        Debug.LogWarning("Generating random stats");
        for (int i = 0; i < 3; i++)
        {
            int index = 0;
            while(prevStat.IndexOf(index) != -1)
            {
                index = Random.Range(0, Stats.Count);
            }
            UIController.AddStatToUI(Stats[index]);
            prevStat.Add(index);
        }
    }

    private void GetAbilityLvl()
    {
        List<int> lvls = new();
        bool generate = true;
        int n = 0;

        foreach (var item in magicBase.GetLevels()) lvls.Add(item);

        for (int i = 0; i < lvls.Count; i++)
        {
            Abilities[i].Lvl = lvls[i];
        }

        while (generate)
        {
            n = Random.Range(0, Abilities.Count);
            if (Abilities[n].Lvl != 0 && Abilities[n].Lvl < 5) generate = false;
        }
        Debug.LogWarning($"Generating ability: {Abilities[n].ID}");
        UIController.AddAbilityToUI(Abilities[n]);
    }
}
