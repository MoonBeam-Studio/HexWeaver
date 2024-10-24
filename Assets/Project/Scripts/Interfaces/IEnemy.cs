using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    int GetAttack();
    float GetSpeed();
    void GetHurt(int damage);
    void UpdateAttack(int value);
    void UpdateAttack(float percentage);
    void UpdateSpeed(int value);
    void UpdateSpeed(float percentage);

    void SetStatus(StatusEnum status);

    StatusEnum GetStatus();
}
