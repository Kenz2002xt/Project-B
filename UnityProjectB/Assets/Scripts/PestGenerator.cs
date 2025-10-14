using NUnit.Framework.Interfaces;
using UnityEngine;

//Procedural Generation: This script uses procedural generation to create random pests
//Each pest is generated using random attributes (xray, blood, sound, type)
//This ensures a unique combination every time, rather than each pest being manually created


//script made with reference to "Procedural-Character-Generation-in-Unity" by NayabSyed05 on GitHub

public class PestGenerator : MonoBehaviour
{
    public PestUIManager pestUIManager; //reference to the pest UI manager for displaying of attributes

    //array for storing pest types
    public Sprite[] pestTypeImages; //possible pest images
    public string[] pestTypeNames; //possible pest names

    //normal and mutated diagnostic visuals
    public Sprite[] normalXrays;
    public Sprite[] mutatedXrays;

    public string[] normalBloodReports;
    public string[] mutatedBloodReports;

    public AudioClip[] normalSounds;
    public AudioClip[] mutatedSounds;

    //chance perentages for mutation level of pest
    public float clearChance = 0.3f;
    public float reportChance = 0.2f;
    public float incinerateChance = 0.5f;

    //enum defining possible pest statuses- this variable only has these specific options- cleaner than using strings
    public enum PestStatus { Clear, Report, Incinerate}

    //inner class to hold generated pest data
    public class Pest
    {
        public Sprite PestTypeImage;
        public string PestTypeName;
        public Sprite XRayImage;
        public string BloodReport;
        public AudioClip SoundClip;
        public PestStatus Status;

        //this is the constructor that sets the pest info
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


    //main method for generating random pest
    public Pest GeneratePest()
    {
        float roll = Random.value; //random value will determine mutation status
        PestStatus status;
        int mutationCount;

        //decides pest status based on the chance roll
        if (roll < clearChance)
        { 
            status = PestStatus.Clear;
            mutationCount = 0; //0 mutations
        }
        else if (roll < clearChance + reportChance)
        { 
            status = PestStatus.Report;
            mutationCount = Random.Range(1, 3); //1-2 mutations
        }
        else 
        {
            status = PestStatus.Incinerate; 
            mutationCount = 3; //all 3 mutations
        }

        //tracks which diagnostic elements are mutated
        //creates 3 slots (xray, blood, sound)
        //randomly will pick which ones are mutated
        bool[] diagnostics = new bool[3];
        for (int i = 0; i < mutationCount; i++)
        {
            int index;
            //makes sure each mutation spot is unique 
            do { index = Random.Range(0, 3); }
            while (diagnostics[index]);
            diagnostics[index] = true;
        }

        //ternary operators used for cleanliness instead of if/else
        //pick random xray (mutated or normal)
        Sprite chosenXray = diagnostics[0]
            ? mutatedXrays[Random.Range(0, mutatedXrays.Length)]
            : normalXrays[Random.Range(0, normalXrays.Length)];

        //pick random blood report (mutated or normal)
        string chosenBlood = diagnostics[1]
            ? mutatedBloodReports[Random.Range(0, mutatedBloodReports.Length)]
            : normalBloodReports[Random.Range(0, normalBloodReports.Length)];

        //pick random sound (mutated or normal)
        AudioClip chosenSound = diagnostics[2]
            ? mutatedSounds[Random.Range(0, mutatedSounds.Length)]
            : normalSounds[Random.Range(0, normalSounds.Length)];

        //choose random pest type (image and and name)
        int pestIndex = Random.Range(0, pestTypeImages.Length);
        Sprite chosenTypeImage = pestTypeImages[pestIndex];
        string chosenTypeName = pestTypeNames[pestIndex];


        //create a new pest object will all of the randomized data
        Pest generatedPest = new Pest(chosenTypeImage, chosenTypeName, chosenXray, chosenBlood, chosenSound, status);


        //update the pest UI
        if (pestUIManager != null)
        {
            pestUIManager.DisplayPest(generatedPest);
        }
        else
        {
            Debug.LogWarning("Pest ui manager missing");
        }

        //sends the pest to the decision manager for player choice comparison
        DecisionManager.Instance.SetCurrentPest(generatedPest);
        return generatedPest;
    }
}
