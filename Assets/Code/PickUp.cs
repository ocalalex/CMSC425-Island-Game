using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Sprite inventoryImage;
    public Inventory inventory;

    public float clickRadius = 20f;

    public GameObject playerObject;
    void OnMouseDown() {

        Transform player = playerObject.transform;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= clickRadius)
        {
            inventory.AddItem(gameObject);
        } 
    }
}
