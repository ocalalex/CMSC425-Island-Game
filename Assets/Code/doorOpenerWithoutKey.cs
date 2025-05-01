using UnityEngine;

public class doorOpenerWithoutKey : MonoBehaviour
{
    public float doorOpenAngle = 90f;
    private bool isOpen = false;
    private float original;

    private void OnMouseDown()
    {
        //will open the door 
        if (!isOpen)
        {
            Debug.Log("Door opened");
            Vector3 rotation = transform.localEulerAngles;
            //operate on x
            if (CompareTag("OpenX")) {
                original = rotation.x;
                rotation.x += doorOpenAngle;
            }
            //operate on z
            else if (CompareTag("OpenZ")) {
                original = rotation.z;
                rotation.z += doorOpenAngle;
            }
            //operate on y
            else {
                original = rotation.y; //saves the original Y for closing later
                rotation.y += doorOpenAngle; //sets the new Y to the open angle
            }
            transform.localEulerAngles = rotation;
            isOpen = true;
        }
        //will close the door
        else {
            Debug.Log("Door closed");
            Vector3 rotation = transform.localEulerAngles;
            //operate on x
            if (CompareTag("OpenX")) {
                rotation.x = original;
            }
            else if (CompareTag("OpenZ")) {
                rotation.z = original;
            }
            //operate on y
             else {
                rotation.y = original;
            }
            transform.localEulerAngles = rotation;
            isOpen = false;
        }
        
    }
}