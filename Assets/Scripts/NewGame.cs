using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : VisibilityController
{
    public void PauseGame()
    {        
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {        
        Time.timeScale = 1;
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        Hide();
        ResumeGame();
    }
}
