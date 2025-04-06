using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class HeadTurn : MonoBehaviour
{
    public Key UpKeyNum = Key.W;
    public Key DownKeyNum = Key.S;
    public Key LftKeyNum = Key.A;
    public Key RgtKeyNum = Key.D;
    [Header("Turning Keys")]
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 50f;
    KeyControl upKey;
    KeyControl downKey;
    KeyControl lftKey;
    KeyControl rgtKey;

    private float nod = 0f; 
    private float turn = 0f; 
    void Start()
    {
        upKey = Keyboard.current[UpKeyNum];
        downKey = Keyboard.current[DownKeyNum];
        if (LftKeyNum != Key.None) lftKey = Keyboard.current[LftKeyNum];
        if (RgtKeyNum != Key.None) rgtKey = Keyboard.current[RgtKeyNum];

    }

    void Update()
    {
        if (downKey.isPressed) 
        {
            nod += speed *Time.deltaTime;

        }
        if (upKey.isPressed) 
        {
            nod -= speed *Time.deltaTime;
        }

        if (lftKey != null && lftKey.isPressed) 
        {
            turn -= speed *Time.deltaTime;

        }
        if (rgtKey != null && rgtKey.isPressed) 
        {
            turn += speed *Time.deltaTime;
        }

        nod = Mathf.Clamp(nod, -45f, 45f);
        turn = Mathf.Clamp(turn, -90f, 90f);

        transform.localRotation = Quaternion.Euler(nod, turn, 0f);
       
    }

}
