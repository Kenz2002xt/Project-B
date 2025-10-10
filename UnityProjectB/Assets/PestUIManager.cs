using TMPro;
using UnityEngine;
using UnityEngine.UI;

//simple script for updating UI's with generated pest data 

public class PestUIManager : MonoBehaviour //manages the UI display for pest info
{
    public DesktopUIManager desktopUIManager; //reference to the desktop UI where the info will be displayed

    //element types for pest info
    public Image pestTypeImage;
    public TMP_Text pestNameText;
    public Image xrayImage;
    public TMP_Text bloodReportText;
    public AudioSource soundSource;
    public Button playSoundButton;

    private PestGenerator.Pest currentPest; //stores the currently displayed pest data for reference in other scripts


    public void DisplayPest(PestGenerator.Pest pest) //updates the UI with generated pest data
    {
        //if each UI element is present, then update the UI information
        if (pestTypeImage != null) pestTypeImage.sprite = pest.PestTypeImage;
        if (pestNameText != null) pestNameText.text = pest.PestTypeName;
        if (xrayImage != null) xrayImage.sprite = pest.XRayImage;
        if (bloodReportText != null) bloodReportText.text = pest.BloodReport;
        if (soundSource != null) soundSource.clip = pest.SoundClip;

        Debug.Log($"Pest displayed: {pest.PestTypeName} ({pest.Status})"); //confirms the display in an easy to write interpolated string
    }

    //plays the currently assigned pest sound
    public void PlayPestSound()
    {
        //only play if audio source and clip are available 
        if (soundSource != null && soundSource.clip != null)
        {
            soundSource.Play();
        }
    }
}
