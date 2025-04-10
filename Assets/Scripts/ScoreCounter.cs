using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private int _score;

    public event Action<int> ScoreChanged;

    public void Add()
    {
        _score++;
        VoicingShoot();
        ScoreChanged?.Invoke(_score);
    }

    public void Reset()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
    }

    private void VoicingShoot()
    {
        _audioSource.Play();
    }
}
