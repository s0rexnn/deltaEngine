using UnityEngine;

public class LinePosition : MonoBehaviour
{
    public RectTransform textTransform;
    public Vector2 hasPortraitPos;
    public Vector2 nonPortraitPos;
    public bool hasPortrait = false;
    public Sprite emptyPortrait;

    [Header("Text Box")]
    public RectTransform boxTransform;
    public Vector2 hiddenPos;
    public Vector2 shownPos;

    void Update()
    {
        if (hasPortrait)
        {
            textTransform.anchoredPosition = hasPortraitPos;
        }
        else
        {
            textTransform.anchoredPosition = nonPortraitPos;
        }

        if (GameStateManager.Instance.inDialogue)
        {
            boxTransform.anchoredPosition = shownPos;
        }
        else
        {
            boxTransform.anchoredPosition = hiddenPos;
        }
    }
}