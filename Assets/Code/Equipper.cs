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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        equipKey = Keyboard.current[equipKeyNum];
    }

    // Update is called once per frame
    void Update()
    {

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
        item.transform.SetParent(hand.transform, false);
        item.transform.localPosition = Vector3.zero;
        item.transform.rotation = Quaternion.LookRotation(transform.forward);
        item.transform.localPosition += itemOffset;
        
    }

    void UnequipItem()
    {
        equipped = false;
        item.SetActive(false);
        
    }

    public Boolean isEquipped()
    {
        return equipped;
    }
}
