using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Settings : MonoBehaviour
{
    [Header("General Settings")]
    bool isVisible = false;
    public bool inGame = false;
    public GameObject settingsMenu;
    public KeyCode toggleKeyCode = KeyCode.Escape;

    [Header("In-Game Menu")]
    public bool enableIngameMenu = true;
    public Toggler gameplayToggler;
    CursorLockMode previousCursorState = CursorLockMode.Confined;
    bool previousCursorVisibility = false;

    public void Start()
    {
        if(settingsMenu == null)
        {
            Debug.LogError("Settings menu is not assigned in the inspector.");
        }
        if(gameplayToggler == null)
        {
            Debug.LogError("Gameplay toggler is not assigned in the inspector.");
        }
    }

    public void Update()
    {
        // Checks if the settings menu key is pressed when the game is running
        if(enableIngameMenu && inGame && Input.GetKeyDown(toggleKeyCode)){
            ToggleSettingsMenu();
        }
    }

    public void ToggleSettingsMenu(){
        if(inGame){
            gameplayToggler.smartToggle(); // Toggles the gameplay actions when the settings menu is opened or closed
        }

        if(isVisible){
            Cursor.lockState = previousCursorState; // Restores the previous cursor state
            Cursor.visible = previousCursorVisibility; // Restores the previous cursor visibility state
            settingsMenu.SetActive(false);
            isVisible = false;
        }else{
            previousCursorState = Cursor.lockState;
            previousCursorVisibility = Cursor.visible; // Store the previous cursor visibility state
            Cursor.lockState = CursorLockMode.Confined; // Keeps the cursor within the game window
            Cursor.visible = true; // Makes the cursor visible
            settingsMenu.SetActive(true);
            isVisible = true;
        }
    }

    public void updateGameStatus(bool status){
        inGame = status;
    }
}
