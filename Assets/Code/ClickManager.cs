using UnityEngine;

// this class keeps track of all clicks, checking whether player is clicking on an object, 
// and if that object has a PickUp component, it calls the component
public class ClickManager : MonoBehaviour
{

    private int objectsLayer;
    void Start()
    {
        objectsLayer = LayerMask.GetMask("Objects"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (Camera.main != null) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // if mouse is pointed at an object within click distance
                if (Physics.Raycast(ray, out hit, 100, objectsLayer)) { // uses 100 as arbitrary large number for maximum distance
                    PickUp itemToPickUp = hit.transform.GetComponent<PickUp>();
                    if(itemToPickUp != null) {
                        // if the object is close enough to be clicked according to its clickRadius, then click it
                        if (hit.distance <= itemToPickUp.clickRadius) {
                            itemToPickUp.Clicked();
                        }
                    }
                }
            }
        }
        
    }
}
