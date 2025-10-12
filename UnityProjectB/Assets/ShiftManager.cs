using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;

public class ShiftManager : MonoBehaviour
{
    public float shiftDuration = 300f; //length of a shift in seconds
    private float shiftTimer; //timer for the active shift
    private float shiftStartHour = 3f;
    private float shiftEndHour = 6f;
    private bool shiftActive = false; //will track if a shift is currently running

    public TMP_Text timerText;
    public TMP_Text infoText;
    public SlideWindow windowScript;
    public Button startShiftButton;

    public Button[] decisionButtons;

    private void Start()
    {
        SetDecisionButtons(false); //disable the decision buttons at the start of the shift
    }
    // Update is called once per frame
    void Update()
    {
        //only start the timer if the shift is active 
        if (shiftActive)
        {
            shiftTimer = shiftTimer - Time.deltaTime;

            //if timer reaches 0 then end the shfit
            if (shiftTimer <= 0)
            {
                EndShift();
            }
            UpdateTimerUI(); //update UI with current shift time
        }
    }

    //start the shift
    public void StartShift()
    {
        windowScript.canOpen = false; //window can't open until npc arrives
        FindFirstObjectByType<NPCSpawner>()?.ResetNPCState(); //resets the NPCs for new shift to avoid bugs as long as NPC spawner isnt null
        shiftTimer = shiftDuration; //set the timer to full shift duration
        shiftActive = true; //mark the shift as active
        Debug.Log("Shift Started!");

        if (startShiftButton != null)
        {
            startShiftButton.interactable = false; //clock in button to start shift is inactive
        }

        timerText.gameObject.SetActive(true); //show the timer text on the UI

        infoText.gameObject.SetActive(true); //show the shift helper text
        StartCoroutine(HideInfoText());
    }

    //hide info text after 3 seconds
    private System.Collections.IEnumerator HideInfoText()
    {
        yield return new WaitForSeconds(3f);
        infoText.gameObject.SetActive(false);

    }


    //ends the shift
    private void EndShift()
    {
        DayManager.instance.EndDay(); //tell the DayManager the day has ended
        shiftActive = false; //set shift as inactive
        windowScript.canOpen = false; //window cannot open
        windowScript.CloseWindow(); //close the window if open
        Debug.Log("Shift ended");

        if (startShiftButton != null)
        {
            startShiftButton.interactable = true; //re-enable the start button for next shift
        }

        SetDecisionButtons(false); //disable decision buttons since shift is over
        timerText.gameObject.SetActive(false); //hide timer/UI
    }

    //update the timer UI to show in game time
    //coded with help from "How to Make a Timer in Unity" by mixedkreations.com
    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            float progress = 1f - (shiftTimer / shiftDuration);

            float gameHour = shiftStartHour + progress * (shiftEndHour - shiftStartHour);
            int displayHour = Mathf.FloorToInt(gameHour);
            int displayMinute = Mathf.FloorToInt((gameHour - displayHour) * 60);


            timerText.text = $"{displayHour:00}:{displayMinute:00} AM";
        }
    }


    //returns the bool if shfit is currently active
    public bool IsShiftActive()
    {
        return shiftActive;
    }

    //called when the player makes a choice in a case
    public void PlayerChose(PestGenerator.PestStatus playerChoice)
    {
        DecisionManager.Instance.EvaluateDecision(playerChoice); //send to the decision manaeger
    }

    //enable or disable all decision buttons logic 
    public void SetDecisionButtons(bool state)
    {
        foreach (Button btn in decisionButtons)
        {
            btn.interactable = state; //each buttons interactable state
        }
    }
}
