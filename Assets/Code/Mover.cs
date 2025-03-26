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
    float dps;
    void Start()
    {
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
            for (int i = 0; i < 30; i++){
                transform.Translate(0, 10f * Time.deltaTime, 0);
            }
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
