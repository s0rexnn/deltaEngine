using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("UI Elements")]
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public Image portraitImage;

    [Header("Player Elements")]
    public GameObject player;
    public Animator playerAnimator;
    private Movement playerMovement;

    private int currentLineIndex = 0;
    private bool isTyping, inActiveDialogue;
    private bool isPlayerInRange = false;

    private LinePosition linePosition;

    void Awake()
    {
        dialoguePanel.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        portraitImage.gameObject.SetActive(false);

        linePosition = FindFirstObjectByType<LinePosition>();
    }

    void Start()
    {
        if (playerMovement == null)
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isPlayerInRange)
        {
            if (!inActiveDialogue)
            {
                StartDialogue();
            }
            else
            {
                NextLine();
            }
        }

        if (GameStateManager.Instance.inDialogue)
        {
            GameStateManager.Instance.CanPlayerMove = false;
            playerAnimator.SetFloat("moveX", playerMovement.LastDirection.x);
            playerAnimator.SetFloat("moveY", playerMovement.LastDirection.y);
            playerAnimator.SetBool("isMoving", false);
        }
    }

    void StartDialogue()
    {
        GameStateManager.Instance.currentNPC = this;

        inActiveDialogue = true;
        currentLineIndex = 0;

        ShowPortraitForCurrentLine();

        dialoguePanel.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        portraitImage.gameObject.SetActive(true);
        GameStateManager.Instance.inDialogue = true;

        DialogueLine currentLine = dialogueData.lines[currentLineIndex];
        StartCoroutine(TypeLine(currentLine.text, currentLine.typingSound, currentLine.typingSpeed));
    }

    void ShowPortraitForCurrentLine()
    {
        Sprite linePortrait = dialogueData.lines[currentLineIndex].portrait;
        linePosition.hasPortrait = dialogueData.lines[currentLineIndex].portrait != null;

        if (linePortrait != null)
        {
            portraitImage.sprite = linePortrait;
            linePosition.hasPortrait = true;
        }
        else if (dialogueData.portrait != null)
        {
            portraitImage.sprite = dialogueData.portrait;
        }

        if (linePortrait == null)
        {
            linePosition.hasPortrait = false;
        }
    }

    IEnumerator TypeLine(string line, AudioClip typingSound, float typingSpeed)
    {
        isTyping = true;
        dialogueText.text = "";
         
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;

            if (letter == ',')
            {
                yield return new WaitForSeconds(0.3f);
            }

            if (typingSound != null)
                SoundManager.PlayCustomSound(typingSound, 0.3f);

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = dialogueData.lines[currentLineIndex].text;
            isTyping = false;
        }
        else if (++currentLineIndex < dialogueData.lines.Length)
        {
            ShowPortraitForCurrentLine();
            DialogueLine currentLine = dialogueData.lines[currentLineIndex];
            StartCoroutine(TypeLine(currentLine.text, currentLine.typingSound, currentLine.typingSpeed));
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        portraitImage.sprite = linePosition.emptyPortrait;
        inActiveDialogue = false;
        dialoguePanel.SetActive(false);
        portraitImage.gameObject.SetActive(false);
        GameStateManager.Instance.inDialogue = false;
        currentLineIndex = 0;
        dialogueText.text = "";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}