using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Pause Menu script copied over from Project A with help from "6 Minute PAUSE MENU Unity Tutorial" by BMo on YouTube

public class PauseMenuManager : MonoBehaviour
{
    //References to pause canvas, pause panels, adjustment sliders, audio source, light, and camera
    public Canvas pauseMenu;
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public Slider audioSlider;
    public Slider brightnessSlider;
    public Light overheadLamp;
    public AudioSource musicAudio;
  
    private bool isPaused = false; //boolean tracking if the game is paused


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.enabled = true; //ensures the menu is initally visible but the panels are hidden
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);

        if (musicAudio == null) //will find the audio source if none is assigned
            musicAudio = FindFirstObjectByType<AudioSource>();


        if (musicAudio != null) //sets initial volume (which will be the max)
            musicAudio.volume = 0.5f;

        if (audioSlider != null) //sets the initial slider values
            audioSlider.value = 1f;

        if (overheadLamp != null)
            brightnessSlider.value = overheadLamp.intensity - 50f; //baseline intensity adjustment for light
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) //listens for the escape key to be pressed to pause
        {
            if (isPaused) Resume();
            else Pause();
        }

        if (musicAudio != null && audioSlider != null)
        {
            musicAudio.volume = 0.5f * audioSlider.value; //updates the volume based on slider
        }

        if (overheadLamp != null && brightnessSlider != null)
            overheadLamp.intensity = 50f + brightnessSlider.value; //updates teh brightness based on slider
    }

    public void Pause()
    {
        pausePanel.SetActive(true); //will show the pause panel
        optionsPanel.SetActive(false); //hides the options panel
        Time.timeScale = 0f; //stops game time
        isPaused = true;
    }

    public void Resume() //resumes the game
    {
        //hides the panels and resumes the game time- also locking the cursor and starting camera movement
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenOptions() //opens options panel from pause
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions() //closes options to return to pause
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ReturnToMainMenu() //will return player to main start menu
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}