using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int zombieCount;
    public GameObject gameCompletedPanel;
    public GameObject gamePausePanel;
    public GameObject gameFailedPanel;
    public static int playerScore;
    public Text playerScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckZombieCount() // Check if all zombies killed then game completed
    { 
        if (zombieCount <= 0)
        {
            gameCompletedPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void GameFailed()  // Game Failed
    {
        gameFailedPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        gamePausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gamePausePanel.SetActive(false);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadLevelTwo()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }
    public void LoadLevelThree()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level3");
    }
    public void LoadLevelFour()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level4");
    }
    public void LoadLevelFive()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level5");
    }
    public void UpdatePlayerScore()
    {
        playerScoreText.text = "Score: "+playerScore;
    }
}
