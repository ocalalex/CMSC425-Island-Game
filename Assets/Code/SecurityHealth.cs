using UnityEngine;
using System.Collections;

public class SecurityHealth : MonoBehaviour
{
    public int health = 5;
    public Color hurtColor = Color.red;
    public float hurtColorIntensity = 0.5f;
    public float hurtDur = 0.2f;

    private Renderer[] renderers;
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
        // if shot
        if (collision.gameObject.CompareTag("Bullet")) {
            health -= 1; // update health

            if (health == 0) {
                Destroy(this.gameObject); // if out of health, kill security
            } 
            StartCoroutine(HurtVisualization()); 
        }
    }

    // security guard glows for a duration when he gets hurt, then resets to default
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
