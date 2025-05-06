using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class SecurityController : MonoBehaviour
{

    private NavMeshAgent agent;
    public GameObject player;
    private Vector3 playerPos;
    public float lightHouseY;
    private Vector3 startPos;

    public UnityEvent WarnPlayer;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position; // stores start location
    }

    void Update()
    {
        playerPos = player.transform.position;
        if (playerPos.y >= lightHouseY) // if player is high enough to be seen by security guard, he moves towards player
        { 
            agent.SetDestination(playerPos);
        } else {
            agent.SetDestination(startPos); // resets security back to beginning to avoid crowding the ladder
        }     
    }

    // on collision with player, teleport player to spawn and increase speed
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player) {
            WarnPlayer?.Invoke(); // suggests that the player find a weapon

            Rigidbody rb = player.GetComponent<Rigidbody>();
            Teleporter teleporter = player.GetComponent<Teleporter>();
            teleporter.Teleport();
            if (agent.speed < 8.0) {
                agent.speed += 0.5f; // gets madder
            }
        }
    }
}
