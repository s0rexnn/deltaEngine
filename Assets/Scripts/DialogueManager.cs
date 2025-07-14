using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class DialougeLine
{
    [TextArea] public string text;
    AudioClip voiceClip;

}


public class DialogueManager : MonoBehaviour
{
    public DialougeLine[] lines;

    private Movement playerMovement;
    public Animator playerAnimator;
    public GameObject player;

    [Header("UI Elements")]

    public TMP_Text dialogueText;
    public Sprite portraitImage;

    [Header("Time parameter")]

    public float typingSpeed = 0.05f;

    [Header("Sound")]

    [SerializeField] private AudioClip typingSound;

    private int currentLineIndex = 0;
    private bool isTyping = false;
    private bool isTryingToSkip = false;
    private bool isPlayerInRange = false;

    void Start()
    {
        playerMovement = player.GetComponent<Movement>();
    }

    void Update()
    {
        if (!isPlayerInRange) return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isTyping)
            {
                isTryingToSkip = true;
            }
            else
            {
                if (!GameStateManager.Instance.inDialogue)
                {
                    StartDialouge();

                    playerMovement.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                    playerAnimator.SetFloat("moveX", playerMovement.LastDirection.x);
                    playerAnimator.SetFloat("moveY", playerMovement.LastDirection.y);
                    playerAnimator.SetBool("isMoving", false);


                }
                else
                {
                    DisplayNextLine();

                }
            }
        }
    }

    public void StartDialouge()
    {
        GameStateManager.Instance.inDialogue = true;;
        currentLineIndex = 0;
        DisplayNextLine();
    }

    void DisplayNextLine()
    {
        if (currentLineIndex >= lines.Length)
        {
            EndDialouge();
            return;
        }
        DialougeLine Line = lines[currentLineIndex];

        StartCoroutine(TypeText(Line.text, typingSound));
        currentLineIndex++;
    }

    IEnumerator TypeText(string line, AudioClip playsound)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            if (isTryingToSkip) break;

            dialogueText.text += letter;

            if (playsound != null)
                SoundManager.PlayCustomSound(playsound, 0.3f);

            yield return new WaitForSeconds(typingSpeed);

        }

        dialogueText.text = line;
        isTryingToSkip = false;
        isTyping = false;
    }


    void EndDialouge()
    {
        GameStateManager.Instance.inDialogue = false;
        currentLineIndex = 0;
        dialogueText.text = "";
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

}

