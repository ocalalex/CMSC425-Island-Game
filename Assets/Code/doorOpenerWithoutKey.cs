using UnityEngine;

public class doorOpenerWithoutKey : MonoBehaviour
{
    private bool isOpen = false;

    private void OnMouseDown()
    {
        if (!isOpen)
        {
            Debug.Log("Door clicked");
            transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            isOpen = true;
        }
    }
}