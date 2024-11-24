
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MagicIcons",fileName = "Magic Icons")]
public class MagicIcons : ScriptableObject
{
    public List<Sprite> AutoAttack = new();
    public List<Sprite> Ability1 = new();
    public List<Sprite> Ability2 = new();
    public List<Sprite> Ultimate = new();
}
