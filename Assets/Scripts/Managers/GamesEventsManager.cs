using UnityEngine;

public class GamesEventsManager : MonoBehaviour
{
    public static GamesEventsManager Instance { get; private set; }

    public DialogueEvents dialogueEvents;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        dialogueEvents = new DialogueEvents();
    }
}
