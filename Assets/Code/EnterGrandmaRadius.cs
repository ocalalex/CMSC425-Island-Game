using UnityEngine;

public class EnterGrandmaRadius : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject grandmaCollider;

     private bool hasAskedGrandmaForHelp = false; //flag to see if clicked on grandma yet
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == grandmaCollider)
        {
            Debug.Log("Enter grandma collider");
            if (!hasAskedGrandmaForHelp) {
                dialogue.InsertGrandmaHelpLine();
                hasAskedGrandmaForHelp = true;
            }
        }
    }
}
