using System;
using UnityEngine;

[RequireComponent(typeof(Bird))]
public class BirdCollisionHandler : MonoBehaviour
{
    public event Action CollisionDetected;

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Ground ground))
        {
            Debug.Log("Земля");
            CollisionDetected?.Invoke();
        }
    }
}
