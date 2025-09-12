using UnityEngine;
using Ink.Runtime;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Parameters")]
    [SerializeField] private float textSpeed = 0.02f;
     
    [Header("UI Elements")]
    [SerializeField] private DialoguePanelUI dialoguePanelUI;
    private Story story;

    private bool dialoguePlaying = false;
    private bool canContinueToNextLine = false;
    private bool isTyping = false;
    private Coroutine displayLineCoroutine;



    private void Awake()
    {
        story = new Story(inkJSON.text);
        dialoguePanelUI.dialogueText.richText = true;
    }

    private void OnEnable()
    {
        GamesEventsManager.Instance.dialogueEvents.OnEnterDialogue += EnterDialogue;
    }
    private void OnDisable()
    {
        GamesEventsManager.Instance.dialogueEvents.OnEnterDialogue -= EnterDialogue;
    }

    private void SumbitPressed()
    {
        if (dialoguePlaying && canContinueToNextLine)
        {
            ContinueOrExitDialogue();
        }
        else return;
    }

    private void Update()
    {
        if (dialoguePlaying && isTyping && Input.GetKeyDown(KeyCode.R))
        {
            NextLine();
        }
        else if (dialoguePlaying && Input.GetKeyDown(KeyCode.R))
        {
            SumbitPressed();
        }
    }

    private void EnterDialogue(string knotName)
    {
        if (dialoguePlaying) return;

        dialoguePlaying = true;

        GamesEventsManager.Instance.dialogueEvents.DialougeStarted();

        if (!knotName.Equals(""))
        {
            story.ChoosePathString(knotName);
            Debug.Log("Knot found: " + knotName);
        }
        else
        {
            Debug.Log("Knot not found, starting from beginning.");
        }

        ContinueOrExitDialogue();
    }

    private void NextLine()
    {
        Debug.Log("Executed");
        StopAllCoroutines();
        dialoguePanelUI.dialogueText.text = story.currentText;
        canContinueToNextLine = true;
        isTyping = false;
    }
 
    private IEnumerator DisplayLine(string line)
    {
        bool isAddingRichText = false;
        dialoguePanelUI.dialogueText.text = "";
        canContinueToNextLine = false;
        isTyping = true;

        foreach (char letter in line.ToCharArray())
        {
            if (letter == '<' || isAddingRichText)
            {
                isAddingRichText = true;
                dialoguePanelUI.dialogueText.text += letter;
                if (letter == '>')
                {
                    isAddingRichText = false;
                }
            }
            else
            {
                dialoguePanelUI.dialogueText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }
        }
        canContinueToNextLine = true;
        isTyping = false;
    }

    private void ContinueOrExitDialogue()
{
    if (story.canContinue)
    {
        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
        }

        string dialogueLine = story.Continue();
        if (dialogueLine != null)
        {
           displayLineCoroutine = StartCoroutine(DisplayLine(dialogueLine)); 
        }
    }
    else
    {
        ExitDialogue();
    }
}

    private void ExitDialogue()
    {
        dialoguePlaying = false;
        Debug.Log("End of conversation.");
        story.ResetState();
        GamesEventsManager.Instance.dialogueEvents.DialougeEnded();
    }

    public bool IsDialoguePlaying()
{
    return dialoguePlaying;
}
}
