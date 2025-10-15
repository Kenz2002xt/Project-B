using TMPro;
using UnityEngine;
using UnityEngine.UI;

//script repurposed from previous project "BLEAK"

public class DayManager : MonoBehaviour
{
    public static DayManager instance; //static instance for singleton access
    public SceneLoader sceneLoader;

    public int currentDay = 1; //tracks current day number
    public int maxDays = 5; //max number of days

    public TMP_Text DayText;
    public Image FadePanel;
    public AudioSource cashSound;

    //array for weekday names
    private string[] weekDays = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

    //array for daily quota values
    private int[] quotas = {100, 150, 200, 250, 300};

    public GameObject InstructionsPanel;
    private bool firstTime = true;

    private void Awake()
    {
        if (instance == null) instance = this; 
        else Destroy(gameObject); //destroy any duplicates 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set transiton panel to fully opaque at the start
        if (FadePanel != null)
        {
            Color c = FadePanel.color;
            c.a = 1f; //only targets alpha
            FadePanel.color = c;
        }

        if (firstTime && InstructionsPanel != null)
        {
            InstructionsPanel.SetActive(true);
            firstTime = false;
        }
        else
        {
            StartNewDay(); //begin the first day
        }
    }

    // Update is called once per frame
    public void StartNewDay()
    {
        //as long as the fnal day isn't reached
        if (currentDay <= maxDays)
        {
            if (FadePanel != null)
            {
                Color c = FadePanel.color;
                c.a = 1f; //only targets alpha
                FadePanel.color = c;
            }

            cashSound?.Play(); //play knock sound
            ShowDayMessage(); //dsiplay text
            DecisionManager.Instance.ResetMoney(); //reset the money
            Invoke("HideBlackPanel", 3f); //fade after 3 seconds
        }
        else
        {
            Debug.Log("Game Won EndDay");
        }
    }


    public void EndDay()
    {
        if (DecisionManager.Instance.currentMoney < GetTodayQuota())
        {
            sceneLoader.OpenGameOver();
            return;
        }
        currentDay++; //increment the day count at the end of each shift

        //if the last day is passed then game over
        if (currentDay > maxDays)
        {
            Debug.Log("Game Won EndDay");
            sceneLoader.OpenWin();
        }
        else
        {
            StartNewDay(); //if last day hasn't passed then start next day
        }
    }

    void ShowDayMessage()
    {
        if (DayText != null)
        {
            int dayIndex = currentDay - 1; //convert to an array index
            DayText.text = $"{weekDays[dayIndex]}\nQuota: ${quotas[dayIndex]}"; //easy to write display text
            DayText.gameObject.SetActive(true); //show the UI text
            Invoke("HideDayText", 3f); //hide after 3 seconds
        }
    }

    //hides day text
    void HideDayText()
    {
        if (DayText != null)
            DayText.gameObject.SetActive(false);
    }

    //removes the black fade panel
    void HideBlackPanel()
    {
        if (FadePanel != null)
        {
            Color c = FadePanel.color;
            c.a = 0f; //only targets alpha
            FadePanel.color = c;
        }
    }


    //returns the day's quota value
    public int GetTodayQuota()
    {
        return quotas[currentDay - 1];
    }

    public void CloseInstructions()
    {
        if (InstructionsPanel != null)
        {
            InstructionsPanel.gameObject.SetActive(false);
        }

        StartNewDay();
    }
}
