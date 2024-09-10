using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore = 0;
    public Text scoreText;
    public GameObject gameOverScreen;
    public GameObject startScreen;  
    public bool gameStarted = false;
    public GameObject lisa;
    public AudioSource succesSound;

    private void Start()
    {
        Time.timeScale = 0;  
        startScreen.SetActive(true);
        scoreText.gameObject.SetActive(false);
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += 1;
        scoreText.text = playerScore.ToString();
        succesSound.Play();

    }

    public void startGame()
    {
        gameStarted = true;  
        Time.timeScale = 1;  
        startScreen.SetActive(false);
        lisa.SetActive(true);
        scoreText.gameObject.SetActive(true);

    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        gameStarted = false;
    }
}
