using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private float maxAlphaDistance = 5f;
    [SerializeField] private float minAlphaDistance = 1f;
    [SerializeField] private Bird _bird;

    private SpriteRenderer _spriteRenderer;
    private Transform _birdTransform;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _birdTransform = _bird.transform;

        SetAlpha(0f);
    }

    private void Update()
    {
        float distance = Mathf.Abs(transform.position.y - _birdTransform.position.y);

        float normalizedDistance = Mathf.InverseLerp(maxAlphaDistance, minAlphaDistance, distance);

        float alpha = Mathf.Clamp01(normalizedDistance);
        SetAlpha(alpha);
    }

    private void SetAlpha(float alpha)
    {
        Color color = _spriteRenderer.color;
        color.a = alpha;
        _spriteRenderer.color = color;
    }
}