using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Jumper : MonoBehaviour
{
    [Header("Jumping")]
    public Key jmpKeyNum = Key.Space;
    public float jumpSize = 5;

    KeyControl jmpKey;

    Rigidbody rb;
    private bool onGround = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        jmpKey = Keyboard.current[jmpKeyNum];
        
    }

    void Update()
    {
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
                if (contact.normal.y > 0.5f)
                {
                    onGround = true;
                    return;
                }
            }
        }
    }
}
