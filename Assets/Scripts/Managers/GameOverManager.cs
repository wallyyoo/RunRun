using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    private bool isGameOver = false;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene"); // ¾À
    }
}
