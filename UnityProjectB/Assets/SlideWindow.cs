using UnityEngine;

//Script made using help from Unity Documentation "Transform.position"

public class SlideWindow : MonoBehaviour
{
    public float slideHeight = 2f; //how high the window will slide open
    public float slideSpeed = 2f; //how fast the window moves when sliding 
    public bool canOpen = false; //used to check if the window is allowed to open

    private Vector3 closedPosition; //will store the windows original closed position
    private Vector3 openPosition; //will store the windows target open position
    public bool isOpening = false; //used to check if the window is currently opening


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedPosition = transform.position; //will set the initial position as the closed position
        openPosition = closedPosition + Vector3.up * slideHeight; //sets and calculates the open posiiton by moving upward
        isOpening = false; //makes sure the window starts closed
    }

    // Update is called once per frame
    void Update()
    {
        //if the window can open and the player is pressing E, the start opening
        if (canOpen && Input.GetKeyDown(KeyCode.E))
        {
            isOpening = true;
        } 
        //if the window is opening and still allowed to open then move
        if (isOpening)
        {
            //move the window towards the open posiiton at the set speed
            transform.position = Vector3.MoveTowards(transform.position, openPosition, slideSpeed * Time.deltaTime);

            //once it reaches target positon, stop moving and lock open
            if (Vector3.Distance(transform.position, openPosition) <0.01f)
            {
                isOpening = false;
                transform.position = openPosition; //smap to exact position
            }
        }
    }

    public void CloseWindow()
    {
        isOpening = false; //stop the movement and return to the closed position
        transform.position = closedPosition; //moves window to closed position
        canOpen = false; //disable until NPCSpawner enables
    }
}
