using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PestUIManager : MonoBehaviour
{
    public DesktopUIManager desktopUIManager;

    public Image pestTypeImage;
    public TMP_Text pestNameText;
    public Image xrayImage;
    public TMP_Text bloodReportText;
    public AudioSource soundSource;
    public Button playSoundButton;

    private PestGenerator.Pest currentPest;


    public void DisplayPest(PestGenerator.Pest pest)
    {

        if (pestTypeImage != null) pestTypeImage.sprite = pest.PestTypeImage;
        if (pestNameText != null) pestNameText.text = pest.PestTypeName;
        if (xrayImage != null) xrayImage.sprite = pest.XRayImage;
        if (bloodReportText != null) bloodReportText.text = pest.BloodReport;
        if (soundSource != null) soundSource.clip = pest.SoundClip;

        Debug.Log($"Pest displayed: {pest.PestTypeName} ({pest.Status})");
    }

    public void PlayPestSound()
    {
        if (soundSource != null && soundSource.clip != null)
        {
            soundSource.Play();
        }
    }
}
