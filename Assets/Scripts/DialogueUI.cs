using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public Vector2 hiddenPosition;
    public Vector2 shownPosition;

     void Update()
    {
        if (GameStateManager.Instance.inDialogue)
        {
             GetComponent<RectTransform>().anchoredPosition = shownPosition;
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = hiddenPosition;
        }
    }
}
