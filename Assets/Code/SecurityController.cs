using UnityEngine;
using UnityEngine.AI;

public class SecurityController : MonoBehaviour
{

    private NavMeshAgent agent;
    public GameObject player;
    private Vector3 playerPos;
    public float lightHouseY;
    private Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        if (playerPos.y >= lightHouseY)
        { 
            agent.SetDestination(playerPos);
        } else {
            agent.SetDestination(startPos); // resets back to beginning to avoid crowding the ladder, making it impossible
        }     
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player) {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            Vector3 teleportPosition = player.GetComponent<Teleporter>().teleportPosition;
            if(teleportPosition != null)
            {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = teleportPosition;
            }
            if (agent.speed < 8.0) {
                agent.speed += 0.5f; // gets madder
            }
        }
    }
}
