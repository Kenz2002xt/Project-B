using UnityEngine;
using UnityEngine.SceneManagement;

//system to load in scenes 
//used to attach to buttons throughout gameplay

public class SceneLoader : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OpenControls()
    {
        SceneManager.LoadScene("ControlsScene");
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OpenWin()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void OpenGameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
