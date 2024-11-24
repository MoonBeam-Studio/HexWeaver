using UnityEngine;

public class PlayerLvlController : MonoBehaviour
{
    [SerializeField] int playerLvL, currentXP, XPtoLevelUp;

    private void Start()
    {
        XPtoLevelUp = ((int)Mathf.Pow(playerLvL, 2)) + 5;
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
        currentXP = residualXP;
        XPtoLevelUp = ((int)Mathf.Pow(playerLvL, 2)) + 5;
        if (currentXP >= XPtoLevelUp) LevelUP(currentXP - XPtoLevelUp);
    }
}
