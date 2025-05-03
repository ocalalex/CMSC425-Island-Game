using UnityEngine;

public class Settings : MonoBehaviour
{
    bool isVisible = false;
    public GameObject settingsMenu;

    public void toggleSettingsMenu(){
        if(isVisible){
            settingsMenu.SetActive(false);
            isVisible = false;
        }else{
            settingsMenu.SetActive(true);
            isVisible = true;
        }
    }
}
