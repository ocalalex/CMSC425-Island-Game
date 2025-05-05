using System;
using UnityEngine;
using UnityEngine.Events;


public class FallerInWater : MonoBehaviour
{
    private Boolean firstFall = true;
    public UnityEvent FirstFallEvent;
    
    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 4) { // if collision is with water layer
            if (firstFall) { // dialogue if first time falling in
                firstFall = !firstFall;
                FirstFallEvent?.Invoke();
            }
            Teleporter teleporter = this.gameObject.GetComponent<Teleporter>();
            teleporter.Teleport();
        } 
    }
}
