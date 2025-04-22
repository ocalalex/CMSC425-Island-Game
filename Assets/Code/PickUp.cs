using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Sprite inventoryImage;
    public Inventory inventory;

    public float clickRadius = 20f;

    public GameObject playerObject;
    private int objectsLayer;

    void Start()
    {
       objectsLayer = LayerMask.GetMask("Objects"); 
    }
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, clickRadius, objectsLayer)) {
                if (hit.transform == transform) {
                    inventory.AddItem(gameObject);
                }
            }
        }
    }
}
