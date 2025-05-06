using UnityEngine;

public class OnLadder : MonoBehaviour
{
    public GameObject player;

    // when player collides with ladder, change player's movement to MoverOnLadder
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

    // when player exits ladder, change player's movement to Mover
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
