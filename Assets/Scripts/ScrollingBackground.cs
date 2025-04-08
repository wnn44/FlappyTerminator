using UnityEngine;

public class InfiniteScrollingBackground : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 2f;

    private float _spriteWidth;
    private float _cameraWidth;
    private Vector3 _startPosition;

    private void Start()
    {
        _spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        _startPosition = transform.position;

        float cameraHeight = 2f * Camera.main.orthographicSize;
        _cameraWidth = cameraHeight * Camera.main.aspect;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * _scrollSpeed * Time.deltaTime);

        if (transform.position.x < _startPosition.x - (_spriteWidth - _cameraWidth))
        {
            transform.position = _startPosition;
        }
    }
}