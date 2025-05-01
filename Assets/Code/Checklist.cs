using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Checklist : MonoBehaviour
{
    public List<Behaviour> componentsToDisable = new List<Behaviour>(); //list of components to disable when the checklist is open
    bool checklistEnbled;
    public Key checklistKey = Key.Q;

    void Start()
    {
        gameObject.SetActive(false);
        checklistEnbled = true;
    }

    void Update()
    {
        Debug.Log("checklistKey");
        if (Keyboard.current[checklistKey].wasReleasedThisFrame) {
            Debug.Log("a");
            gameObject.SetActive(true);
        }
    }

    void disableComponents()
    {
        foreach (Behaviour component in componentsToDisable)
        {
            component.enabled = false; //disable all the components in the list
        }
    }
    void enableComponents()
    {
        foreach (Behaviour component in componentsToDisable)
        {
            component.enabled = true; //enable all the components in the list
        }
    }
}
