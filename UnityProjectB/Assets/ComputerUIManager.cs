using UnityEngine;

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
