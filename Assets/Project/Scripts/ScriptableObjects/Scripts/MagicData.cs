using UnityEngine;

[CreateAssetMenu(menuName = "Magic/General", fileName ="General", order = 0)]
public class MagicData : ScriptableObject
{
    public MagicAbilityData AutoAttackData;
    public MagicAbilityData Ability1Data;
    public MagicAbilityData Ability2Data;
    public MagicAbilityData UltimateData;
    public MagicIcons Icons;
}
