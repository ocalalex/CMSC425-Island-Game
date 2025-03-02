using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CamMover : MonoBehaviour
{
    public Key forwardKey = Key.W;
    public Key backwardKey = Key.S;
    public Key leftKey = Key.A;
    public Key rightKey = Key.D;   
    public float speed = 5;

    KeyControl fdKey;
    KeyControl bkKey;
    KeyControl ltKey;
    KeyControl rtKey;

    void Start()
    {
        fdKey = Keyboard.current[forwardKey];
        bkKey = Keyboard.current[backwardKey];
        ltKey = Keyboard.current[leftKey];
        rtKey = Keyboard.current[rightKey];
    }

    void Update()
    {
        if(fdKey.isPressed){
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if(bkKey.isPressed){
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        if(ltKey.isPressed){
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if(rtKey.isPressed){
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

    }
}
