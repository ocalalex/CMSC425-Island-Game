using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class MoverOnLadder : MonoBehaviour
{
    [Header("Moving Keys")]
    public Key upKeyNum = Key.UpArrow;
    public Key downKeyNum = Key.DownArrow;
    public Key ltKeyNum = Key.LeftArrow;
    public Key rtKeyNum = Key.RightArrow;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 10f;
    KeyControl upKey;
    KeyControl downKey;
    KeyControl ltKey;
    KeyControl rtKey;

    private bool onGround = true;

    void Start()
    {
        upKey = Keyboard.current[upKeyNum];
        downKey = Keyboard.current[downKeyNum];
        ltKey = Keyboard.current[ltKeyNum];
        rtKey = Keyboard.current[rtKeyNum];
    }

    // Update is called once per frame
    void Update()
    {
        if (upKey.isPressed)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            if (onGround) {
                onGround = !onGround;
            }

        }
        if (downKey.isPressed)
        {
            if (onGround) {
                transform.Translate(0, 0, -speed * Time.deltaTime);
            } else {
                transform.Translate(0, -speed * Time.deltaTime, 0);
            }
        }
        if (ltKey.isPressed)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (rtKey.isPressed)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LadderTop"))
        {
            Debug.Log("Reached the top");
            Rigidbody playerRB = GetComponent<Rigidbody>();
            playerRB.isKinematic = false;

            GetComponent<Mover>().enabled = true;
            GetComponent<MoverOnLadder>().enabled = false;
        }
        else if (other.gameObject.CompareTag("LadderBottom"))
        {
            Debug.Log("At ground");

            onGround = true;
        }
    }
}
