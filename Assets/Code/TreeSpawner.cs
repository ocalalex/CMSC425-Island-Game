using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [Header("General")]
    public GameObject treePrefab;
    public int treeCount = 15;
    public int maxAttemptsFactor = 10;  //prevents infinite loops when spawning trees

    public Transform treeContainer;
    public LayerMask landLayerMask; //layer in which the trees will be spawned

    [Header("Spawn Area")]
    public Vector3 spawnCenter;
    public Vector3 spawnSize;
    public float raycastMaxDistance = 150f; //max distance to check for ground position
    public float raycastOriginYOffset = 100f;   //start height of raycast to find ground position

    [Header("Edge Checking")]
    public float edgeRayYOffset = 1f; //start height of the edge checking ray when determining stable ground
    public float edgeRayMaxDistance = 2f; //max distance to check for stable ground
    public float edgeClearance = 1f; //distance to check for stable ground
    public float treeClearance = 1f;    //distance to check for other objects when spawning trees

    void Start()
    {
        if(treePrefab == null){
            Debug.LogError("Tree Prefab is not assigned in the inspector.");
            return;
        }
        if(landLayerMask == 0){
            Debug.LogWarning("Land Layer Mask is not assigned in the inspector.");
        }

        spawnTrees();
    }

    void spawnTrees(){
        int spawnCount = 0;
        int attempts = 0;
        int maxAttempts = treeCount * maxAttemptsFactor; //max attempts to find a spawn position
        Vector3[] checkDirections = {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right
        };

        //inverts the landLayerMask to ignore when checking for collisions with other objects
        LayerMask layersToIgnore = ~landLayerMask;  
        
        while(attempts < maxAttempts && spawnCount < treeCount){
            float randomX = Random.Range(-spawnSize.x /2f, spawnSize.x / 2f);
            float randomZ = Random.Range(-spawnSize.z /2f, spawnSize.z / 2f);

            Vector3 rayOrigin = spawnCenter + new Vector3(randomX, raycastOriginYOffset, randomZ);

            //casts a ray to find the ground position
            RaycastHit hit;
            if(Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastMaxDistance, landLayerMask)){
                Vector3 spawnPosition = hit.point;
                bool isStable = true;
                Vector3 checkBase = spawnPosition + Vector3.up * edgeRayYOffset;

                //determines if the ground around the spawn position is stable in all directions
                foreach (Vector3 dir in checkDirections){
                    Vector3 edgePoint = checkBase + dir * edgeClearance;

                    //checks if the ray hits the ground in the direction of the edge
                    if(!Physics.Raycast(edgePoint, Vector3.down, edgeRayMaxDistance, landLayerMask)){
                        isStable = false;
                        break;
                    }
                }

                //checks if the spawn position is clear of other objects
                if(isStable && !Physics.CheckSphere(spawnPosition, treeClearance, layersToIgnore)){
                    Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
                    GameObject tree = Instantiate(treePrefab, spawnPosition, spawnRotation);
                    spawnCount++;

                    //organizes tree in the treeContainer parent
                    if(treeContainer != null){
                        tree.transform.SetParent(treeContainer);
                    }
                }
            }

            attempts++;
        }
        if(attempts >= maxAttempts){
            Debug.LogWarning("Max attempts reached. Some trees may not be spawned.");
        }
    }
}
