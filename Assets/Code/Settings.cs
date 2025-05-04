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
        if(enableIngameMenu){
            HotKeyToggleSettingsMenu();
        }
    }

    public void ToggleSettingsMenu(){
        if(isVisible){
            settingsMenu.SetActive(false);
            isVisible = false;
        }else{
            settingsMenu.SetActive(true);
            isVisible = true;
        }
    }

    public void updateGameStatus(bool status){
        inGame = status;
    }
    public void HotKeyToggleSettingsMenu(){
        if(inGame && Input.GetKeyDown(toggleKeyCode)){
            ToggleSettingsMenu();
            gameplayToggler.toggleActions();
        }
    }
}
