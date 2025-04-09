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

    public void AddItem(GameObject item) {
        items.Add(item);

        PickUp assignSprite = item.GetComponent<PickUp>();
        itemSprites.Add(assignSprite.inventoryImage);
        

        item.SetActive(false);
        Display();
    }

    public void UseItem(int i) {
        // Use key to open door
        items.RemoveAt(i);
        itemSprites.RemoveAt(i);
        Display();
    }

    public void UseItem(GameObject item) {
        items.Remove(item);

        PickUp assignSprite = item.GetComponent<PickUp>();
        itemSprites.Remove(assignSprite.inventoryImage);

        Display();
    }

    public Boolean CheckItem(GameObject item) {
        return items.Contains(item);
    }

    /* void Display() {
        for (int i = 0; i < inventory.Length; i++) {
            if (i < items.Count) {
                inventory[i].sprite = itemImage;
            } else {
                inventory[i].sprite = null;
            }
        }
    } */

    void Display() {
        for (int i = 0; i < inventory.Length; i++) {
            if (i < items.Count) {

                PickUp assignSprite = items[i].GetComponent<PickUp>();
                inventory[i].sprite = assignSprite.inventoryImage;
            } else {
                inventory[i].sprite = null;
            }
        }
    }
}
