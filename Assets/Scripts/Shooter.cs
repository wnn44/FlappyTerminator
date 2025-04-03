using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Bullet _projectilePrefab; // ������ �������
    [SerializeField] private Transform _firePoint; // �����, ������ �������� ������ (����� ��������� �������� � ����������)
    [SerializeField] private float _projectileSpeed = 10f; // �������� �������
    [SerializeField] private float _fireRate = 0.5f; // ���������������� (��������� � �������)

    private float _nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + 1f / _fireRate;
        }
    }

    void Shoot()
    {
        Bullet projectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = projectile.transform.right * _projectileSpeed;
    }
}
