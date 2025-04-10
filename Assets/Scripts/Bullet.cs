using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifetime = 3f;

    private Shooter _pool;
    private Coroutine _lifeCoroutine;

    public void SetPool(Shooter pool)
    {
        _pool = pool;
    }

    public void Activate()
    {
        _lifeCoroutine = StartCoroutine(LifeRoutine());
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(_lifetime);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (_pool != null)
        {
            _pool.ReturnBulletToPool(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ReturnToPool();
    }

    private void OnDisable()
    {
        if (_lifeCoroutine != null)
        {
            StopCoroutine(_lifeCoroutine);
            _lifeCoroutine = null;
        }
    }
}