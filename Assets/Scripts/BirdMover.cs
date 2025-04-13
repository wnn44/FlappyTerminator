using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdMover : MonoBehaviour
{
    [SerializeField] private float _tapForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;
    [SerializeField] private BirdCollisionHandler _birdCollisionHandler;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float targetAngle;

        if (_rigidbody2D.velocity.y < 0)
        {
            targetAngle = _minRotationZ;
        }
        else
        {
            targetAngle = _maxRotationZ;
        }

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        if (_birdCollisionHandler.IsAlive)
        {
            _rigidbody2D.velocity = new Vector2(_speed, _tapForce);
        }
    }
}
