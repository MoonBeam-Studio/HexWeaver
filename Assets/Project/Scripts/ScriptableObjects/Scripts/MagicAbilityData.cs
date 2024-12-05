using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Magics/Ability Data",fileName ="AbilityData", order=1)]
public class MagicAbilityData : ScriptableObject
{
    public string Name;
    public List<string> DescriptionList = new();
}
