using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Equipper : MonoBehaviour
{
    public Key equipKeyNum = Key.E;
    public Inventory inventory;
    public GameObject hand;
    public GameObject item;
    private bool equipped = false;
    private KeyControl equipKey;

    [SerializeField]
    private Vector3 itemOffset = new Vector3(-0.5f, 1.5f, 1);
    private Vector3 rotationOffset = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        equipKey = Keyboard.current[equipKeyNum];
    }

    // Update is called once per frame
    void Update()
    {

        // Only equips if item is in inventory and equip key is released
        if(inventory.CheckItem(item) && equipKey.wasReleasedThisFrame)
        {
            Debug.Log("Equipping item: " + item.name);
            if (equipped)
            {
                UnequipItem();
            }
            else
            {
                EquipItem();
            }
        }
    }

    void EquipItem()
    {
        item.SetActive(true);
        item.GetComponent<BoxCollider>().enabled = false;
        equipped = true;

        // Ensures the item is properly rotated when equipped in the hand
        item.transform.SetParent(hand.transform, false);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.Euler(rotationOffset);
        item.transform.localPosition += itemOffset;
        
    }

    void UnequipItem()
    {
        equipped = false;
        item.SetActive(false);
        
    }

    // Getter for equipped variable
    public Boolean isEquipped()
    {
        return equipped;
    }
}
