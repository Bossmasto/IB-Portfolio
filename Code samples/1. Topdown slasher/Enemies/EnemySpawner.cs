using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] _enemyPrefabs;
    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] float _initialSpawnDelay;
    [SerializeField] float _respawnRate = 10f;
    [SerializeField] int _totalNumberToSpawn;
    [SerializeField] int _numberToSpawnEachTime = 1;

    float _spawnTimer;
    int _totalNumberSpawned;

    void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (ShouldSpawn())
            Spawn();
    }

    void OnEnable()
    {
        _spawnTimer = _respawnRate - _initialSpawnDelay;
    }

    bool ShouldSpawn()
    {
        if (_totalNumberSpawned >= _totalNumberToSpawn &&
            _totalNumberToSpawn > 0)
            return false;

        return _spawnTimer >= _respawnRate;
    }

    void Spawn()
    {
        _spawnTimer = 0;

        var availableSpawnPoints = _spawnPoints.ToList();


        for (int i = 0; i < _numberToSpawnEachTime; i++)
        {
            if (_totalNumberSpawned >= _totalNumberToSpawn &&
                _totalNumberToSpawn > 0)
                break;

            Enemy prefab = ChooseRandomEnemyPrefab();

            if (prefab != null)
            {
                Transform spawnPoint = ChooseRandomSpawnPoint(availableSpawnPoints);

                if (availableSpawnPoints.Contains(spawnPoint))
                    availableSpawnPoints.Remove(spawnPoint);

                //Enemy enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

                Enemy enemy = prefab.Get<Enemy>(spawnPoint.position, spawnPoint.rotation);

                _totalNumberSpawned++;
            }
        }
    }

    Transform ChooseRandomSpawnPoint(List<Transform> availableSpawnPoints)
    {
        if (availableSpawnPoints.Count == 0)
            return transform;

        if (availableSpawnPoints.Count == 1)
            return _spawnPoints[0];

        return availableSpawnPoints[UnityEngine.Random.Range(0, availableSpawnPoints.Count)];        //random spawnPoint based on the length
    }

    Enemy ChooseRandomEnemyPrefab()
    {
        if (_enemyPrefabs.Length == 0)
            return null;

        if (_enemyPrefabs.Length == 1)
            return _enemyPrefabs[0];

        return _enemyPrefabs[UnityEngine.Random.Range(0, _enemyPrefabs.Length)];        //random prefab based on the lenght
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, Vector3.one);

        foreach (var spawnPoint in _spawnPoints)
        {
            Gizmos.DrawSphere(spawnPoint.position, 0.5f);
        }
    }

#endif
}
