using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyerService
{
    private class EnemyEntry
    {
        public Enemy Enemy;
        public Func<bool> Condition;
    }

    private readonly List<EnemyEntry> _enemies = new();

    public int EnemyCount => _enemies.Count;

    public void RegisterEnemy(Enemy enemy, Func<bool> condition)
    {
        _enemies.Add(new EnemyEntry
        {
            Enemy = enemy,
            Condition = condition
        });
    }

    public void Update()
    {
        for (int i = _enemies.Count - 1; i >= 0; i--)
        {
            var entry = _enemies[i];

            if (entry.Condition())
            {
                entry.Enemy.Kill();
                _enemies.RemoveAt(i);
            }
        }

        Debug.Log($"Количество живых врагов: {_enemies.Count}");
    }

    public bool IsDeadCondition(IKillable enemy) => enemy.IsDead;

    public bool TimerCondition(IKillable enemy) => enemy.LifeTime <= 0;

    public bool CountCondition(int count) => EnemyCount <= count;
}