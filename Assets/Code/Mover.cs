using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Mover : MonoBehaviour
{
    public float speed = 5.0f;
    public Key fdKeyNum = Key.W;
    public Key bkKeyNum = Key.S;
    public Key ltKeyNum = Key.A;
    public Key rtKeyNum = Key.D;

    private KeyControl fdKey;
    private KeyControl bkKey;
    private KeyControl ltKey;
    private KeyControl rtKey;

    void Start()
    {
        fdKey = Keyboard.current[fdKeyNum];
        bkKey = Keyboard.current[bkKeyNum];
        ltKey = Keyboard.current[ltKeyNum]; 
        rtKey = Keyboard.current[rtKeyNum];
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
