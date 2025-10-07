using UnityEngine;

public class CameraController : MonoBehaviour
{
    public SlideWindow windowScript;
    public Transform forwardView;
    public Transform leftView;
    public Transform rightView;

    public float lookSpeed = 5f;

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
            windowScript.canOpen = true;
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
