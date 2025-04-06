using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Inventory : MonoBehaviour
{
    public Sprite itemImage;
    public Image[] inventory;
    private List<GameObject> items;
    void Start()
    {
        items = new List<GameObject>();
        Display();
    }

    public void AddItem(GameObject item) {
        items.Add(item);
        item.SetActive(false);
        Display();
    }

    public void UseItem(int i) {
        // Use key to open door
        items.RemoveAt(i);
        Display();
    }

    public void UseItem(GameObject item) {
        items.Remove(item);
        Display();
    }

    public Boolean CheckItem(GameObject item) {
        return items.Contains(item);
    }

    void Display() {
        for (int i = 0; i < inventory.Length; i++) {
            if (i < items.Count) {
                inventory[i].sprite = itemImage;
            } else {
                inventory[i].sprite = null;
            }
        }
    }
}
