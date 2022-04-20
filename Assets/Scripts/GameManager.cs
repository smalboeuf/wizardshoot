using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public bool GameOver = false;
    public int CurrentRound = 0;
    public float TimeBetweenRounds = 4;
    private int _enemiesToSpawn = 3;

    public List<GameObject> Enemies;

    public List<Transform> TopSpawnPositions;
    public List<Transform> LeftSpawnPositions;
    public List<Transform> RightSpawnPositions;
    public List<Transform> BottomSpawnPositions;

    private void StartGame()
    {
        StartWave();
    }

    public void StartWave()
    {
        SpawnArea spawnArea = GetRandomSpawnArea();
        SpawnWaveEnemies(spawnArea);
    }

    private void SpawnWaveEnemies(SpawnArea spawnArea)
    {
        List<Transform> topSpawnPointsClone = new List<Transform>();
        List<Transform> bottomSpawnPointsClone = new List<Transform>();
        List<Transform> leftSpawnPointsClone = new List<Transform>();
        List<Transform> rightSpawnPointsClone = new List<Transform>();

        topSpawnPointsClone.AddRange(TopSpawnPositions);
        bottomSpawnPointsClone.AddRange(BottomSpawnPositions);
        leftSpawnPointsClone.AddRange(LeftSpawnPositions);
        rightSpawnPointsClone.AddRange(RightSpawnPositions);
        
        // Get a randomized enemy
        GameObject randomEnemy = Enemies[Random.Range(0, Enemies.Count - 1)];
        // Get a random range of amount of enemies to spawn
        int amountOfEnemiesToSpawn = Random.Range(1, _enemiesToSpawn + 1);
        InstantiateEnemies(amountOfEnemiesToSpawn, topSpawnPointsClone, randomEnemy);
        amountOfEnemiesToSpawn = Random.Range(1, _enemiesToSpawn + 1);
        InstantiateEnemies(amountOfEnemiesToSpawn, bottomSpawnPointsClone, randomEnemy);
        amountOfEnemiesToSpawn = Random.Range(1, _enemiesToSpawn + 1);
        InstantiateEnemies(amountOfEnemiesToSpawn, leftSpawnPointsClone, randomEnemy);
        amountOfEnemiesToSpawn = Random.Range(1, _enemiesToSpawn + 1);
        InstantiateEnemies(amountOfEnemiesToSpawn, rightSpawnPointsClone, randomEnemy);
    }

    private void InstantiateEnemies(int numberOfEnemies, List<Transform> spawnPoints, GameObject enemy)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Count - 1);
            Transform enemySpawnPoint = spawnPoints[spawnPointIndex];
            Instantiate(enemy, enemySpawnPoint.position, Quaternion.identity);
            spawnPoints.RemoveAt(spawnPointIndex);
        }
    }

    private SpawnArea GetRandomSpawnArea() {
        return (SpawnArea)Random.Range(1, 3);
    }
}

enum SpawnArea
{
    Top,
    Bottom,
    Left,
    Right
}
