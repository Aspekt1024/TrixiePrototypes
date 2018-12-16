using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnemySpawner : MonoBehaviour {

    public enum Modes
    {
        Random, Repeat
    }

    public EnemyBase[] EnemyPrefabs;
    public Modes Mode;

    // Bounds:
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    
    private float spawnDelay = 1.5f;

    private void Start()
    {
        ArenaUI.SetEnemyName("spawning...");
        CalculateBounds();
        StartCoroutine(SpawnEnemy());
    }

    private void EnemyDefeated(EnemyBase enemy)
    {
        Destroy(enemy.gameObject);
        StartCoroutine(SpawnEnemy());
    }
    
    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnDelay);

        int i = Random.Range(0, EnemyPrefabs.Length);
        EnemyBase enemy = Instantiate(EnemyPrefabs[i]);
        Vector2 spawnLocation = new Vector2();

        switch (enemy.Type)
        {
            case EnemyBase.EnemyTypes.Undefined:
                spawnLocation.x = maxX / 2f;
                spawnLocation.y = (maxY - minY) / 2f;
                break;
            case EnemyBase.EnemyTypes.Ground:
                spawnLocation.x = maxX / 2f;
                spawnLocation.y = minY;
                break;
            case EnemyBase.EnemyTypes.Air:
                spawnLocation.x = Random.Range(minX, maxX);
                spawnLocation.y = Random.Range(minY, maxY);
                break;
            default:
                break;
        }

        enemy.Spawn(spawnLocation);
        ArenaUI.SetEnemyName(enemy.GetType().ToString());

        enemy.OnDeath += EnemyDefeated;
    }

    private void CalculateBounds()
    {
        Vector2 centre = Vector2.zero;

        RaycastHit2D hit = Physics2D.Raycast(centre, Vector2.left, 100f, 1 << LayerMask.NameToLayer("Terrain"));
        minX = hit.point.x + 0.5f;

        hit = Physics2D.Raycast(centre, Vector2.right, 100f, 1 << LayerMask.NameToLayer("Terrain"));
        maxX = hit.point.x - 0.5f;

        hit = Physics2D.Raycast(centre, Vector2.up, 10f, 1 << LayerMask.NameToLayer("Terrain"));
        maxY = hit.point.y - 0.5f;

        hit = Physics2D.Raycast(centre, Vector2.down, 10f, 1 << LayerMask.NameToLayer("Terrain"));
        minY = hit.point.y + 0.5f;
    }
}
