using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public Button tryAgainButton;
   
public TextMeshProUGUI highScoreText;
    private bool isGameOver = false;

    void Start()
    {
        Time.timeScale = 1f;

        if (tryAgainButton != null)
        {
            tryAgainButton.onClick.AddListener(RestartGame);
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f; // Pause the game when the game is over
        gameOverUI.SetActive(true);
        int currentScore = player_movement.numberofCoins;
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        // Check and update if new high score
        if (currentScore > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
        }

        // Update text
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }
        

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
