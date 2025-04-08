using UnityEngine;

public class PlayerExitTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject building;

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == building)
        {
            Debug.Log("Exited building");
            dialogue.TriggerExitLine();
        }
    }
}