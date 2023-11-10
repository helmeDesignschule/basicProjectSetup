using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// a test implementation of the Observer pattern.
/// Here, we observe all enemies, and unlock an achievement, once enough enemies where defeated.
/// </summary>
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
