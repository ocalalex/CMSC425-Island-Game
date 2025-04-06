using UnityEngine;

public class GetInBoat : MonoBehaviour
{
    public float clickRadius = 10f;
    public GameObject playerObject;
    void OnMouseDown() 
    {
        Transform player = playerObject.transform;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= clickRadius)
        {
            sitInBoat();
        }
    }

    void sitInBoat() {
        Transform player = playerObject.transform;

        player.parent = transform;

        Rigidbody playerRB = playerObject.GetComponent<Rigidbody>();
        playerRB.isKinematic = true;

        player.localPosition = new Vector3(0, 0.2f, 0);

        playerObject.GetComponent<Mover>().enabled = false;
        
    }
}
