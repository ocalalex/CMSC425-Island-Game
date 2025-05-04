using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public Camera startCamera;
    public Camera gameCamera;
    public UnityEvent playEvent;
    AudioListener startListener;
    AudioListener gameListener;

    [Header("Debugging")]
    public bool skipMenu = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (startCamera == null)
        {
            Debug.LogError("Start Camera is not assigned in the inspector.");
        }

        startListener = startCamera.GetComponent<AudioListener>();
        if (startListener == null)
        {
            Debug.LogWarning("Start Camera doesn't contain AudioListener component.");
        }
        gameListener = gameCamera.GetComponent<AudioListener>();
        if (gameListener == null)
        {
            Debug.LogWarning("Game Camera doesn't contain AudioListener component.");
        }
        if (skipMenu)
        {
            playGame();
        }
        else
        {
            showMainMenu();
        }
        
    }

    public void showMainMenu()
    {
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
        startListener.enabled = true;
        gameListener.enabled = false;
    }

    public void playGame(){
        startCamera.enabled = false;
        gameCamera.enabled = true;
        startListener.enabled = false;
        gameListener.enabled = true;
        playEvent.Invoke();
        mainMenuUI.SetActive(false);
    }
}
