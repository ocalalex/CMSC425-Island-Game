using System;
using UnityEngine;

public class SpotlightDetection : MonoBehaviour
{
    public float maxDistance = 20f;
    public int rayCount = 30;
    public float angleSpread = 20f;
    public LayerMask detectionLayer;
    public bool debugRays = true;
    public Dialogue dialogue;

    private Light spotLight;
    private Boolean spottedOnce;

    private LayerMask notLightHouse;
    public int uncutWires = 3;

    void Start()
    {
        spotLight = GetComponent<Light>();
        spottedOnce = false;
        notLightHouse = ~LayerMask.GetMask("Lighthouse");
    }

    void Update()
    {
        // shuts off light if all of the wires have been cut
        if (uncutWires == 0) {
            Destroy(this.gameObject);
        }
        
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        bool actionTriggered = false;
        
        //fibonacci spiral
        for(int i = 0; i < rayCount; i++){
            //ensures only a single action is triggered for all rays
            if(actionTriggered){
                break;
            }

            float t = (float)i / rayCount;
            float halfAngle = angleSpread * 0.5f * Mathf.Deg2Rad;

            //establishes the spherical coordinates of the ray
            float inclination = t * halfAngle;
            float azimuth = 2 * Mathf.PI * i / goldenRatio;
            
            // Convert spherical to cartesian coordinates
            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);
            Vector3 rayDir = transform.TransformDirection(new Vector3(x, y, z));
            rayDir.Normalize();

            if(Physics.Raycast(transform.position, rayDir, out RaycastHit hit, maxDistance, notLightHouse)){ // layermask ignores the cosmetic lighthouse objects that immediately block the ray
                //draws blue ray if a hit is detected
                if(debugRays){
                    Debug.DrawRay(transform.position, rayDir * hit.distance, Color.blue);
                }

                // Check if the hit object is in the detection layer
                if((detectionLayer & (1 << hit.collider.gameObject.layer)) != 0){
                    Teleporter teleporter = hit.collider.GetComponent<Teleporter>();

                    // Checks if hit object has teleporter and dialogue component
                    //Also checks if prior rays of the same update() have already triggered the action
                    if(!actionTriggered && teleporter != null && dialogue != null){

                        //teleports and starts dialoging if the object is not already spotted
                        if(!spottedOnce){
                            spottedOnce = true;
                            dialogue.TriggerSpottedLine();  
                        }
                        actionTriggered = true;
                        teleporter.Teleport();
                        if(!spottedOnce){
                            dialogue.isSpotted = false;
                        }
                        

                    }

                    //draws green ray if object his is in detection layer
                    if (debugRays)
                    {
                        Debug.DrawRay(transform.position, rayDir * hit.distance, Color.green);
                    }

                }

            //draws red ray if no collision is detected
            }else if (debugRays){
                Debug.DrawRay(transform.position, rayDir * maxDistance, Color.red);
            }
        }
    }
}
