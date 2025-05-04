using System.Collections.Generic;
using UnityEngine;

public class Toggler : MonoBehaviour
{
    public List<Behaviour> actionsToToggle;
    public List<GameObject> objectsToToggle;
    public bool status = false;

    void Start()
    {
        if (actionsToToggle == null)
        {
            Debug.LogWarning("No actions to toggle assigned in the inspector.");
            actionsToToggle = new List<Behaviour>();
        }
        if (objectsToToggle == null)
        {
            Debug.LogWarning("No objects to toggle assigned in the inspector.");
            objectsToToggle = new List<GameObject>();
        }

        //sets the intial status of the actions and objects to the status variable
        foreach (Behaviour action in actionsToToggle)
        {
            action.enabled = status;
        }
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(status);
        }
    }

    //toggles the status of the actions and objects
    public void toggleActions()
    {
        status = !status;
        Debug.Log("Toggling actions and objects. New status: " + status);
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(status);
        }
        foreach (Behaviour action in actionsToToggle)
        {
            action.enabled = status;
        }
        
    }
    
}
