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

public class PlayerLvlController : MonoBehaviour
{
    List<int> prevStat = new();
    LvLUIController UIController;

    public List<StatsUI> Stats = new();
    [SerializeField] int playerLvL, currentXP, XPtoLevelUp;

    private void Start()
    {
        XPtoLevelUp = ((int)Mathf.Pow(playerLvL, 2)) + 5;
        UIController = GetComponent<LvLUIController>();
    }

    public void AddXP(int value)
    {
        currentXP += (int)(value*(1+(PlayerStatsController.Stats._EXPGain)));
        if (currentXP >= XPtoLevelUp) LevelUP(currentXP-XPtoLevelUp);
    }

    private void LevelUP(int residualXP)
    {
        playerLvL++;
        EventManager.Events.OnLevelUpEvent();
        SetStat();
        currentXP = residualXP;
        XPtoLevelUp = ((int)Mathf.Pow(playerLvL, 2)) + 5;
        if (currentXP >= XPtoLevelUp) LevelUP(currentXP - XPtoLevelUp);
    }

    private void SetStat()
    {
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
}
