using System;
using UnityEngine;
using UnityEngine.Events;

public class CaneToGrandma : MonoBehaviour
{
    public Inventory inventory;
    public GameObject toolbox;
    public GameObject cane;
    public float clickRadius = 20f; 
    private int objectsLayer;
    
    private Boolean firstTalk = true;

    private Camera mainCam;
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
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main != null) {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, clickRadius, objectsLayer)) {
                    if (hit.transform == transform) {
                        Debug.Log("Clicked on Grandma");
                        if (inventory.CheckItem(cane)) // see if user have cane to give to grandma
                        {
                            ReturnCaneEvent?.Invoke();
                            inventory.UseItem(cane);   // remove cane
                            inventory.AddItem(toolbox);      // add toolbox
                            checklistController.CheckItem(toolbox);

                        }
                        else
                        {
                            if (firstTalk) {
                                firstTalk = !firstTalk;
                                GrandmaHelpEvent?.Invoke();
                            }
                        }
                    
                    }
                }
            }
        }
    }
}