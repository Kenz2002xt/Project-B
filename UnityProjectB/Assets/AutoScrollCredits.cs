using UnityEngine;

//script copied over from Project A

public class AutoScrollCredits : MonoBehaviour
{
    public RectTransform creditsPanel; //where the credits are
    public float scrollSpeed = 7f; //scroll speed

    // Update is called once per frame
    void Update()
    {
        if (creditsPanel != null)
        {
            creditsPanel.anchoredPosition = creditsPanel.anchoredPosition + Vector2.up * scrollSpeed * Time.deltaTime; //auto scrolling logic
        }
    }
}