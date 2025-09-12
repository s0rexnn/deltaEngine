using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public bool CanPlayerMove;
    public bool inDialogue = false;
    public bool inMenu = false;
    public bool inSubMenu = false;
    
    private Movement playerMovement;
    private Animator playeranimator;

    private void Awake()
    {
        inSubMenu = false;
        Instance = this;
    }

    private void Start()
    {
        playerMovement = FindFirstObjectByType<Movement>();
        playeranimator = playerMovement.GetComponentInChildren<Animator>();

    }

    void Update()
    {
        if (GameStateManager.Instance.CanPlayerMove == false)
        {
            playerMovement.canMove = false;
            playerMovement.isMoving = false;
        }

        if (GameStateManager.Instance.inDialogue || GameStateManager.Instance.inMenu)
        {
            CanPlayerMove = false;
            playerMovement.isMoving = false;
        }
        else
        {
            CanPlayerMove = true;
        }

        if (GameStateManager.Instance.inDialogue || GameStateManager.Instance.inMenu)
        {
            playerMovement.runningSpeed = playerMovement.walkingSpeed;
        }
    }
  }

