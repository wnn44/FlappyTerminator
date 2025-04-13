using System;
using UnityEngine;

[RequireComponent(typeof(Bird))]
public class BirdCollisionHandler : MonoBehaviour
{
    public bool IsAlive { get; private set; } = true;

    public event Action GameOver;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Ground ground))
        {            
            GameOver?.Invoke();
        }

        if (other.TryGetComponent(out Bullet bullet))
        {
            IsAlive = false;
        }
    }
}
