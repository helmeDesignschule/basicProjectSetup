using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementHandler : MonoBehaviour
{
    private int _enemiesKilled = 0;
    private void OnEnable()
    {
        Enemy.OnEnemyDied += TestForEnemiesKilledAchievement;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDied -= TestForEnemiesKilledAchievement;
    }

    private void TestForEnemiesKilledAchievement(Enemy enemy)
    {
        _enemiesKilled++;
        if (_enemiesKilled == 3)
            Debug.Log("You unlocked the Enemy Slayer Achievement!");
    }
}
