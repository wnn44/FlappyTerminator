using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemy;
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private float _moveDistance = 3f;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private float _screenOffset = 1f;

    private Camera _mainCamera;
    private float _timer;
    private List<Enemy> _activeEnemies = new List<Enemy>();
    private EnemyMover _enemyMover;

    private void Start()
    {
        _mainCamera = Camera.main;
        _timer = _spawnInterval;
        _enemyMover = new EnemyMover(_moveSpeed, _waitTime);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            SpawnEnemy();
            _timer = _spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = _enemy[Random.Range(0, _enemy.Length)];

        _activeEnemies.Add(enemy);

        Vector2 spawnPos = GetRightSpawnPosition();
        enemy.transform.position = spawnPos;
        enemy.gameObject.SetActive(true);

        StartCoroutine(_enemyMover.MoveEnemy(enemy, spawnPos, _moveDistance));
    }

    private Enemy FindInactiveEnemy(Enemy prefab)
    {
        foreach (var enemy in _activeEnemies)
        {
            if (!enemy.gameObject.activeInHierarchy && enemy.name.StartsWith(prefab.name))
            {
                return enemy;
            }
        }
        return null;
    }

    private Vector2 GetRightSpawnPosition()
    {
        float offsetUp = -3f;
        float offsetDown = 3f;

        float rightEdge = _mainCamera.ViewportToWorldPoint(new Vector2(1, 0)).x;
        float randomY = Random.Range(offsetUp, offsetDown);
        return new Vector2(rightEdge + _screenOffset, randomY);
    }
}