using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WireCutter : MonoBehaviour
{
    public UnityEvent NoToolboxEvent;
    public Inventory inventory;
    public GameObject toolbox;
    public GameObject spotlight;

    private LayerMask objectsLayer;
    public int clickRadius;

    void Start()
    {
        objectsLayer = LayerMask.GetMask("Objects");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main != null) {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // if clicking on wire
                if (Physics.Raycast(ray, out hit, clickRadius, objectsLayer)) {
                    if (hit.transform == transform) {
                        if (inventory.CheckItem(toolbox)) // see if user has toolbox to cut the wire with
                        {
                            SpotlightDetection spotlightDetection = spotlight.GetComponent<SpotlightDetection>();
                            spotlightDetection.uncutWires--; // number of uncut wires updated in SpotlightDetection 

                            Transform[] brokenWires = GetComponentsInChildren<Transform>(true); // the (true) includes inactive children 
                            foreach (Transform brokenWire in brokenWires) { // activates the broken wire objects
                                brokenWire.parent = null;
                                brokenWire.gameObject.SetActive(true);
                            }

                            Destroy(this.gameObject); // destroys the uncut wire object 

                        }
                        else // if no toolkit, tell user they need a toolkit
                        {
                            NoToolboxEvent?.Invoke();
                        }
                    
                    }
                }
            }
        }
        
    }
}
