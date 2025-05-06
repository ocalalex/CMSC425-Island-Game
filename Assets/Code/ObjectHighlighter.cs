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

        // Enable emission for all materials in all children renderers - allows the materials to emit a light
        foreach (var renderer in renderers)
        {
            foreach (Material material in renderer.materials) {
                material.EnableKeyword("_EMISSION");
            }
        }
    }

    void Update()
    {
        // checks that the camera attached to the player is the active camera
        if (Camera.main != null) { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if mouse is hovering over an object within specified distance
            if (Physics.Raycast(ray, out hit, highlightDistance, objectsLayer)) {
                // if the object is THIS object, update each material in object to emit light to "highlight" it
                if (hit.transform == transform) {
                    foreach (var renderer in renderers)
                    {
                        foreach (Material material in renderer.materials) {
                            material.SetColor("_EmissionColor", highlightColor*highlightIntensity);
                        }
                    }
                } else { // if object is NOT this object, update materials light emission to black, which is default
                    foreach (var renderer in renderers)
                    {
                        foreach (Material material in renderer.materials) {
                            material.SetColor("_EmissionColor", Color.black);
                        }
                    }
                }
            } else { // if mouse is not hovering over any object
                foreach (var renderer in renderers)
                {
                    foreach (Material material in renderer.materials) {
                        material.SetColor("_EmissionColor", Color.black);
                    }
                }
            }
        }
    }
}
