using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private Shooter _shooter;
    [SerializeField] private BirdMover _birdMover;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private BirdCollisionHandler _birdCollisionHandler;

    private float _nextFireTime = 0f;

    private void OnEnable()
    {
        _inputReader.ShootPressed += OnShootPressed;
        _inputReader.JumpPressed += OnJump;
    }

    private void OnDisable()
    {
        _inputReader.ShootPressed -= OnShootPressed;
        _inputReader.JumpPressed -= OnJump;
    }

    private void FixedUpdate()
    {
        if (_birdCollisionHandler.IsAlive == false)
        {
            VoicingShoot();
        }
    }

    private void OnShootPressed()
    {
        if (Time.time >= _nextFireTime && Time.timeScale > 0)
        {
            _shooter.Shoot();
            _nextFireTime = Time.time + 1f / _fireRate;
        }
    }

    private void OnJump()
    {
        _birdMover.Jump(); 
    }

    private void VoicingShoot()
    {
        _audioSource.Play();
    }
}
