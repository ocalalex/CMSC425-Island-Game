using System.Collections;
using System.Resources;
using UnityEngine;
using UnityEngine.Events;

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
    private int objectsLayer;
    public ChecklistController checklistController;

    public UnityEvent ChecklistAvailableEvent;
    void Start()
    {
        objectsLayer = LayerMask.GetMask("Objects"); 
        mainCamera.enabled = true;
        endCamera.enabled = false;
        mapCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main != null) {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // if player clicks and is within clicking distance
                if (Physics.Raycast(ray, out hit, clickRadius, objectsLayer)) {
                    if (hit.transform == transform) // checks that the hit object is the boat
                    {
                        // if all necessary items are in inv
                        if (inventory.CheckItem(gear) && inventory.CheckItem(toolbox) && inventory.CheckItem(fuel) && inventory.CheckItem(engine) && inventory.CheckItem(propeller))
                        {
                            sitInBoat();
                            // use all the items
                            inventory.UseItem(gear);
                            inventory.UseItem(toolbox);
                            inventory.UseItem(fuel);
                            inventory.UseItem(engine);
                            inventory.UseItem(propeller);
                            if (!isMoving)
                            {
                                StartCoroutine(MoveBoat(5f, 20f));
                            }
                        }
                        else
                        {
                            checklistController.foundBoat = true; // allows player to access checklist after clicking boat
                            // if not all items are in inv, remind player to use checklist
                            ChecklistAvailableEvent?.Invoke();
                        }
                    }
                }
            }
        }
    }
                
    private IEnumerator MoveBoat(float duration, float secDuration)
    {
        isMoving = true;
        float startTime = Time.time;
        while (Time.time < startTime + secDuration)
        {
            if (Time.time > startTime + duration)
            {
                mainCamera.enabled = false;
                endCamera.enabled = true;
            }
            transform.Translate(0, 0, -speed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }

    void sitInBoat()
    {
        // stop user from restarting if they accidentally collide with water while in boat
        Destroy(playerObject.GetComponent<FallerInWater>());
        
        Transform player = playerObject.transform;
        player.parent = transform; // make player a child of boat 

        Rigidbody playerRB = playerObject.GetComponent<Rigidbody>();
        playerRB.isKinematic = true; // disable physics for player

        player.localPosition = new Vector3(0, 0.2f, 0); // places user in position relative to boat

        playerObject.GetComponent<Mover>().enabled = false;

    }
}
