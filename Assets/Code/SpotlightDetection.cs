using UnityEngine;

public class SpotlightDetection : MonoBehaviour
{
    public float maxDistance = 20f;
    public int rayCount = 30;
    public float angleSpread = 20f;
    public LayerMask detectionLayer;
    public bool debugRays = true;

    private Light spotLight;

    void Start()
    {
        spotLight = GetComponent<Light>();
    }

    void Update()
    {
        
        Vector3 forwardDir = transform.forward;
        float coneRadius = Mathf.Tan(angleSpread * 0.5f * Mathf.Deg2Rad);
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        
        //fibonacci spiral
        for(int i = 0; i < rayCount; i++){
            float theta = 2 * Mathf.PI * i / goldenRatio;
            float r = coneRadius * Mathf.Sqrt(i) / Mathf.Sqrt(rayCount);
            float x = r * Mathf.Cos(theta);
            float y = r * Mathf.Sin(theta);
            Vector3 rayDir = forwardDir + new Vector3(x, y, 0);
            rayDir.Normalize();

            if(Physics.Raycast(transform.position, rayDir, out RaycastHit hit, maxDistance)){
                    
                    if(debugRays){
                      Debug.DrawRay(transform.position, rayDir * hit.distance, Color.blue);
                      Debug.Log("Ray hit: " + hit.collider.gameObject.name);
                    }

                    if((detectionLayer & (1 << hit.collider.gameObject.layer)) != 0){
                        Debug.Log("Spotlight detected: " + hit.collider.gameObject.name);
                        if (debugRays)
                        {
                            Debug.DrawRay(transform.position, rayDir * hit.distance, Color.green);
                        }
                    }

                }else if (debugRays){
                    Debug.DrawRay(transform.position, rayDir * maxDistance, Color.red);
                }
        }

        


        // int tempRayCount = Mathf.FloorToInt(Mathf.Sqrt(rayCount));
        // float angleStep = angleSpread / tempRayCount;

        // for(int i = -tempRayCount/2; i <= tempRayCount/2; i++){
        //     for(int j = -tempRayCount/2; j <= tempRayCount / 2; j++){

        //         float horizontalAngle = i * angleStep;
        //         float verticalAngle = j * angleStep;

        //         Vector3 rayDir = Quaternion.Euler(verticalAngle, horizontalAngle, 0) * forwardDir;

                
        //         if(Physics.Raycast(transform.position, rayDir, out RaycastHit hit, maxDistance)){
                    
        //             if(debugRays){
        //               Debug.DrawRay(transform.position, rayDir * hit.distance, Color.blue);
        //               Debug.Log("Ray hit: " + hit.collider.gameObject.name);
        //             }

        //             if((detectionLayer & (1 << hit.collider.gameObject.layer)) != 0){
        //                 Debug.Log("Spotlight detected: " + hit.collider.gameObject.name);
        //                 if (debugRays)
        //                 {
        //                     Debug.DrawRay(transform.position, rayDir * hit.distance, Color.green);
        //                 }
        //             }

        //         }else if (debugRays){
        //             Debug.DrawRay(transform.position, rayDir * maxDistance, Color.red);
        //         }
        //     }

        // }
    }
}
