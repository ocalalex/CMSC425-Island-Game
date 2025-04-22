using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Equipper : MonoBehaviour
{
    public Key equipKeyNum = Key.E;
    public Inventory inventory;
    public GameObject user;
    public GameObject item;
    private bool equipped = false;
    private KeyControl equipKey;

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
        item.transform.position = transform.position + new Vector3(0, 0, 1);
        item.transform.rotation = transform.rotation;
        
    }

    void UnequipItem()
    {
        equipped = false;
        item.SetActive(false);
        
    }
}
