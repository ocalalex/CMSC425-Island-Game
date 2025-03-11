using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Mover : MonoBehaviour
{
    public Key fdKeyNum = Key.UpArrow;
    public Key bkKeyNum = Key.DownArrow;
    [Header("Turning Keys")]
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 10;
    KeyControl fdKey;
    KeyControl bkKey;
    float dps;
    void Start()
    {
        fdKey = Keyboard.current[fdKeyNum];
        bkKey = Keyboard.current[bkKeyNum];
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
    }
}
