using UnityEngine;
using System.Collections;

public class EnemyMover
{
    private float _moveSpeed;
    private float _waitTime;

    public EnemyMover(float moveSpeed, float waitTime)
    {
        _moveSpeed = moveSpeed;
        _waitTime = waitTime;
    }

    public IEnumerator MoveEnemy(Enemy enemy, Vector2 startPos, float moveDistance)
    {
        float waitTimer = 0f;
        float nextShotTime = 0f;
        int minBullets = 1;
        int maxBullets = 5;
        int shotsToFire;
        int shotsFired = 0;

        Vector2 targetPos = startPos - new Vector2(moveDistance, 0);

        yield return MoveEnemyToPosition(enemy, startPos, targetPos);

        while (waitTimer < _waitTime)
        {
            waitTimer += Time.deltaTime;
            shotsToFire = Random.Range(minBullets, maxBullets);

            if (shotsFired < shotsToFire && Time.time >= nextShotTime)
            {
                enemy.Shooter.Shoot();
                shotsFired++;
                nextShotTime = Time.time + Random.Range(0, _waitTime / shotsToFire);
            }

            yield return null;
        }

        yield return MoveEnemyToPosition(enemy, targetPos, startPos);

        enemy.gameObject.SetActive(false);
    }

    public IEnumerator MoveEnemyToPosition(Enemy enemy, Vector2 fromPos, Vector2 toPosition)
    {
        float limitAccuracy = 0.01f;
        Vector2 toTarget = toPosition - (Vector2)enemy.transform.position;

        while (toTarget.sqrMagnitude > limitAccuracy)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                yield break;
            }

            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, toPosition, _moveSpeed * Time.deltaTime);
            toTarget = toPosition - (Vector2)enemy.transform.position;
            yield return null;
        }

        enemy.transform.position = toPosition;
    }
}