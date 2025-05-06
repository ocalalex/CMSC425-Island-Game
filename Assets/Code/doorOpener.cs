using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*This code is used to open the initial door with a key*/
public class doorOpener : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float angleOpened = 110;
    public float angleClosed = 0;
    Quaternion rotOpened;
    Quaternion rotClosed;

    public Inventory inventory;

    public GameObject key;
    void Start()
    {
        //set the rotations of the door
        rotOpened = Quaternion.Euler(0, angleOpened, 0);
        rotClosed = Quaternion.Euler(0, angleOpened, 0);
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        //If the user has the key in inventory, opens the door and the key disappears from inventory
        if (inventory.CheckItem(key)) {
            OpenDoor();
            inventory.UseItem(key);
        } else {
            Debug.Log("No key");
        }
    }
    private void OpenDoor()
    {
        //rotates the door
        transform.localRotation = Quaternion.Lerp(rotClosed, rotOpened, Time.deltaTime);
    }
}
