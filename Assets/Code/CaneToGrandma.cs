using System;
using UnityEngine;
using UnityEngine.Events;

public class CaneToGrandma : MonoBehaviour
{
    public Inventory inventory;
    public GameObject toolbox;
    public GameObject player;
    public GameObject cane;
    public float clickRadius = 20f; 
    private int objectsLayer;
    public ChecklistController checklistController;

    public UnityEvent GrandmaHelpEvent;

    public UnityEvent ReturnCaneEvent;

    void Start()
    {
        objectsLayer = LayerMask.GetMask("Objects");
        if (inventory == null)
            Debug.LogWarning("Inventory not assigned to CaneToGrandma.");
        if (toolbox == null)
            Debug.LogWarning("Toolbox not assigned to CaneToGrandma.");
    }

    void Update()
    {
        // if player is near grandma, she asks for help - in Dialogue script, it stops this from happening more than once
        if (Vector3.Distance(transform.position, player.transform.position) <= clickRadius) {
            GrandmaHelpEvent?.Invoke(); // dialogue that asks player for help finding cane
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main != null) {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, clickRadius, objectsLayer)) { //note: grandma is in objectsLayer
                    if (hit.transform == transform) {
                        Debug.Log("Clicked on Grandma");
                        if (inventory.CheckItem(cane)) // see if user has cane to give to grandma
                        {
                            ReturnCaneEvent?.Invoke(); // dialogue thanking player
                            inventory.UseItem(cane);   // remove cane from inv
                            inventory.AddItem(toolbox);      // add toolbox to inv
                            checklistController.CheckItem(toolbox); // check toolbox in checklist

                        }
                    
                    }
                }
            }
        }
    }
}