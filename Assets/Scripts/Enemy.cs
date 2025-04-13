using UnityEngine;

[RequireComponent (typeof(Shooter))]
[RequireComponent(typeof(EnemyCollisionHandler))]
public class Enemy : MonoBehaviour
{
    public Shooter Shooter { get; private set; }

    private void Awake()
    {
        Shooter = GetComponent<Shooter>();
    }

    public void Activate(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}