using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagicBase 
{
    void SetAttackCount();
    void SetAttackCount(int value);
    int GetAttackCount();
    void Ability1();
    void Ability2();
    void Ultimate();
}
