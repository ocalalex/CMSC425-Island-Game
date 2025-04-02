using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
