using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Turner : MonoBehaviour
{
    public Key raKeyNum = Key.RightArrow;
    public Key laKeyNum = Key.LeftArrow;
    KeyControl raKey;
    KeyControl laKey;
    [Header("Turning Keys")]
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float rpm = 10;
    float dps;
    void Start()
    {
        dps = 6 * rpm;
        raKey = Keyboard.current[raKeyNum];
        laKey = Keyboard.current[laKeyNum];
    }

    // Update is called once per frame
    void Update()
    {
        if (raKey.isPressed)
        {
            transform.Rotate(0, dps * Time.deltaTime, 0);
        }
        if (laKey.isPressed)
        {
            transform.Rotate(0, -dps * Time.deltaTime, 0);
        }
    }
}
