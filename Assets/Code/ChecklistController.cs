using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ChecklistController : MonoBehaviour
{
    public GameObject checklist;
    public bool foundBoat = false;
    public Key checklistKey = Key.Tab;
    bool checklistShown = false;
    public GameObject gear;
    public GameObject toolbox;
    public GameObject fuel;
    public GameObject engine;
    public GameObject propeller;

    void Update() {
        if (foundBoat && Keyboard.current[checklistKey].wasReleasedThisFrame) {
            if (checklistShown) {
                checklist.SetActive(false);
            } else {
                checklist.SetActive(true);
            }
            checklistShown = !checklistShown;
        }
    }

     public void CheckItem(GameObject item) {
        checklist.SetActive(true);
        if (item == toolbox) {
            checklist.GetComponent<Checklist>().checkItem(0);
        } else if (item == fuel) {
            checklist.GetComponent<Checklist>().checkItem(1);
        } else if (item == engine) {
            checklist.GetComponent<Checklist>().checkItem(2);
        } else if (item == propeller) {
            checklist.GetComponent<Checklist>().checkItem(3);
        } else if (item == gear) {
            checklist.GetComponent<Checklist>().checkItem(4);
        }
        checklist.SetActive(false);
    }
}
