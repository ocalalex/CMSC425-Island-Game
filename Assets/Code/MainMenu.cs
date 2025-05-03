using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public Camera startCamera;
    public Camera gameCamera;
    public UnityEvent playEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (startCamera == null)
        {
            Debug.LogError("Start Camera is not assigned in the inspector.");
        }
        if (gameCamera == null)
        {
            Debug.LogError("Game Camera is not assigned in the inspector.");
        }
        if (playEvent == null)
        {
            Debug.LogError("Play Event is not assigned in the inspector.");
        }
        if (mainMenuUI == null)
        {
            Debug.LogError("Main Menu UI is not assigned in the inspector.");
        }
        mainMenuUI.SetActive(true);
        startCamera.enabled = true;
        gameCamera.enabled = false;
    }

    public void playGame(){
        startCamera.enabled = false;
        gameCamera.enabled = true;
        playEvent.Invoke();
        mainMenuUI.SetActive(false);
    }
}
