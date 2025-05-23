using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const int MAX_SPAWN_SEARCH_ITERATIONS = 50;

    [SerializeField] private EnemyController _enemyPrefab;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private InputReaderSO _inputReaderSO;

    [SerializeField] private float _spawnRadiusLowerBound;
    [SerializeField] private float _spawnRadiusUpperBound;
    
    [SerializeField] private float _minDistanceBetweenEntities;

    private List<EnemyController> _spawnedEnemies = new ();

    private void Start()
    {
        _inputReaderSO.OnAttackEvent += () => DestroyClosestEnemy();
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

            foreach (var spawnedEnemy in _spawnedEnemies)
            {
                if (Vector3.Distance(spawnPosition, spawnedEnemy.transform.position) < _minDistanceBetweenEntities)
                {
                    isValid = false;

                    continue;
                }
            }
        } while (!isValid && iterCount < MAX_SPAWN_SEARCH_ITERATIONS);

        EnemyController enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.Init(target: _playerTransform);
        
        _spawnedEnemies.Add(enemy);
    }

    private void DestroyClosestEnemy() 
    {
        EnemyController closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (var spawnedEnemy in _spawnedEnemies)
        {
            var distanceToPlayer = Vector3.Distance(spawnedEnemy.transform.position, _playerTransform.position);

            if (distanceToPlayer < closestDistance)
            {
                closestEnemy = spawnedEnemy;
                closestDistance = distanceToPlayer;
            }
        }

        if (closestEnemy == null) return;
        
        _spawnedEnemies.Remove(closestEnemy);
        closestEnemy.Kill();
    }
}
