using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static bool gameHasEnded = false;
    public static bool gameIsPaused = false;


    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI gameOverTextUI;

    private void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            pauseMenuUI.transform.Find("Quit_Button").gameObject.SetActive(false);
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void EndGame(bool win)
    {
        Debug.Log("Im here");
        gameHasEnded = true;
        if (win)
        {
            //Display Win UI Screen
            gameOverUI.SetActive(true);
            playerUI.SetActive(false);
            gameOverTextUI.text = "You Win!";
        }
        else
        {
            //Display Lose UI Screen
            gameOverUI.SetActive(true);
            playerUI.SetActive(false);
            gameOverTextUI.text = "You Lose!";
        }
        Invoke("StopTime", 0.1f);
    }

    

    void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        playerUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Restart()
    {
        gameHasEnded = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
