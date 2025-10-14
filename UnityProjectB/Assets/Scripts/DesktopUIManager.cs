using UnityEngine;

//this script manages the desktop UI by opening the specific panels within the canvas (xray, blood work, sound, info
//This ensures whichever panel is selected it is brought to the front no matter the scene heirarchy using "SetAsLastSibling"

public class DesktopUIManager : MonoBehaviour
{
    public GameObject panelXray;
    public GameObject panelBlood;
    public GameObject panelSound;
    public GameObject panelInfo;


    public void OpenXrayPanel()
    {
        panelXray.SetActive(true);
        panelXray.transform.SetAsLastSibling();
    }

    public void OpenBloodPanel()
    {
        panelBlood.SetActive(true);
        panelBlood.transform.SetAsLastSibling();
    }

    public void OpenSoundPanel()
    {
        panelSound.SetActive(true);
        panelSound.transform.SetAsLastSibling();
    }

    public void OpenInfoPanel()
    {
        panelInfo.SetActive(true);
        panelInfo.transform.SetAsLastSibling();
    }

    //function is used by a close button in the scene
    public void CloseCurrentPanel(GameObject panelToClose)
    {
        panelToClose.SetActive(false);
    }
}
