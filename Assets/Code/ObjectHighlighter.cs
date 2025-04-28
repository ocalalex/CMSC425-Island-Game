using UnityEngine;

public class ObjectHighlighter : MonoBehaviour
{

    public GameObject playerObject;
    public float highlightDistance = 50f;
    private Transform player;
    private Renderer[] renderers;

    public Color highlightColor = Color.grey;
    public float highlightIntensity = 0.5f;

    private int objectsLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = playerObject.transform;
        objectsLayer = LayerMask.GetMask("Objects");

        renderers = GetComponentsInChildren<Renderer>(); 

        // Enable emission keyword for all materials
        foreach (var renderer in renderers)
        {
            renderer.material.EnableKeyword("_EMISSION");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null) { // checks that the camera attached to the player is the active camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, highlightDistance, objectsLayer)) {
                if (hit.transform == transform) {
                    foreach (var renderer in renderers)
                    {
                        renderer.material.SetColor("_EmissionColor", highlightColor*highlightIntensity);
                    }
                } else {
                    foreach (var renderer in renderers)
                    {
                        renderer.material.SetColor("_EmissionColor", Color.black);
                    }
                }
            } else {
                foreach (var renderer in renderers)
                {
                    renderer.material.SetColor("_EmissionColor", Color.black);
                }
            }
        }
    }
}
