using System.Collections;
using System.Resources;
using UnityEngine;

public class GetInBoat : MonoBehaviour
{
    public float clickRadius = 20f;
    public GameObject playerObject;
    public Transform drone;
    private bool isMoving = false;
    public float speed = 10f;

    public Inventory inventory;
    public GameObject gear;
    public GameObject toolbox;
    public GameObject fuel;
    public GameObject engine;
    public GameObject propeller;
    public Camera mainCamera;
    public Camera endCamera;
    public Camera mapCamera;
    void Start()
    {
        mainCamera.enabled = true;
        endCamera.enabled = false;
        mapCamera.enabled = false;
    }
    void OnMouseDown() 
    {
        Transform player = playerObject.transform;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= clickRadius)
        {
            if (inventory.CheckItem(gear) && inventory.CheckItem(toolbox)) {
                sitInBoat();
                inventory.UseItem(gear);
                if (!isMoving) {
                    StartCoroutine(MoveBoat(5f, 20f));
                }
            } else {
                Debug.Log("No gear");
            }
        }
    }
    private IEnumerator MoveBoat(float duration, float secDuration) {
        isMoving = true;
        float startTime = Time.time;
        while (Time.time < startTime + secDuration) {
            if (Time.time > startTime + duration) {
                mainCamera.enabled = false;
                endCamera.enabled = true;
            }
            transform.Translate(0, 0, -speed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
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
