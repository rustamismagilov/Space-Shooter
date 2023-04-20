using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    public GameObject _asteroidContainer;
    [SerializeField] private GameObject[] _asteroidPrefab;
    [SerializeField] private GameObject[] _PowerUpIconsPrefabs;

    private bool _stopSpawning = false;

    [SerializeField] float powerUpLifetime = 7.0f;
    [SerializeField] float asteroidLifetime = 8.0f;

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
            yield return new WaitForSeconds(Random.Range(15.0f, 30.0f));
        }
    }

    IEnumerator SpawnAsteroidsRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomAsteroidPrefab = Random.Range(0, 1);
            GameObject asteroidObject = Instantiate(_asteroidPrefab[randomAsteroidPrefab], SpawnPosition, Quaternion.identity);
            asteroidObject.transform.parent = _asteroidContainer.transform;
            AsteroidsController asteroid = asteroidObject.GetComponent<AsteroidsController>();
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            Destroy(asteroidObject, asteroidLifetime);
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        RemoveExistingObjectsOnPlayerDeath();
    }

    private void RemoveExistingObjectsOnPlayerDeath()
    {
        foreach (Transform child in _enemyContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in _asteroidContainer.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach (GameObject powerUp in powerUps)
        {
            Destroy(powerUp);
        }
    }
}
