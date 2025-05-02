using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public int treeCount = 15;

    public Vector3 spawnCenter;
    public Vector3 spawnSize;

    public LayerMask landLayerMask;
    public float raycastMaxDistance = 150f;
    public float raycastOriginYOffset = 100f;

    public Transform treeContainer;

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
        
        while(spawnCount < treeCount){
            float randomX = Random.Range(-spawnSize.x /2f, spawnSize.x / 2f);
            float randomZ = Random.Range(-spawnSize.z /2f, spawnSize.z / 2f);

            Vector3 rayOrigin = spawnCenter + new Vector3(randomX, raycastOriginYOffset, randomZ);

            RaycastHit hit;
            if(Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastMaxDistance, landLayerMask)){
                Vector3 spawnPosition = hit.point;
                Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
                GameObject tree = Instantiate(treePrefab, spawnPosition, spawnRotation);
                spawnCount++;

                if(treeContainer != null){
                    tree.transform.SetParent(treeContainer);
                }
            }
        }
    }
}
