using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    public Sprite inventoryImage;
    public Inventory inventory;

    public float clickRadius = 20f;

    public GameObject playerObject;

    public UnityEvent dialogueEvent;
    public ChecklistController checklistController;

    void Start()
    {
       if(dialogueEvent == null){
            Debug.Log("DialogueEvent is not assigned in the inspector.");
       }
       if(inventory == null){
            Debug.Log("Inventory is not assigned in the inspector.");
       }
       if(inventoryImage == null){
            Debug.Log("InventoryImage is not assigned in the inspector.");
       }
    }

    // when object is clicked on within clickRadius (as controlled in ClickManager), pick up item
    public void Clicked() {
        // pick up item
        inventory.AddItem(gameObject);
        // if dialogue exists for item, run it
        dialogueEvent?.Invoke();
        if (checklistController != null) { // check item off checklist if it is on checklist
            checklistController.CheckItem(gameObject);
        }
    }
}
