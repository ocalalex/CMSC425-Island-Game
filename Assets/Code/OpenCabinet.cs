using UnityEngine;

public class OpenCabinet : MonoBehaviour
{
    private bool isOpen = false;
    public float amountOpen = 1f;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void OnMouseDown()
    {
        Vector3 pos = originalPosition;

        if (!isOpen)
        {
            pos.z -= amountOpen; // or -= if you want it to move the other way
            isOpen = true;
        }
        else
        {
            // Go back to original
            pos = originalPosition;
            isOpen = false;
        }

        transform.localPosition = pos;
    }
}