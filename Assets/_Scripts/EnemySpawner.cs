using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const int MAX_SPAWN_SEARCH_ITERATIONS = 50;

    [SerializeField] private EnemyController _enemyPrefab;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private InputReaderSO _inputReaderSO;

    [Header("Check if spawn is valid based on other entities")]
    [SerializeField] private float _spawnRadiusLowerBound;
    [SerializeField] private float _spawnRadiusUpperBound;
    
    [SerializeField] private float _minDistanceBetweenEntities;

    [Header("Check if spawn is valid based on area collision")]
    [SerializeField] private LayerMask _spawnableAreaLayer;
    [SerializeField] private float _spawnCheckHeight = 5f;

    private List<EnemyController> _activeEnemies = new();
    private ObjectPool<EnemyController> _enemyPool;

    private void Start()
    {
        _enemyPool = new ObjectPool<EnemyController>(_enemyPrefab, 10, transform);

        _inputReaderSO.OnAttackEvent += DestroyClosestEnemy;
    }

    public void SpawnEnemy()
    {
        int iterCount = 0;
        Vector3 spawnPosition = Vector3.zero;
        bool isValid = false;

        do
        {
            iterCount++;
            Vector2 spawnCoordinats = Random.insideUnitCircle * Random.Range(_spawnRadiusLowerBound, _spawnRadiusUpperBound);
            
            spawnPosition.x = _playerTransform.position.x + spawnCoordinats.x;
            spawnPosition.z = _playerTransform.position.z + spawnCoordinats.y;

            if (!IsSpawnLocationValid(spawnPosition))
            {
                isValid = false;

                continue;
            } 

            foreach (var spawnedEnemy in _activeEnemies)
            {
                if (Vector3.Distance(spawnPosition, spawnedEnemy.transform.position) < _minDistanceBetweenEntities)
                {
                    isValid = false;

                    continue;
                }
            }

            isValid = true;

        } while (!isValid && iterCount < MAX_SPAWN_SEARCH_ITERATIONS);

        if (iterCount >= MAX_SPAWN_SEARCH_ITERATIONS) return;
        
        EnemyController enemy = _enemyPool.Get();
        enemy.transform.position = spawnPosition;
        enemy.transform.rotation = Quaternion.identity;
        enemy.Init(_playerTransform);

        _activeEnemies.Add(enemy);
    }

    private bool IsSpawnLocationValid(Vector3 position)
    {
        Vector3 checkPosition = position + Vector3.up * _spawnCheckHeight;

        return Physics.Raycast(checkPosition, Vector3.down, out RaycastHit hit, _spawnCheckHeight * 2, _spawnableAreaLayer);
    }

    private void DestroyClosestEnemy()
    {
        EnemyController closest = null;
        float minDist = float.MaxValue;

        foreach (var enemy in _activeEnemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, _playerTransform.position);
            if (dist < minDist)
            {
                closest = enemy;
                minDist = dist;
            }
        }

        if (closest != null)
        {
            _activeEnemies.Remove(closest);
            _enemyPool.ReturnToPool(closest);
        }
    }
}
