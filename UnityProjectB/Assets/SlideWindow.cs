using UnityEngine;

public class SlideWindow : MonoBehaviour
{
    public float slideHeight = 2f;
    public float slideSpeed = 2f;
    public bool canOpen = false;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpening = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.up * slideHeight;
        isOpening = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.E))
        {
            isOpening = true;
        } 
        if (isOpening && canOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, slideSpeed * Time.deltaTime);
        }
    }

    public void CloseWindow()
    {
        isOpening = false;
        transform.position = closedPosition;
    }
}
