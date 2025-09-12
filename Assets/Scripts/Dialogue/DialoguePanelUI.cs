using UnityEngine;
using TMPro;

public class DialoguePanelUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] public TextMeshProUGUI dialogueText;

    private void Awake()
    {
        dialoguePanel.SetActive(false);
        ResetState();
    }

    private void OnEnable()
    {
        GamesEventsManager.Instance.dialogueEvents.OnDisplayLine += DisplayDialougeLine;
        GamesEventsManager.Instance.dialogueEvents.OnDialogueStarted += DialogueStarted;
        GamesEventsManager.Instance.dialogueEvents.OnDialogueEnded += DialogueEnded;
    }

    private void OnDisable()
    {
        GamesEventsManager.Instance.dialogueEvents.OnDisplayLine -= DisplayDialougeLine;
        GamesEventsManager.Instance.dialogueEvents.OnDialogueStarted -= DialogueStarted;
        GamesEventsManager.Instance.dialogueEvents.OnDialogueEnded -= DialogueEnded;
    }

    private void DialogueStarted()
    {
        dialoguePanel.SetActive(true);
    }

    private void DialogueEnded()
    {
        dialoguePanel.SetActive(false);
        ResetState();
    }

    private void DisplayDialougeLine(string line)
    {
        dialogueText.text = line;
    }
    
    private void ResetState()
    {
        dialogueText.text = "";
    }
}