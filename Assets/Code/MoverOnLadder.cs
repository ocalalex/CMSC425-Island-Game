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
            // if onGround, downKey will move you backwards away from ladder
            if (onGround) {
                transform.Translate(0, 0, -speed * Time.deltaTime);
            } else { // else, downKey will move you down the ladder
                transform.Translate(0, -speed * Time.deltaTime, 0);
            }
        }
        // left and right controls are the same as Mover
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
        // if player reaches top of ladder, return player control to the normal Mover and take away control from Ladder Mover
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
            onGround = true;
        }
    }
}
