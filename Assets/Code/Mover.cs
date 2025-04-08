using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Mover : MonoBehaviour
{
    [Header("Turning Keys")]
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
    Rigidbody rb;

    [Header("Jumping")]
    public Key jmpKeyNum = Key.Space;
    public float jumpSize = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fdKey = Keyboard.current[fdKeyNum];
        bkKey = Keyboard.current[bkKeyNum];
        ltKey = Keyboard.current[ltKeyNum];
        rtKey = Keyboard.current[rtKeyNum];
        jmpKey = Keyboard.current[jmpKeyNum];
    }

    // Update is called once per frame
    void Update()
    {
        if (fdKey.isPressed)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);

        }
        if (bkKey.isPressed)
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        if (ltKey.isPressed)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (rtKey.isPressed)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (jmpKey.isPressed && onGround)
        {
            Vector3 impulse = Vector3.up;
            rb.AddForce(jumpSize*impulse, ForceMode.Impulse);
            onGround = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
}
