using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Level1Button()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1_Demo");
    }

    public void Level1TrapEveryWhereButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1_TrapsEveryWhere");
    }

    public void TutorialButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1_Tutorial");
    }
}
