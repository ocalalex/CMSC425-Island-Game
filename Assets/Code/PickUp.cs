using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Inventory inventory;
    public GameObject key;
    void OnMouseDown() {
        inventory.AddItem(gameObject);
    }
}
