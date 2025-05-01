using UnityEngine;
using System.Collections;

public class SecurityHealth : MonoBehaviour
{
    public int health = 5;
    public Color hurtColor = Color.red;
    public float hurtColorIntensity = 0.5f;
    public float hurtDur = 0.2f;

    private Renderer[] renderers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>(); 

        // Enable emission for all children renderers
        foreach (var renderer in renderers)
        {
            renderer.material.EnableKeyword("_EMISSION");
        }
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) {
            Debug.Log("Bullet");
            health -= 1;

            if (health == 0) {
                Destroy(this.gameObject);
            }
            StartCoroutine(HurtVisualization());
        }
    }

    IEnumerator HurtVisualization(){
        foreach (var renderer in renderers)
        {
            renderer.material.SetColor("_EmissionColor", hurtColor*hurtColorIntensity);
        }
        yield return new WaitForSeconds(hurtDur);
        foreach (var renderer in renderers)
        {
            renderer.material.SetColor("_EmissionColor", Color.black);
        }
    }
}
