using System;
using UnityEngine;

//[RequireComponent(typeof(BirdMover))]
[RequireComponent(typeof(BirdCollisionHandler))]
public class Bird : MonoBehaviour
{
    [SerializeField] private float _loopXPosition;
    [SerializeField] private float _teleportOffset;
                                                          
    // private BirdMover _birdMover;
    private BirdCollisionHandler _handler;

    public event Action GameOver;

    private void Awake()
    {
        _handler = GetComponent<BirdCollisionHandler>();
    //    _birdMover = GetComponent<BirdMover>();
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
            Time.timeScale = 0;
            GameOver?.Invoke();
    }
}
