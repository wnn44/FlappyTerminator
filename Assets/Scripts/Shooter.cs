using UnityEngine;
using System.Collections.Generic;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Bullet _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private int _poolSize = 20;

    private float _nextFireTime = 0f;
    private Queue<Bullet> _bulletPool = new Queue<Bullet>();

    private void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            Bullet bullet = Instantiate(_projectilePrefab);
            bullet.gameObject.SetActive(false);
            bullet.SetPool(this);
            _bulletPool.Enqueue(bullet);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= _nextFireTime && Time.timeScale > 0)
        {
            Shoot();
            _nextFireTime = Time.time + 1f / _fireRate;
        }
    }

    public void ReturnBulletToPool(Bullet bullet)
    {
        if (bullet == null) return;

        bullet.gameObject.SetActive(false);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;

        _bulletPool.Enqueue(bullet);
    }

    public void Shoot()
    {
        if (_bulletPool.Count == 0)
        {
            return;
        }

        Bullet projectile = _bulletPool.Dequeue();

        projectile.gameObject.SetActive(true);
        projectile.transform.position = _firePoint.position;
        projectile.transform.rotation = _firePoint.rotation;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = projectile.transform.right * _projectileSpeed;

        VoicingShoot();

        projectile.Activate();
    }

    private void VoicingShoot()
    {
        if (Time.timeScale > 0)
        {
            _audioSource.Play();
        }
    }
}