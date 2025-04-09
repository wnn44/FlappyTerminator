using System;
using UnityEngine;

[RequireComponent(typeof(BirdCollisionHandler))]
public class Bird : MonoBehaviour
{
    [SerializeField] private float _loopXPosition;
    [SerializeField] private float _teleportOffset;

    private BirdCollisionHandler _handler;

    public event Action GameOver;

    private void Awake()
    {
        _handler = GetComponent<BirdCollisionHandler>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }

    private void Update()
    {
        if (transform.position.x > _loopXPosition)
        {
            Vector3 newPos = transform.position;
            newPos.x -= _teleportOffset;
            transform.position = newPos;
        }
    }

    private void ProcessCollision()
    {
        GameOver?.Invoke();
    }
}
