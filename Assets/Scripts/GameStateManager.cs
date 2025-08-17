using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public NPC currentNPC;
    
    public bool inDialogue = false;
    public bool inMenu = false;
    public bool inSubMenu = false;
    public bool CanPlayerMove;
    public bool isRoomSwapped = false;
    

    private Movement playerMovement;
    private Animator playeranimator;

    private void Awake()
    {
        inSubMenu = false;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerMovement = FindFirstObjectByType<Movement>();
        playeranimator = playerMovement.GetComponentInChildren<Animator>();

    }

    void Update()
    {
        if (GameStateManager.Instance.inDialogue || GameStateManager.Instance.inMenu)
        {
            CanPlayerMove = false;
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

