using UnityEngine;

public class CaneToGrandma : MonoBehaviour
{
    public Inventory inventory;
    public GameObject cane; //the cane to be given to grandma
    public GameObject toolbox;

    public float clickRadius = 20f;
    private int objectsLayer;

    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, clickRadius, objectsLayer)) 
            //casts a ray up to clickRadius and only detect hits with objects in the object layer
            {
                if (hit.collider.gameObject.name == "Grandma") //if you clicked on grandma
                {
                    Debug.Log("You clicked on Grandma");
                       //seeing if you have to cane to give to grandma, if you do then she will give you toolbox
                    if (inventory.CheckItem(cane)) {
                        Debug.Log("Grandma received the cane");
                        inventory.UseItem(cane); //removes cane from inventory 
                        inventory.AddItem(toolbox); //adds toolbox
                    } else {
                        Debug.Log("No Cane given to grandma");
                    }
            
                }
            }


        }
    }
}