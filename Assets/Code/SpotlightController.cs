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

    void Start()
    {
        spotLight = GetComponent<Light>();
        spottedOnce = false;
    }

    void Update()
    {
        
        Vector3 forwardDir = transform.forward;
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
            float inclination = t * halfAngle;
            float azimuth = 2 * Mathf.PI * i / goldenRatio;
            
            // Convert spherical to cartesian coordinates
            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);
            Vector3 rayDir = transform.TransformDirection(new Vector3(x, y, z));
            rayDir.Normalize();

            if(Physics.Raycast(transform.position, rayDir, out RaycastHit hit, maxDistance)){
                    
                    if(debugRays){
                      Debug.DrawRay(transform.position, rayDir * hit.distance, Color.blue);
                    //   Debug.Log("Ray hit: " + hit.collider.gameObject.name);
                    }

                    if((detectionLayer & (1 << hit.collider.gameObject.layer)) != 0){
                        Teleporter teleporter = hit.collider.GetComponent<Teleporter>();

                        if(!actionTriggered && teleporter != null && dialogue != null){
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

                        // Debug.Log("Spotlight detected: " + hit.collider.gameObject.name);
                        if (debugRays)
                        {
                            Debug.DrawRay(transform.position, rayDir * hit.distance, Color.green);
                        }

                    }

                }else if (debugRays){
                    Debug.DrawRay(transform.position, rayDir * maxDistance, Color.red);
                }
        }
    }
}
