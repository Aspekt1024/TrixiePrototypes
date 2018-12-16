using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemySpawner : MonoBehaviour {

    public ExplodingEnemy ExplodingEnemyPrefab;
    public int EnemiesToSpawn = 1;
    public float Downtime = 1f;
    public float TimeEnemyKilled;

    private ExplodingEnemy[] enemies;
    private int enemiesSpawned = 0;

    private enum States
    {
        Idle, WaitingToSpawn
    }
    private States state;

    private void Start()
    {
        enemies = new ExplodingEnemy[EnemiesToSpawn];
    }

    private void Update()
    {
        switch (state)
        {
            case States.Idle:
                if (enemiesSpawned < EnemiesToSpawn)
                {
                    state = States.WaitingToSpawn;
                }
                break;
            case States.WaitingToSpawn:
                if (Time.time > TimeEnemyKilled + Downtime)
                {
                    SpawnEnemy();

                    if (enemiesSpawned >= EnemiesToSpawn)
                    {
                        state = States.Idle;
                    }
                }
                break;
            default:
                break;
        }

    }

    private void EnemyKilled(ExplodingEnemy enemy)
    {
        TimeEnemyKilled = Time.time;
        enemiesSpawned--;
        Camera.main.GetComponent<CameraShake>().Shake(0.2f, 1f);
    }

    private void SpawnEnemy()
    {
        Vector2 spawnLocation = GetSpawnPoint();

        if (enemies[enemiesSpawned] == null)
        {
            enemies[enemiesSpawned] = Instantiate(ExplodingEnemyPrefab);
            enemies[enemiesSpawned].OnDeath += EnemyKilled;
        }
        else
        {
            enemies[enemiesSpawned].Spawn(spawnLocation);
        }


        enemiesSpawned++;
    }


    private Vector2 GetSpawnPoint()
    {
        Vector2 centre = Vector2.zero;

        float minX = 0f;
        float maxX = 0f;
        float minY = 0f;
        float maxY = 0f;

        RaycastHit2D hit = Physics2D.Raycast(centre, Vector2.left, 10f, 1 << LayerMask.NameToLayer("Terrain"));
        minX = hit.point.x + 0.5f;

        hit = Physics2D.Raycast(centre, Vector2.right, 10f, 1 << LayerMask.NameToLayer("Terrain"));
        maxX = hit.point.x - 0.5f;

        hit = Physics2D.Raycast(centre, Vector2.up, 10f, 1 << LayerMask.NameToLayer("Terrain"));
        minY = hit.point.y - 0.5f;

        hit = Physics2D.Raycast(centre, Vector2.down, 10f, 1 << LayerMask.NameToLayer("Terrain"));
        maxY = hit.point.y + 0.5f;

        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

}
