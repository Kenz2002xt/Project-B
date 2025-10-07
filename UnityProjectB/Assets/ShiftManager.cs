using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;

public class ShiftManager : MonoBehaviour
{
    public float shiftDuration = 300f;
    private float shiftTimer;
    private float shiftStartHour = 1f;
    private float shiftEndHour = 6f;
    private bool shiftActive = false;

    public TMP_Text timerText;
    public SlideWindow windowScript;
    public Button startShiftButton;

    // Update is called once per frame
    void Update()
    {
        if (shiftActive)
        {
            shiftTimer = shiftTimer - Time.deltaTime;
            if (shiftTimer <= 0)
            {
                EndShift();
            }
            UpdateTimerUI();
        }
    }

    public void StartShift()
    {

        shiftTimer = shiftDuration;
        shiftActive = true;
        windowScript.canOpen = true;
        Debug.Log("Shift Started!");

        if (startShiftButton != null)
        {
            startShiftButton.interactable = false;
        }

        StartCoroutine(CaseLoop());
    }

    private IEnumerator CaseLoop()
    {
        while (shiftActive)
        {
            SpawnCase();
            yield return new WaitForSeconds(5f);
        }
    }

    private void SpawnCase()
    {
        Debug.Log("NPC arrived new case started");
    }

    private void EndShift()
    {
        shiftActive = false;
        windowScript.canOpen = false;
        windowScript.CloseWindow();
        Debug.Log("Shift ended");

        if (startShiftButton != null)
        {
            startShiftButton.interactable = true;
        }
    }

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

    public bool IsShiftActive()
    {
        return shiftActive;
    }
}
