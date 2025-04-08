using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Activate(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}