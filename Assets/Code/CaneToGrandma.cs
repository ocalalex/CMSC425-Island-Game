using UnityEngine;

public class CaneToGrandma : MonoBehaviour
{
    public Inventory inventory;
    public GameObject toolbox;
    public LayerMask interactLayer; // Layer Grandma is on
    public float clickRadius = 20f; 

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;

        if (inventory == null)
            Debug.LogWarning("Inventory not assigned to CaneToGrandma.");
        if (toolbox == null)
            Debug.LogWarning("Toolbox not assigned to CaneToGrandma.");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 20f, interactLayer))
            {
                if (hit.collider.gameObject.name == "Grandma")
                {
                    Debug.Log("Clicked on Grandma");

                    if (inventory.CheckItem(gameObject)) // this cane
                    {
                        Debug.Log("Grandma received the cane");

                        inventory.UseItem(gameObject);   // remove cane
                        inventory.AddItem(toolbox);      // add toolbox

                        // Optional: destroy cane object from scene
                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log("Cane not in inventory");
                    }
                }
            }
        }
    }
}