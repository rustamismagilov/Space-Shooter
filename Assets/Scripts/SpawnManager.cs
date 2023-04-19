using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private GameObject[] _PowerUpIconsPrefabs;

    private bool _stopSpawning = false;

    float powerUpLifetime = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnAsteroidsRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 spawnPosition = new(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 3);
            GameObject powerUpObject = Instantiate(_PowerUpIconsPrefabs[randomPowerUp], SpawnPosition, Quaternion.identity);
            Destroy(powerUpObject, powerUpLifetime);
            yield return new WaitForSeconds(Random.Range(7.0f, 15.0f));
        }
    }

    IEnumerator SpawnAsteroidsRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_PowerUpIconsPrefabs[randomPowerUp], SpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
