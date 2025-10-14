using UnityEngine;

//simple script to manage switching between the main computer UI and the internal desktop computer UI
//this is done by enabling and diabling the respective canvases


public class ComputerUIManager : MonoBehaviour
{

    public GameObject mainComputerCanvas;
    public GameObject desktopCanvas;

   public void OpenDesktop ()
    {
        mainComputerCanvas.SetActive (false);
        desktopCanvas.SetActive (true);
    }

    public void ExitDesktop()
    {
        desktopCanvas.SetActive(false);
        mainComputerCanvas.SetActive(true);
    }
}
