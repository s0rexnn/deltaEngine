using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [System.Serializable]
    public class MenuUI
    {
        public string name;
        public RectTransform panel;    
        public Vector2 hiddenPosition;  
        public Vector2 shownPosition;   
        public float slideSpeed = 2f;

        [HideInInspector]
        public bool isOpen = false;
    }

    public MenuUI[] menus; // Assign multiple menus in the Inspector
    public GameObject player;
    public Animator playerAnimator;
    private Movement playerMovement;

    void Start()
    {
        playerMovement = player.GetComponent<Movement>();

        // Move all menus to hidden positions at start
        foreach (var menu in menus)
        {
            if (menu.panel != null)
                menu.panel.anchoredPosition = menu.hiddenPosition;
        }
    }

    void Update()
    {
   
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleMenu(0);
            ToggleMenu(1);
            SoundManager.PlaySound(SoundType.MENU_CLICK, 0.5f);
        }
 
        // Move the menus toward their target positions
        foreach (var menu in menus)
        {
            if (menu.panel != null)
            {
                Vector2 target = menu.isOpen ? menu.shownPosition : menu.hiddenPosition;
                menu.panel.anchoredPosition = Vector2.Lerp(
                    menu.panel.anchoredPosition,
                    target,
                    menu.slideSpeed
                );
            }
        }

        // Disable player movement if any menu is open

        GameStateManager.Instance.inMenu = false;
        foreach (var menu in menus)
        {
            if (menu.isOpen)
            {
                GameStateManager.Instance.inMenu = true;
                break;
            }
        }

        playerMovement.canMove = !GameStateManager.Instance.inMenu;

        if (GameStateManager.Instance.inMenu)
        {
            playerMovement.canMove = false;
            player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            playerAnimator.SetFloat("moveX", playerMovement.LastDirection.x);
            playerAnimator.SetFloat("moveY", playerMovement.LastDirection.y);
            playerAnimator.SetBool("isMoving", false);
        }
    }

    public void ToggleMenu(int index)
    {
        if (index >= 0 && index < menus.Length)
        {
            menus[index].isOpen = !menus[index].isOpen;
        }
    }
}