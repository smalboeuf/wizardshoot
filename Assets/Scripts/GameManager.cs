using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private PlayerController player;

    [SerializeField] private int _enemiesToBeKilledBeforeDrop = 10;
    public int EnemiesKilledSinceLastDrop = 0;

    public int CurrentRound = 0;
    public float TimeBetweenRounds = 4;
    private int _enemiesToSpawn = 3;
    [SerializeField] private bool _canSpawnEnemies = true;

    public List<GameObject> Enemies;

    public List<Transform> TopSpawnPositions;
    public List<Transform> LeftSpawnPositions;
    public List<Transform> RightSpawnPositions;
    public List<Transform> BottomSpawnPositions;
   
    public void GameOver()
    {
        _canSpawnEnemies = false;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }

        gameOverPanel.SetActive(true);
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
    }

    public void PlayAgain()
    {
        gameOverPanel.SetActive(false);
        SpawnWaveEnemies();
        _canSpawnEnemies = true;
        player.CanMove = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SpawnWaveEnemies()
    {
        if (_canSpawnEnemies)
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

            CurrentRound++;
            RoundBasedChecks();
        }
    }

    public bool MinimumEnemiesForADropKilled()
    {
        return EnemiesKilledSinceLastDrop >= _enemiesToBeKilledBeforeDrop;
    }

    private void RoundBasedChecks()
    {
        if (CurrentRound % 10 == 0)
        {
            if (_enemiesToSpawn < 6)
            {
                _enemiesToSpawn++;
            }
        }

        if (CurrentRound % 5 == 0)
        {
            if (TimeBetweenRounds > 1)
            {
                TimeBetweenRounds -= 0.25f;
            }
        }
    }

    private void InstantiateEnemies(int numberOfEnemies, List<Transform> spawnPoints, GameObject enemyObject)
    {
        // For each enemy find a random spawn point
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Count - 1);
            Transform enemySpawnPoint = spawnPoints[spawnPointIndex];
            GameObject spawnedEnemy = Instantiate(enemyObject, enemySpawnPoint.position, Quaternion.identity);
            Enemy enemy = spawnedEnemy.GetComponent<Enemy>();
            enemy.SpawnEvents();
            enemy.SpawnArea = enemySpawnPoint.GetComponent<SpawnPoint>().SpawnArea;
            
            spawnPoints.RemoveAt(spawnPointIndex);
        }
    }
}

