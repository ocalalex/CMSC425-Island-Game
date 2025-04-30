using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    public Sprite inventoryImage;
    public Inventory inventory;

    public float clickRadius = 20f;

    public GameObject playerObject;

    public UnityEvent dialogueEvent;


    private int objectsLayer;

    void Start()
    {
       objectsLayer = LayerMask.GetMask("Objects"); 
       if(dialogueEvent == null){
            Debug.LogError("DialogueEvent is not assigned in the inspector.");
       }
       if(inventory == null){
            Debug.LogError("Inventory is not assigned in the inspector.");
       }
       if(inventoryImage == null){
            Debug.LogError("InventoryImage is not assigned in the inspector.");
       }
    }
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, clickRadius, objectsLayer)) {
                if (hit.transform == transform) {
                    inventory.AddItem(gameObject);
                    dialogueEvent?.Invoke();
                }
            }
        }
    }
}
