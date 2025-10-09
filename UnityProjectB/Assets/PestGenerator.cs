using NUnit.Framework.Interfaces;
using UnityEngine;

public class PestGenerator : MonoBehaviour
{
    public PestUIManager pestUIManager;

    public Sprite[] pestTypeImages;
    public string[] pestTypeNames;

    public Sprite[] normalXrays;
    public Sprite[] mutatedXrays;

    public string[] normalBloodReports;
    public string[] mutatedBloodReports;

    public AudioClip[] normalSounds;
    public AudioClip[] mutatedSounds;

    public float clearChance = 0.4f;
    public float reportChance = 0.2f;
    public float incinerateChance = 0.4f;

    public enum PestStatus { Clear, Report, Incinerate}

    public class Pest
    {
        public Sprite PestTypeImage;
        public string PestTypeName;
        public Sprite XRayImage;
        public string BloodReport;
        public AudioClip SoundClip;
        public PestStatus Status;

        public Pest(Sprite pestType, string name, Sprite xray, string blood, AudioClip sound, PestStatus status)
        {
            PestTypeImage = pestType;
            PestTypeName = name;
            XRayImage = xray;
            BloodReport = blood;
            SoundClip = sound;
            Status = status;
        }
    }

    public Pest GeneratePest()
    {
        float roll = Random.value;
        PestStatus status;
        int mutationCount;

        if (roll < clearChance)
        { 
            status = PestStatus.Clear;
            mutationCount = Random.Range(0, 2);
        }
        else if (roll < clearChance + reportChance)
        { 
            status = PestStatus.Report;
            mutationCount = 2;
        }
        else 
        {
            status = PestStatus.Incinerate; 
            mutationCount = 3;
        }


        bool[] diagnostics = new bool[3];
        for (int i = 0; i < mutationCount; i++)
        {
            int index;
            do { index = Random.Range(0, 3); }
            while (diagnostics[index]);
            diagnostics[index] = true;
        }

        Sprite chosenXray = diagnostics[0]
            ? mutatedXrays[Random.Range(0, mutatedXrays.Length)]
            : normalXrays[Random.Range(0, normalXrays.Length)];

        string chosenBlood = diagnostics[1]
            ? mutatedBloodReports[Random.Range(0, mutatedBloodReports.Length)]
            : normalBloodReports[Random.Range(0, normalBloodReports.Length)];

        AudioClip chosenSound = diagnostics[2]
            ? mutatedSounds[Random.Range(0, mutatedSounds.Length)]
            : normalSounds[Random.Range(0, normalSounds.Length)];

        int pestIndex = Random.Range(0, pestTypeImages.Length);
        Sprite chosenTypeImage = pestTypeImages[pestIndex];
        string chosenTypeName = pestTypeNames[pestIndex];

        Pest generatedPest = new Pest(chosenTypeImage, chosenTypeName, chosenXray, chosenBlood, chosenSound, status);

        if (pestUIManager != null)
        {
            pestUIManager.DisplayPest(generatedPest);
        }
        else
        {
            Debug.LogWarning("Pest ui manager missing");
        }

        return generatedPest;
    }
}
