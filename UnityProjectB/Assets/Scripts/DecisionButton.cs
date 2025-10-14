using UnityEngine;

//This script simply sends the player's choice to the ShiftManager when a decision button is clicked

public class DecisionButton : MonoBehaviour
{
    public PestGenerator.PestStatus playerChoice;

    public void OnPlayerClick()
    {
        FindFirstObjectByType<ShiftManager>().PlayerChose(playerChoice);
    }
}
