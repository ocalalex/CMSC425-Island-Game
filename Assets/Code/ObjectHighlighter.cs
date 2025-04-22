using UnityEngine;

public class ObjectHighlighter : MonoBehaviour
{

    public GameObject playerObject;
    public float highlightDistance = 20f;
    private Transform player;
    private Renderer[] renderers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = playerObject.transform;
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
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= highlightDistance)
        {
            foreach (var renderer in renderers)
            {
                renderer.material.SetColor("_EmissionColor", Color.grey*0.5f);
            }
        } else {
            foreach (var renderer in renderers)
            {
                renderer.material.SetColor("_EmissionColor", Color.black);
            }
        }
    }
}
