using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpener : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float angleOpened = 110;
    public float angleClosed = 0;
    public float flapTime = 3f;
    Quaternion rotOpened;
    Quaternion rotClosed;
    RevCo revCo;
    void Start()
    {
        rotOpened = Quaternion.Euler(0, angleOpened, 0);
        rotClosed = Quaternion.Euler(0, angleOpened, 0);
        revCo = gameObject.AddComponent<RevCo>();
        revCo.Init(OpenDoor);
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        revCo.Action();
    }
    private void OpenDoor(float t)
    {
        transform.localRotation = Quaternion.Lerp(rotClosed, rotOpened, t);
    }
}
