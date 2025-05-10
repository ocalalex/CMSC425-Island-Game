using System;
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

        // update movement vector depending on keys pressed
        if (fdKey.isPressed) input += transform.forward;
        if (bkKey.isPressed) input -= transform.forward;
        if (ltKey.isPressed) input -= transform.right;
        if (rtKey.isPressed) input += transform.right;

        if (input != Vector3.zero)
        {
            input.Normalize(); // normalize the direction vector
            Vector3 move = input * speed * Time.deltaTime; // change vector length

            // separates out x and z directions
            Vector3 moveX = new Vector3(move.x, 0, 0);
            Vector3 moveZ = new Vector3(0, 0, move.z);

            // specifies locations for collision checking rays to ensure 
            // that the top AND bottom of user is considered in collisions
            Vector3 topPos = rb.position + new Vector3(0, 1.25f, 0);
            Vector3 bottomPos = rb.position + new Vector3(0, -1.25f, 0);

            // checks X direction for collisions
            bool checkTopX = Physics.Raycast(topPos, moveX, out RaycastHit hitTopX, moveX.magnitude + collisionBuffer);
            bool checkBottomX = Physics.Raycast(bottomPos, moveX, out RaycastHit hitBottomX, moveX.magnitude + collisionBuffer);

            // these tags identify objects with irregular collision boxes that are not true to reality
            String[] tagsToWalkThrough = {"Tree", "WalkThrough", "LadderBottom", "LadderTop"};

            // if player collided in X direction from top of body, check if the collision was 
            // a collider the player can walk through
            if (checkTopX) {
                bool hitInvis = false;
                foreach (String tag in tagsToWalkThrough) {
                    hitInvis = hitInvis || hitTopX.collider.CompareTag(tag);
                }

                if (!hitInvis) { // if collision CAN'T be walked through, then don't move in x direction
                    move.x = 0;
                }
            } else if (checkBottomX) { // do same thing for bottom of the body
                bool hitInvisColl = false;
                foreach (String tag in tagsToWalkThrough) {
                    hitInvisColl = hitInvisColl || hitBottomX.collider.CompareTag(tag);
                }
                if (!hitInvisColl) { // if collision CAN'T be walked through, then don't move in x direction
                    move.x = 0;
                }
            }

            // checks Z direction for collisions
            bool checkTopZ = Physics.Raycast(topPos, moveZ, out RaycastHit hitTopZ, moveZ.magnitude + collisionBuffer);
            bool checkBottomZ = Physics.Raycast(bottomPos, moveZ, out RaycastHit hitBottomZ, moveZ.magnitude + collisionBuffer);

            // if player collided in Z direction from top of body, check if the collision was 
            // a collider the player can walk through
            if (checkTopZ) {
                bool hitInvis = false;
                foreach (String tag in tagsToWalkThrough) {
                    hitInvis = hitInvis || hitTopZ.collider.CompareTag(tag);
                }
                if (!hitInvis) { // if collision CAN'T be walked through, then don't move in z direction
                    move.z = 0;
                }
            } else if (checkBottomZ) { // do same thing for bottom of the body
                bool hitInvis = false;
                foreach (String tag in tagsToWalkThrough) {
                    hitInvis = hitInvis || hitBottomZ.collider.CompareTag(tag);
                }
                if (!hitInvis) { // if collision CAN'T be walked through, then don't move in z direction
                    move.z = 0;
                }
            }

            rb.MovePosition(rb.position + move);
            
        }
    }
}
