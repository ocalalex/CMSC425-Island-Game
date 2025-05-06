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


    private int objectsLayer;

    void Start()
    {
       objectsLayer = LayerMask.GetMask("Objects"); 
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
    void Update() {
        // if clicking
        if (Input.GetMouseButtonDown(0)) {
            if (Camera.main != null) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // if mouse is pointed at object within click distance
                if (Physics.Raycast(ray, out hit, clickRadius, objectsLayer)) {
                    if (hit.transform == transform) {
                        // pick up item
                        inventory.AddItem(gameObject);
                        // if dialogue exists for item, run it
                        dialogueEvent?.Invoke();
                        if (checklistController != null) { // check item off checklist if it is on checklist
                            checklistController.CheckItem(gameObject);
                        }
                    }
                }
            }
        }
    }
}
