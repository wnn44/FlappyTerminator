using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIButtons : MonoBehaviour
{
    [Header("Кнопки")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        Time.timeScale = 0f;

        _startButton.onClick.AddListener(OnStartButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        _startButton.gameObject.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    private void OnRestartButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClicked);
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }
}
