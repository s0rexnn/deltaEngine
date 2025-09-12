using UnityEngine;

public class SubMenu : MonoBehaviour
{
    private MenuManager menuHandling;

    void Awake()
    {
        menuHandling = GetComponent<MenuManager>();

    }

    void Update()
    {
    if (GameStateManager.Instance.inMenu && Input.GetKey(KeyCode.Z))
    {
    switch (menuHandling.CurrentMenuOption)
    {
        case MenuManager.menuOptions.Inventory:
            Debug.Log("Inventory is selected!");
            GameStateManager.Instance.inSubMenu = true;
            break;

        case MenuManager.menuOptions.Equipment:
            Debug.Log("Equipment is selected!");
            GameStateManager.Instance.inSubMenu = true;
            break;

        case MenuManager.menuOptions.Power:
            Debug.Log("Power is selected!");
            GameStateManager.Instance.inSubMenu = true;
            break;

        case MenuManager.menuOptions.Configuration:
            Debug.Log("Configuration is selected!");
            GameStateManager.Instance.inSubMenu = true;
            break;
    }
}
                
    }
}
