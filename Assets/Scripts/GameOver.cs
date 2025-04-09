using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Bird _bird;

    private void OnEnable()
    {
        _bird.GameOver += ActivateGameOverPanel;
    }

    private void OnDisable()
    {
        _bird.GameOver -= ActivateGameOverPanel;
    }

    private void ActivateGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
