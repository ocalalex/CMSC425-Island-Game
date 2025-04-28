using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Mover : MonoBehaviour
{
    [Header("Moving Keys")]

    public Key fdKeyNum = Key.UpArrow;
    public Key bkKeyNum = Key.DownArrow;
    public Key ltKeyNum = Key.LeftArrow;
    public Key rtKeyNum = Key.RightArrow;
    
    [Header("Movement Options")]
    public float speed = 10f;
    public float collisionBuffer = 0.5f;
    KeyControl fdKey;
    KeyControl bkKey;
    KeyControl ltKey;
    KeyControl rtKey;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fdKey = Keyboard.current[fdKeyNum];
        bkKey = Keyboard.current[bkKeyNum];
        ltKey = Keyboard.current[ltKeyNum];
        rtKey = Keyboard.current[rtKeyNum];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = Vector3.zero;

        if (fdKey.isPressed) input += transform.forward;
        if (bkKey.isPressed) input -= transform.forward;
        if (ltKey.isPressed) input -= transform.right;
        if (rtKey.isPressed) input += transform.right;

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
    }
}
