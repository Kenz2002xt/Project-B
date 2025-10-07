using UnityEngine;

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

    public void CloseCurrentPanel(GameObject panelToClose)
    {
        panelToClose.SetActive(false);
    }
}
