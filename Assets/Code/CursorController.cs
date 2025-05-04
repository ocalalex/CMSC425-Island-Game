using UnityEngine;

public class CursorController : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; // Keeps the cursor within the game window
        Cursor.visible = true;
    }

    public void gameStart()
    {
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the game window
        Cursor.visible = false; // Hides the cursor
    }
}
