using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Mover : MonoBehaviour
{
    public Key fdKeyNum = Key.UpArrow;
    public Key bkKeyNum = Key.DownArrow;
    public Key jmpKeyNum = Key.Space;
    private bool onGround = true;
    [Header("Turning Keys")]
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 10f;
    KeyControl fdKey;
    KeyControl bkKey;
    KeyControl jmpKey;
    Rigidbody rb;
    float dps;
    public float jumpSize = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fdKey = Keyboard.current[fdKeyNum];
        bkKey = Keyboard.current[bkKeyNum];
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
