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

    public enum menuOptions
    {
        Inventory, 
        Equipment,
        Power,
        Configuration,
    }

    public menuOptions CurrentMenuOption => (menuOptions)currentMenu;
    public MenuUI[] menuElements; 

    [Header("Player Elements")]
    public GameObject player;
    public Animator playerAnimator;
    private Movement playerMovement;

    [Header("Animators Elements")]
    public Animator menuAnimator;
    public Animator menuInfoAnimator;

    private int currentMenu = 0;
    private menuOptions previousMenu;

    void Start()
    {
        playerMovement = player.GetComponent<Movement>();

        foreach (var menu in menuElements)
        {
            if (menu.panel != null)
                menu.panel.anchoredPosition = menu.hiddenPosition;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !GameStateManager.Instance.inSubMenu)
        {
            ToggleMenu(0);
            ToggleMenu(1);
            ToggleMenu(2);
        }

        foreach (var menu in menuElements)
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

        GameStateManager.Instance.inMenu = false;
        foreach (var menu in menuElements)
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
            GameStateManager.Instance.CanPlayerMove = false;
            playerAnimator.SetFloat("moveX", playerMovement.LastDirection.x);
            playerAnimator.SetFloat("moveY", playerMovement.LastDirection.y);
            playerAnimator.SetBool("isMoving", false);
        }

        // Handle menu navigation

        currentMenu = Mathf.Clamp(currentMenu, 0, 3);
        menuOptions currentOption = (menuOptions)currentMenu;
        
        if (GameStateManager.Instance.inMenu)
        {
           if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentMenu++;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentMenu--;
            }
        }

        if (!GameStateManager.Instance.inMenu)
        {
            currentMenu = 0;
        }

        switch (currentOption)
        {
            case menuOptions.Inventory:

                if (currentOption != previousMenu)
                {
                    if (GameStateManager.Instance.inMenu && !GameStateManager.Instance.inSubMenu)
                    {
                        menuAnimator.SetInteger("currentButton", 0);
                        menuInfoAnimator.SetInteger("currentInfo", 0);
                        SoundManager.PlaySound(SoundType.MENU_MOVE, 0.5f);
                        previousMenu = currentOption;
                    }
                }
                break;

            case menuOptions.Equipment:

                if (currentOption != previousMenu)
                {
                    if (GameStateManager.Instance.inMenu && !GameStateManager.Instance.inSubMenu)
                    {
                        menuAnimator.SetInteger("currentButton", 1);
                        menuInfoAnimator.SetInteger("currentInfo", 1);
                        SoundManager.PlaySound(SoundType.MENU_MOVE, 0.5f);
                        previousMenu = currentOption;
                    }
                }
                break;

            case menuOptions.Power:

                if (currentOption != previousMenu)
                {
                    if (GameStateManager.Instance.inMenu && !GameStateManager.Instance.inSubMenu)
                    {
                        menuAnimator.SetInteger("currentButton", 2);
                        menuInfoAnimator.SetInteger("currentInfo", 2);
                        SoundManager.PlaySound(SoundType.MENU_MOVE, 0.5f);
                        previousMenu = currentOption;
                    }
                }
                break;


            case menuOptions.Configuration:

                if (currentOption != previousMenu)
                {
                    if (GameStateManager.Instance.inMenu && !GameStateManager.Instance.inSubMenu)
                    {
                        menuAnimator.SetInteger("currentButton", 3);
                        menuInfoAnimator.SetInteger("currentInfo", 3);
                        SoundManager.PlaySound(SoundType.MENU_MOVE, 0.5f);
                        previousMenu = currentOption;
                    }
                }
                break;

        }
        

    }

    public void ToggleMenu(int index)
    {
        if (index >= 0 && index < menuElements.Length)
        {
            menuElements[index].isOpen = !menuElements[index].isOpen;
        }
    }
}