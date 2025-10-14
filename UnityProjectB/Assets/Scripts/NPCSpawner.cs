using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Procedural Generation: NPCSpawner handles the procedural generation of NPC arrivals during a shift
//each NPC "knocks" at the window after a random delay, displays random dialouge, and cost

//script made with reference to "Procedural-Character-Generation-in-Unity" by NayabSyed05 on GitHub
//script made with reference to "Coroutines" by Unity Learn Tutorials


public class NPCSpawner : MonoBehaviour
{
    public SlideWindow windowScript;
    public ShiftManager shiftManager;
    public AudioSource knockSound;
    public PestGenerator pestSpawner;

    public GameObject dialougePanel;
    public TMP_Text dialougeText;
    public TMP_Text costText;
    public int currentCost;
    public Button acceptButton;
    public Button denyButton;

    public Image npcImage; //reference to the npc image
    public Sprite[] npcSprites; //array of possible NPC sprites

    public bool npcWaiting = false; //true if npc is waiting at window
    private bool npcSpawned = false; //true if npc is currently spawned
    private bool canSpawnNextNPC = true; //prevents the npc from spawning too quickly
    public bool caseInProgress = false; //tracks if a case is going on while the window is open


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialougePanel.SetActive(false); //initially hide the dialouge panel

        acceptButton.onClick.AddListener(OnAccept);
        denyButton.onClick.AddListener(OnDeny);
    }

    // Update is called once per frame
    void Update()
    {
        if (!shiftManager.IsShiftActive()) return; //skip if the shift is NOT active 

        //if npc is waiting, window is open, and the npc hasn't spawned yet, then show the npc
        if (windowScript != null && npcWaiting && !npcSpawned && IsWindowOpen())
        {
            ShowNPC();
        }

        //if no npc is waitng, no npc is spawned, no case is in progress, the spawner can queue new NPC, and window isnt open then run
        if (!npcWaiting && !npcSpawned && !caseInProgress && canSpawnNextNPC && !IsWindowOpen())
        {
            StartCoroutine(SpawnNextNPCAfterDelay());
        }
    }

    //coroutine waits based on a randonly generated wait time, then signals an npc is waiting
    private IEnumerator SpawnNextNPCAfterDelay()
    {
        canSpawnNextNPC = false; //prevents multiple coroutines from running

        float delay = Random.Range(2f, 8f);
        yield return new WaitForSeconds(delay);

        //only spawn if shift is active and npc isnt already there
        if (shiftManager.IsShiftActive() && !npcSpawned)
        {
            npcWaiting = true; //npc at window
            windowScript.canOpen = true; //allow the sliding window to open
            Debug.Log("npc arrived and is knocking");
            knockSound?.Play(); //play knock sound
        }

        canSpawnNextNPC = true; //allow next npc to be queued
    }

    
    //show the npc dialogue panel with random text and cost
    private void ShowNPC()
    {
        npcSpawned = true; //npc is now spawned
        npcWaiting = false; //npc is no longer waiting
        caseInProgress = true;

        Debug.Log("NPC Spawned at window- add images later");
        dialougePanel.SetActive(true);
        dialougeText.text = GenerateRandomDialouge();
        costText.text = GenerateRandomCost();

        //assign a random npc image
        if (npcSprites != null && npcSprites.Length > 0 && npcImage != null)
        {
            npcImage.sprite = npcSprites[Random.Range(0, npcSprites.Length)];
        }
    }

    //called when the player accepts the NPC case- a pest will spawn and the dialouge panel with buttons is hidden
    private void OnAccept()
    {
        Debug.Log("npc accepted, triggering spawn pest");

        if (pestSpawner != null)
        {
            pestSpawner.GeneratePest();
            Debug.Log("pest successfully spawned");
        }
        else
        {
            Debug.LogWarning("No pest spawner found");
        }

        caseInProgress = true;
        dialougePanel.SetActive(false);
        npcSpawned = false; //mark the npc as no longer active
        windowScript.canOpen = false; //disable window until next npc
    }


    //called when the player denies an NPC case- closes the window and sets npc as no longer active
    private void OnDeny()
    {
        Debug.Log("npc denied window closing");
        dialougePanel.SetActive(false);
        npcSpawned = false;

        caseInProgress=false;
        windowScript.CloseWindow();
        windowScript.canOpen = false; //disable window until next npc
    }

    private bool IsWindowOpen()
    {
        return windowScript.isOpening;
    }

    private string GenerateRandomDialouge()
    {
        string[] dialouges = new string[]
        {
            "I heard some scratching in my house and found this...",
            "Morning. I just need this cleared so I can call my boss.",
            "I was told to bring this to you",
            "Can you take a look at this?",
            "This pest facility is stinking up the town, take this",
            "I'd appreciate if you could take this off my hands",
            "All I hear all night is movement in the walls",
            "I just got this thing, been actin' weird though",
            "This thing ruined my garden, might be one a them weird mutants",
            "All you people do is harm these poor animals. Just take it",
            "Do I need an appointment, or is this the right window?",
            "They said you decide what happens next...is that true?",
            "Man, I'd hate to have your job- take this thing would you",
            "I brought everything, just like they told me I swear",
            "I don't want to talk. Just clear it and let me go",
            "It's not dangerous it's just an animal. You'll clear it right?",
            "If you burn it, make sure it feels nothing. Please",
            "This thing ain't right",
            "I've done this a dozen times. Just take it",
            "You're new here aren't you? Ha, wonder what happened to the last one",
            "I was told this wouldn't take long",
            "You inspectors just like the power, don't you?",
            "Sometimes it breathes. Sometimes it doesn't",
            "Tell your boss to stop pumping smoke into our town",
            "Smells like death in here...no offense"
        };

        return dialouges[Random.Range(0, dialouges.Length)]; //pick a random string
    }

    private string GenerateRandomCost() //generate random cost for NPC case
    {
        currentCost = Random.Range(10, 71);
        return "$" + currentCost.ToString();
    }

    //resets the state when starting as new shift to avoid bugs
    public void ResetNPCState()
    {
        npcWaiting = false;
        npcSpawned = false;
        canSpawnNextNPC = true;
        caseInProgress = false;

        if (knockSound != null && knockSound.isPlaying)
            knockSound.Stop();

        if (dialougePanel != null)
            dialougePanel.SetActive(false);
    }
}
