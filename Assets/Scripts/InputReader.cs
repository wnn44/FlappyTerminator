using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    public event UnityAction JumpPressed;
    public event UnityAction ShootPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpPressed?.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            ShootPressed?.Invoke();
        }
    }
}