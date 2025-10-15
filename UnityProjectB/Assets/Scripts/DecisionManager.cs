using TMPro;
using UnityEngine;

//script made with reference to "Singletons in Unity (done right)" by gamedevbeginner.com
//manages the decisions, money, and evaluation

public class DecisionManager : MonoBehaviour
{
    public static DecisionManager Instance; //using a singleton instance so the manager can be accessed globally
    public PestGenerator.Pest currentPest; //stores the pest being currently evaluated

    public int currentMoney = 0; //tracks earned/lost money
    public TMP_Text costText; //displays current money and quota

    private int incorrectAmount = 0; //tracks how many wrong decisions made
    private int maxIncorrectAllowed = 3; //lose after 3 incorrect decisions a day

    public AudioSource correct; //sound effects
    public AudioSource incorrect;
    public ParticleSystem correctParticles;
    public ParticleSystem wrongParticles;

    private DayManager dayManager; //referencing the day manager for access to the daily set quotas

    //before starting, initiallize the singletons/references
    private void Awake() 
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject); //destroy any duplicates 

        dayManager = FindFirstObjectByType<DayManager>(); //find the day manager
        UpdateMoneyUI(); //update the money display at the start of the game
    }

    public void SetCurrentPest(PestGenerator.Pest pest) //called when a new pest is accepted for evaluation
    {
        currentPest = pest; //storing the current pest being processed 
        FindFirstObjectByType<ShiftManager>().SetDecisionButtons(true); //sets the decision buttons as enabled for the player
    }

    
    public void EvaluateDecision(PestGenerator.PestStatus playerChoice) 
    {
        //checking if the player chose the correst decision based on generated pest status (clear, report, incinerate)
        NPCSpawner npcSpawner = FindFirstObjectByType<NPCSpawner>(); 
        int cost = npcSpawner.currentCost; //storing the cost for the correct/incorrect decision


        if (currentPest.Status == playerChoice) //if match...
        {
            currentMoney = currentMoney + cost; //add money
            correct?.Play(); //play ding
            Debug.Log("Correct decision");
            if (correctParticles != null)
            {
                correctParticles.Play();
            }
        }
        else //if non match...
        {
            currentMoney = currentMoney - cost; //subtract money (can go negative)
            incorrect?.Play(); //play buzzer
            Debug.Log("Incorrect decision");

            incorrectAmount++;

            if (incorrectAmount >= maxIncorrectAllowed)
            {
                FindFirstObjectByType<DayManager>().sceneLoader.OpenGameOver();
                return;
            }

            if (wrongParticles != null)
            {
                wrongParticles.Play();
            }
        }

        UpdateMoneyUI();

        FindFirstObjectByType<ShiftManager>().SetDecisionButtons(false); //after making a choice disable the buttons
        FindFirstObjectByType<SlideWindow>().CloseWindow(); //close the window for next npc
        Debug.Log("window closed");

        //updating case in progress to false after window is closed
        if (npcSpawner != null)
        {
            npcSpawner.caseInProgress = false;
        }
    }

    //updating money display UI with current money and quota
    private void UpdateMoneyUI()
    {
        if (costText != null && dayManager != null)
        {
            int todayQuota = dayManager.GetTodayQuota();  //getting the daily quota from the DayManager
            costText.text = "$" + currentMoney.ToString() + " / $" + todayQuota.ToString(); //display as: $X / $Quota
        }
    }


    //resets and update the earned money to 0 at the start of a new day
    public void ResetMoney()
    {
        currentMoney = 0;
        incorrectAmount = 0;
        UpdateMoneyUI() ;
    }
}
