using UnityEngine;

//script made with information from "Unity- First-Person: Forced Look at Object/Target" by VoltageDev on Youtube
//script made with information from "How to Use Lerp (Unity Tutorial" by Ketra Games on Youtube


public class CameraController : MonoBehaviour
{
    public SlideWindow windowScript; //reference to the script that controls the window
    public ShiftManager shiftManager; //checks if the shift has started in shift manager 
    public NPCSpawner npcSpawner; //checks to see if an npc has spawned

    //transforms for the camera positions
    public Transform forwardView;
    public Transform leftView;
    public Transform rightView;

    //how fast the camera is moving into positions
    public float lookSpeed = 5f;

    //the target transform the camera is moving towards
    private Transform targetView;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //sets the target view on start to forward and moves the camera to the start target
    void Start()
    {
        targetView = forwardView;
        MoveToView(forwardView);
    }

    // Update is called once per frame
    void Update()
    {
        //when pressing W, the target view is set to forward, and the player cannot press E to open the window
        if (Input.GetKeyDown(KeyCode.W))
        {
            targetView = forwardView;
            windowScript.canOpen = false;
        }
        //when pressing A, the player looks left at the decision buttons and cannot open the window
        else if (Input.GetKeyDown(KeyCode.A))
        {
            targetView = leftView;
            windowScript.canOpen = false;
        }
        //when pressing D, the player looks right at the window.
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetView = rightView;
            //if the shift is active and npc arrived then the player can open the window when looking at it, if not- then the window can't open
            if (shiftManager != null && shiftManager.IsShiftActive() && npcSpawner.npcWaiting)
            {
                windowScript.canOpen = true;
            }
            else
            {
                windowScript.canOpen = false;
            }
        }

        //moves the camera smoothly to the target view every frame
        transform.position = Vector3.Lerp(transform.position, targetView.position, Time.deltaTime * lookSpeed);
        //rotates the camera smoothly to the target view every frame
        transform.rotation = Quaternion.Slerp(transform.rotation, targetView.rotation, Time.deltaTime * lookSpeed);
    }

    //this helper function allows the camera to instantly snap to a view
    private void MoveToView(Transform view)
    {
        transform.position = view.position;
        transform.rotation = view.rotation;
    }
}
