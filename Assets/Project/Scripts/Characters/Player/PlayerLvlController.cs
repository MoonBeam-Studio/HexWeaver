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
    public string name;
    public string description;
}
public class PlayerLvlController : MonoBehaviour
{
    List<int> prevStat = new();
    LvLUIController UIController;

    public List<StatsUI> Stats = new();
    public List<AbilityCardUI> Abilities = new();
    public MagicData magicData;
    [SerializeField] int playerLvL, currentXP, XPtoLevelUp;

    private IMagicBase magicBase;

    private void Start()
    {
        XPtoLevelUp = ((int)Mathf.Pow(playerLvL, 2)) + 5;
        UIController = GetComponent<LvLUIController>();
        magicBase = GameObject.Find("AttacksAndAbilities").GetComponent<IMagicBase>();
        
        Abilities[0].name = magicData.AutoAttackData.Name;
        Abilities[0].icon = magicData.Icons.AutoAttack[0];

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

        string _description = "", _name = "";
        switch (Abilities[n].ID)
        {
            case "Attack": 
                _description = magicData.AutoAttackData.DescriptionList[Abilities[n].Lvl-1].ToString(); 
                _name = magicData.AutoAttackData.Name.ToString();
                break;
            case "Ability1":
                _description = magicData.Ability1Data.DescriptionList[Abilities[n].Lvl-1].ToString();
                _name = magicData.Ability1Data.Name.ToString();
                break;
            case "Ability2": 
                _description = magicData.Ability2Data.DescriptionList[Abilities[n].Lvl-1].ToString();
                _name = magicData.Ability2Data.Name.ToString();
                break;
            case "Ultimate": 
                _description = magicData.UltimateData.DescriptionList[Abilities[n].Lvl-1].ToString();
                _name = magicData.UltimateData.Name.ToString();
                break;
        }
        Abilities[n].name = _name;
        Abilities[n].description = _description;

        UIController.AddAbilityToUI(Abilities[n]);
    }
}
