using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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

    private void Start()
    {
        _mainCamera = Camera.main;
        _timer = _spawnInterval;
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

        StartCoroutine(EnemyMovement(enemy, spawnPos));
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

    private IEnumerator EnemyMovement(Enemy enemy, Vector2 startPos)
    {
        float waitTimer = 0f;
        float nextShotTime = 0f;
        int minBullets = 1;
        int maxBullets = 5;
        int shotsToFire;
        int shotsFired = 0;

        Vector2 targetPos = startPos - new Vector2(_moveDistance, 0);

        while (Vector2.Distance(enemy.transform.position, targetPos) > 0)
        {
            if (!enemy.gameObject.activeInHierarchy) yield break;
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, targetPos, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        Shooter enemyShooter = enemy.GetComponent<Shooter>();

        if (enemyShooter == null)
        {
            enemyShooter = enemy.gameObject.AddComponent<Shooter>();
        }

        while (waitTimer < _waitTime)
        {
            waitTimer += Time.deltaTime;
            shotsToFire = Random.Range(minBullets, maxBullets);

            if (shotsFired < shotsToFire && Time.time >= nextShotTime)
            {
                enemyShooter.Shoot();
                shotsFired++;
                nextShotTime = Time.time + Random.Range(0, _waitTime / shotsToFire);
            }

            yield return null;
        }

        while (Vector2.Distance(enemy.transform.position, startPos) > 0)
        {
            if (!enemy.gameObject.activeInHierarchy) yield break;
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, startPos, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        enemy.gameObject.SetActive(false);
    }
}