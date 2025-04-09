using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Sprite inventoryImage;
    public Inventory inventory;
    public GameObject key;
    void OnMouseDown() {
        inventory.AddItem(gameObject);
    }
}
