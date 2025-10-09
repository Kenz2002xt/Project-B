using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCSpawner : MonoBehaviour
{
    public SlideWindow windowScript;
    public ShiftManager shiftManager;
    public AudioSource knockSound;
    public PestGenerator pestSpawner;

    public GameObject dialougePanel;
    public TMP_Text dialougeText;
    public Button acceptButton;
    public Button denyButton;

    private bool npcWaiting = false;
    private bool npcSpawned = false;
    private bool canSpawnNextNPC = true;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialougePanel.SetActive(false);

        acceptButton.onClick.AddListener(OnAccept);
        denyButton.onClick.AddListener(OnDeny);
    }

    // Update is called once per frame
    void Update()
    {
        if (!shiftManager.IsShiftActive()) return;

        if (windowScript != null && npcWaiting && !npcSpawned && IsWindowOpen())
        {
            ShowNPC();
        }

        if (!IsWindowOpen() && !npcWaiting && canSpawnNextNPC)
        {
            StartCoroutine(SpawnNextNPCAfterDelay());
        }
    }

    private IEnumerator SpawnNextNPCAfterDelay()
    {
        canSpawnNextNPC = false;
        yield return new WaitForSeconds(2f);

        if (shiftManager.IsShiftActive() && !IsWindowOpen())
        {
            npcWaiting = true;
            Debug.Log("npc arrived and is knocking");
            knockSound?.Play();
        }

        canSpawnNextNPC = true;
    }

    private void ShowNPC()
    {
        npcSpawned = true;
        npcWaiting = false;

        Debug.Log("NPC Spawned at window- add images later");
        dialougePanel.SetActive(true);
        dialougeText.text = GenerateRandomDialouge();
    }

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

        dialougePanel.SetActive(false);
        npcSpawned = false;
    }

    private void OnDeny()
    {
        Debug.Log("npc denied window closing");
        dialougePanel.SetActive(false);
        npcSpawned = false;

        windowScript.CloseWindow();
    }

    private bool IsWindowOpen()
    {
        return windowScript.isOpening;
    }

    private string GenerateRandomDialouge()
    {
        string[] dialouges = new string[]
        {
            "I heard some scratching in the vents and found this...",
            "He used to be my sisters pet, but he's been acting strange",
            "I was told to bring this to you",
            "Can you take a look at this?",
            "This pest facility is stinking up the town, take this",
            "I'd appreciate if you could take this off my hands",
            "All I hear all night is movement in the walls",
            "I just got this thing, been actin' weird though",
            "This thing ruined my garden, might be one a them weird mutants",
            "All you people do is harm these poor animals. Just take it"
        };

        return dialouges[Random.Range(0, dialouges.Length)];
    }
}
