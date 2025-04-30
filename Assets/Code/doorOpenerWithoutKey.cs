using UnityEngine;

public class doorOpenerWithoutKey : MonoBehaviour
{
    public float doorOpenAngle = 90f;
    private bool isOpen = false;

    private void OnMouseDown()
    {
        if (!isOpen)
        {
            Debug.Log("Door clicked");
            Vector3 rotation = transform.localEulerAngles;
            rotation.y += doorOpenAngle;
            transform.localEulerAngles = rotation;
            isOpen = true;
        }
    }
}