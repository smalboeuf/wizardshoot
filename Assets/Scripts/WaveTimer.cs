using UnityEngine;

public class WaveTimer : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private float _timeCounter = 0;

    private void Update()
    {
        if (_timeCounter <= 0)
        {
            HandleSpawnWave();
        }

        if (_timeCounter > 0)
        {
            _timeCounter -= Time.deltaTime;
        }

        if (_timeCounter <= 0)
        {
            _timeCounter = 0;
        }
    }

    private void HandleSpawnWave()
    {
        gameManager.StartWave();
        _timeCounter = gameManager.TimeBetweenRounds;
    }
}
