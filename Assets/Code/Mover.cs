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

        if (fdKey.isPressed)
            input += transform.forward;
        if (bkKey.isPressed)
            input -= transform.forward;
        if (ltKey.isPressed)
            input -= transform.right;
        if (rtKey.isPressed)
            input += transform.right;
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
            Vector3 move = input * speed * Time.fixedDeltaTime;

            bool moveBool = true;

            if (Physics.Raycast(rb.position, input, out RaycastHit hit, move.magnitude + 0.1f))
            {
                if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Ground")) {
                    moveBool = false; 
                }
                
            }

            if (moveBool) {
                rb.MovePosition(rb.position + move);
            }
            
        }

        CheckGrounded();

        if (jmpKey.isPressed && onGround)
        {
            Vector3 impulse = Vector3.up;
            rb.AddForce(jumpSize*impulse, ForceMode.Impulse);
            onGround = false;
        }
    }
    void CheckGrounded()
    {
        Ray ray = new Ray(feet.transform.position, Vector3.down);
        onGround = Physics.Raycast(ray, 0.5f);
        Debug.Log(onGround);
    }
}
