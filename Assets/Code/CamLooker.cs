using UnityEngine;

public class CamLooker : MonoBehaviour
{
    public float sensitivity = 5f;
    public float maxXRotation = 80f;
    public float minXRotation = -80f;

    float rotationX = 0f;
    float rotationY = 0f;

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;

  

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}
