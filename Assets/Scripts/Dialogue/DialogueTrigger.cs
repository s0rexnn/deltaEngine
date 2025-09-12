using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private string knotName;

    private bool playerInRange = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GamesEventsManager.Instance != null)
            {
                playerInRange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void FixedUpdate()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Z) && !DialogueManagerIsPlaying())
        {
            GamesEventsManager.Instance.dialogueEvents.EnterDialogue(knotName);
            return;
        }
        else if (DialogueManagerIsPlaying())
        {
            return;
        }
    }

    private bool DialogueManagerIsPlaying()
    {
        var manager = FindFirstObjectByType<DialogueManager>();
        return manager != null && manager.IsDialoguePlaying();
    }
}

