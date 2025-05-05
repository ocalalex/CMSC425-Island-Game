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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectsLayer = LayerMask.GetMask("Objects");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main != null) {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, clickRadius, objectsLayer)) {
                    if (hit.transform == transform) {
                        if (inventory.CheckItem(toolbox)) // see if user have cane to give to grandma
                        {
                            SpotlightDetection spotlightDetection = spotlight.GetComponent<SpotlightDetection>();
                            spotlightDetection.uncutWires--;

                            Transform[] brokenWires = GetComponentsInChildren<Transform>(true); // the (true) gets inactive children 
                            foreach (Transform brokenWire in brokenWires) { // activates the broken wire objects
                                brokenWire.parent = null;
                                brokenWire.gameObject.SetActive(true);
                            }

                            Destroy(this.gameObject);

                        }
                        else
                        {
                            NoToolboxEvent?.Invoke();
                        }
                    
                    }
                }
            }
        }
        
    }
}
