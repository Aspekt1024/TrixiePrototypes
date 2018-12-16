using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EnemyHandler : MonoBehaviour
{
    public float RespawnTime = 1f;

    public Enemy[] Enemies;

    private struct destroyedEnemy
    {
        public Enemy enemy;
        public float destroyedTime;
    }

    private readonly List<destroyedEnemy> destroyedEnemies = new List<destroyedEnemy>();
    
    private enum states
    {
        respawning, normal
    }
    private states state;

    public void EnemyDestroyed(Enemy enemy)
    {
        if (state == states.respawning) return;

        enemy.gameObject.SetActive(false);

        if (enemy.IsEvil)
        {
            state = states.respawning;
            StartCoroutine(RespawnRoutine());
        }

        var destroyedEnemy = new destroyedEnemy
        {
            enemy = enemy,
            destroyedTime = Time.time,
        };
        destroyedEnemies.Add(destroyedEnemy);

    }

    private void Start()
    {
        PickEvilEnemy();
        state = states.normal;
    }

    private void PickEvilEnemy()
    {
        var evilEnemyIndex = UnityEngine.Random.Range(0, Enemies.Length);
        for (int i = 0; i < Enemies.Length; i++)
        {
            if (i == evilEnemyIndex)
            {
                Enemies[i].SetEvil();
            }
            else
            {
                Enemies[i].SetFriendly();
            }
        }
    }

    private void Update()
    {
        if (state == states.respawning) return;

        for (int i = destroyedEnemies.Count - 1; i >= 0; i--)
        {
            if (destroyedEnemies[i].destroyedTime + destroyedEnemies[i].enemy.DeathCooldown < Time.time)
            {
                destroyedEnemies[i].enemy.Revive();
                destroyedEnemies.Remove(destroyedEnemies[i]);
            }
        }
    }

    private System.Collections.IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(RespawnTime);

        foreach (var e in destroyedEnemies)
        {
            e.enemy.Revive();
        }
        destroyedEnemies.Clear();
        PickEvilEnemy();

        state = states.normal;
    }
    
}
