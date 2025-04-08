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

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Rigidbody2D _rigidbody2D;
    //private Quaternion _maxRotation;
    //private Quaternion _minRotation;

    private void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        //_maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        //_minRotation = Quaternion.Euler(0, 0, _minRotationZ);

        Reset();
    }

    private void Update()
    {
        float targetAngle;

        if (Input.GetKeyDown(KeyCode.Space) && _birdCollisionHandler.IsAlive)
        {
            _rigidbody2D.velocity = new Vector2(_speed, _tapForce);
        }

        if (_rigidbody2D.velocity.y < 0)
        {
            targetAngle = _minRotationZ;
        }
        else
        {
            targetAngle = _maxRotationZ;
        }

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime
        );
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _rigidbody2D.velocity = Vector2.zero;
    }
}
