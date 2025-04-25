using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Mover : MonoBehaviour
{
    [Header("Turning Keys")]
    public Inventory inventory;
    public GameObject map;
    public GameObject userMarker;
    public GameObject keyMarkerText;
    public GameObject keyMarkerDot;

    public float collisionBuffer = 0.5f;
    public Key fdKeyNum = Key.UpArrow;
    public Key bkKeyNum = Key.DownArrow;
    public Key ltKeyNum = Key.LeftArrow;
    public Key rtKeyNum = Key.RightArrow;
    
    private bool onGround = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 10f;
    KeyControl fdKey;
    KeyControl bkKey;
    KeyControl ltKey;
    KeyControl rtKey;
    KeyControl jmpKey;
    KeyControl mapKey;
    Rigidbody rb;

    [Header("Jumping")]
    public Key jmpKeyNum = Key.Space;
    public Key mapKeyNum = Key.M;
    public bool inMapView = false;
    public float jumpSize = 5;

    private Transform feet; 
    public Camera mainCamera;
    public Camera mapCamera;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        feet = transform.Find("Feet");
        keyMarkerText.GetComponent<Renderer>().enabled = false;
        keyMarkerDot.GetComponent<Renderer>().enabled = false;
        userMarker.GetComponent<Renderer>().enabled = false;
        fdKey = Keyboard.current[fdKeyNum];
        bkKey = Keyboard.current[bkKeyNum];
        ltKey = Keyboard.current[ltKeyNum];
        rtKey = Keyboard.current[rtKeyNum];
        jmpKey = Keyboard.current[jmpKeyNum];
        mapKey = Keyboard.current[mapKeyNum];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = Vector3.zero;

        if (fdKey.isPressed) input += transform.forward;
        if (bkKey.isPressed) input -= transform.forward;
        if (ltKey.isPressed) input -= transform.right;
        if (rtKey.isPressed) input += transform.right;
        if (mapKey.wasReleasedThisFrame)
        {
            if (inventory.CheckItem(map)) 
            {
                if (inMapView)
                {
                    inMapView = false;
                    keyMarkerText.GetComponent<Renderer>().enabled = false;
                    keyMarkerDot.GetComponent<Renderer>().enabled = false;
                    userMarker.GetComponent<Renderer>().enabled = false;
                    mainCamera.enabled = true;
                    mapCamera.enabled = false;
                } else {
                    inMapView = true;
                    mainCamera.enabled = false;
                    keyMarkerText.GetComponent<Renderer>().enabled = true;
                    keyMarkerDot.GetComponent<Renderer>().enabled = true;
                    userMarker.GetComponent<Renderer>().enabled = true;
                    mapCamera.enabled = true;
                }
            }
        }

        if (input != Vector3.zero)
        {
            input.Normalize();
            Vector3 move = input * speed * Time.deltaTime;

            Vector3 moveX = new Vector3(move.x, 0, 0);
            Vector3 moveZ = new Vector3(0, 0, move.z);

            Vector3 topPos = rb.position + new Vector3(0, 1.25f, 0);
            Vector3 bottomPos = rb.position + new Vector3(0, -1.25f, 0);

            bool checkTopX = Physics.Raycast(topPos, moveX, out RaycastHit hitTopX, moveX.magnitude + collisionBuffer);
            bool checkBottomX = Physics.Raycast(bottomPos, moveX, out RaycastHit hitBottomX, moveX.magnitude + collisionBuffer);
            if (checkTopX) {
                bool hitInvisColl = hitTopX.collider.CompareTag("Tree") || hitTopX.collider.CompareTag("WalkThrough");

                if (!hitInvisColl) {
                    move.x = 0;
                }
            }
            if (checkBottomX) {
                bool hitInvisColl = hitBottomX.collider.CompareTag("Tree") || hitBottomX.collider.CompareTag("WalkThrough");

                if (!hitInvisColl) {
                    move.x = 0;
                }
            }

            bool checkTopZ = Physics.Raycast(topPos, moveZ, out RaycastHit hitTopZ, moveZ.magnitude + collisionBuffer);
            bool checkBottomZ = Physics.Raycast(bottomPos, moveZ, out RaycastHit hitBottomZ, moveZ.magnitude + collisionBuffer);
            bool checkZ = checkTopZ || checkBottomZ;

            if (checkTopZ) {
                bool hitInvisColl = hitTopZ.collider.CompareTag("Tree") || hitTopZ.collider.CompareTag("WalkThrough");

                if (!hitInvisColl) {
                    move.z = 0;
                }
            }
            if (checkBottomZ) {
                bool hitInvisColl = hitBottomZ.collider.CompareTag("Tree") || hitBottomZ.collider.CompareTag("WalkThrough");

                if (!hitInvisColl) {
                    move.z = 0;
                }
            }

            rb.MovePosition(rb.position + move);
            
        }

        if (jmpKey.isPressed && onGround)
        {
            Vector3 impulse = Vector3.up;
            rb.AddForce(jumpSize*impulse, ForceMode.Impulse);
            onGround = false;
        }

        if (rb.linearVelocity.y > jumpSize)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpSize, rb.linearVelocity.z);
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (!onGround) 
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
                {
                    onGround = true;
                    return;
                }
            }
        }
    }
}
