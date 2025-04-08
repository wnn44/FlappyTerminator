using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private ScoreCounter _scoreCounter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Bullet bullet))
        {
            _scoreCounter.Add();

            _enemy.Activate(false);
        }
    }
}
 