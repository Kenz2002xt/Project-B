using UnityEngine;

public class CameraController : MonoBehaviour
{
    public SlideWindow windowScript;
    public ShiftManager shiftManager;

    //transforms for the camera positions
    public Transform forwardView;
    public Transform leftView;
    public Transform rightView;

    //how fast the camera is moving into positions
    public float lookSpeed = 5f;

    //the target transform the camera is moving towards
    private Transform targetView;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetView = forwardView;
        MoveToView(forwardView);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            targetView = forwardView;
            windowScript.canOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            targetView = leftView;
            windowScript.canOpen = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetView = rightView;

            if (shiftManager != null && shiftManager.IsShiftActive())
            {
                windowScript.canOpen = true;
            }
            else
            {
                windowScript.canOpen = false;
            }
        }


        transform.position = Vector3.Lerp(transform.position, targetView.position, Time.deltaTime * lookSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetView.rotation, Time.deltaTime * lookSpeed);
    }

    private void MoveToView(Transform view)
    {
        transform.position = view.position;
        transform.rotation = view.rotation;
    }
}
