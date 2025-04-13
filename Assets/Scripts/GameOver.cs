using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameOverPanel _gameOverPanel;
    [SerializeField] private BirdCollisionHandler _birdCollisionHandler;

    private void OnEnable()
    {
        _birdCollisionHandler.GameOver += ActivateGameOverPanel;
    }

    private void OnDisable()
    {
        _birdCollisionHandler.GameOver -= ActivateGameOverPanel;
    }

    private void ActivateGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
    }
}
