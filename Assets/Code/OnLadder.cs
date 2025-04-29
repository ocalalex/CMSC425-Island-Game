using UnityEngine;

public class OnLadder : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("On ladder");
            Rigidbody playerRB = player.GetComponent<Rigidbody>();
            playerRB.isKinematic = true;

            player.GetComponent<Mover>().enabled = false;
            player.GetComponent<MoverOnLadder>().enabled = true;
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            Rigidbody playerRB = player.GetComponent<Rigidbody>();
            playerRB.isKinematic = false;

            player.GetComponent<Mover>().enabled = true;
            player.GetComponent<MoverOnLadder>().enabled = false;
            
        }
    }
}
