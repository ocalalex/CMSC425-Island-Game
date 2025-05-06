using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Inventory : MonoBehaviour
{
    //public Sprite itemImage;
    public Image[] inventory;
    private List<GameObject> items;
    private List<Sprite> itemSprites;
    void Start()
    {
        items = new List<GameObject>();
        itemSprites = new List<Sprite>();
        Display();
    }

    // add an item to the inventory, and update display
    public void AddItem(GameObject item) {
        items.Add(item);

        PickUp assignSprite = item.GetComponent<PickUp>();
        itemSprites.Add(assignSprite.inventoryImage);
        

        item.SetActive(false);
        Display();
    }

    // remove item from inventory using index, and update display
    public void UseItem(int i) {
        // Use key to open door
        items.RemoveAt(i);
        itemSprites.RemoveAt(i);
        Display();
    }

    // remove item from inventory, and update display
    public void UseItem(GameObject item) {
        items.Remove(item);

        PickUp assignSprite = item.GetComponent<PickUp>();
        itemSprites.Remove(assignSprite.inventoryImage);

        Display();
    }

    // checks if item is in inv
    public Boolean CheckItem(GameObject item) {
        return items.Contains(item);
    }

    // updates inventory slots to display exactly the objects in inv
    void Display() {
        for (int i = 0; i < inventory.Length; i++) { // for each inventory slot
            if (i < items.Count) { // if there exists an item for that slot

                PickUp assignSprite = items[i].GetComponent<PickUp>(); // item sprite is stored in the pickup script for the item
                inventory[i].sprite = assignSprite.inventoryImage; // assign inventory slot with item sprite
            } else { // if inventory slot is empty
                inventory[i].sprite = null;
            }
        }
    }
}
