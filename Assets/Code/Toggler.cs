using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class Toggler : MonoBehaviour
{
    public List<Behaviour> actionsToToggle;
    public List<GameObject> objectsToToggle;
    public bool status = false;
    List<bool> previousActionsState;
    List<bool> previousObjectsState;

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

        backupStates(); //backup the states of the actions and objects

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

    public void smartToggle(){
        status = !status;
        Debug.Log("Smart toggling actions and objects. New status: " + status);
        if(!status){

            backupStates(); //backup the states of the actions and objects
            foreach (GameObject obj in objectsToToggle)
            {
                obj.SetActive(status);
            }
            foreach (Behaviour action in actionsToToggle)
            {
                action.enabled = status;
            }
        
        }else{
            //restores the previous state of the actions and objects when status is true
            for(int i = 0; i < previousObjectsState.Count; i++){
                objectsToToggle[i].SetActive(previousObjectsState[i]);
            }
            for(int i = 0; i < previousActionsState.Count; i++){
                actionsToToggle[i].enabled = previousActionsState[i];
            }
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

    //restores the previous state of the actions and objects when status is true
    void backupStates(){
        previousActionsState = new List<bool>();
        foreach (Behaviour action in actionsToToggle)
        {
            previousActionsState.Add(action.enabled);
        }

        previousObjectsState = new List<bool>();
        foreach (GameObject obj in objectsToToggle)
        {
            previousObjectsState.Add(obj.activeSelf);
        }
    }
    
}
