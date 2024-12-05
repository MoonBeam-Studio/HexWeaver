
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Magics/Icons",fileName = "Magic Icons",order =0)]
public class MagicIcons : ScriptableObject
{
    public List<Sprite> AutoAttack = new();
    public List<Sprite> Ability1 = new();
    public List<Sprite> Ability2 = new();
    public List<Sprite> Ultimate = new();
}
