using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/*This file is to create the map item functionality when clicking M*/

public class Mapper : MonoBehaviour
{

    public Inventory inventory;
    public GameObject map, userMarker, keyMarkerText, keyMarkerDot, spawnMarkerText, spawnMarkerDot, grannyMarkerText,
     grannyMarkerDot, cargoMarkerText, cargoMarkerDot, factMarkerText, factMarkerDot, lightMarkerText, lightMarkerDot, boatMarkerText, boatMarkerDot;

    public Key mapKeyNum = Key.M;
    public bool inMapView = false;

    public Camera mainCamera;
    public Camera mapCamera;

    KeyControl mapKey;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyMarkerText.GetComponent<Renderer>().enabled = false;
        keyMarkerDot.GetComponent<Renderer>().enabled = false;
        spawnMarkerText.GetComponent<Renderer>().enabled = false;
        spawnMarkerDot.GetComponent<Renderer>().enabled = false;
        grannyMarkerText.GetComponent<Renderer>().enabled = false;
        grannyMarkerDot.GetComponent<Renderer>().enabled = false;
        cargoMarkerText.GetComponent<Renderer>().enabled = false;
        cargoMarkerDot.GetComponent<Renderer>().enabled = false;
        factMarkerText.GetComponent<Renderer>().enabled = false;
        factMarkerDot.GetComponent<Renderer>().enabled = false;
        lightMarkerText.GetComponent<Renderer>().enabled = false;
        lightMarkerDot.GetComponent<Renderer>().enabled = false;
        boatMarkerText.GetComponent<Renderer>().enabled = false;
        boatMarkerDot.GetComponent<Renderer>().enabled = false;
        userMarker.GetComponent<Renderer>().enabled = false;
        mainCamera.enabled = true;
        mapCamera.enabled = false;
        mapKey = Keyboard.current[mapKeyNum];
    }

    // Update is called once per frame
    void Update()
    {
        if (mapKey.wasReleasedThisFrame)
        {
            if (inventory.CheckItem(map)) 
            {
                keyMarkerText.GetComponent<Renderer>().enabled = !keyMarkerText.GetComponent<Renderer>().enabled;
                keyMarkerDot.GetComponent<Renderer>().enabled = !keyMarkerDot.GetComponent<Renderer>().enabled;
                userMarker.GetComponent<Renderer>().enabled = !userMarker.GetComponent<Renderer>().enabled;
                spawnMarkerText.GetComponent<Renderer>().enabled = !spawnMarkerText.GetComponent<Renderer>().enabled;
                spawnMarkerDot.GetComponent<Renderer>().enabled = !spawnMarkerDot.GetComponent<Renderer>().enabled;
                grannyMarkerText.GetComponent<Renderer>().enabled = !grannyMarkerText.GetComponent<Renderer>().enabled;
                grannyMarkerDot.GetComponent<Renderer>().enabled = !grannyMarkerDot.GetComponent<Renderer>().enabled;
                cargoMarkerText.GetComponent<Renderer>().enabled = !cargoMarkerText.GetComponent<Renderer>().enabled;
                cargoMarkerDot.GetComponent<Renderer>().enabled = !cargoMarkerDot.GetComponent<Renderer>().enabled;
                factMarkerText.GetComponent<Renderer>().enabled = !factMarkerText.GetComponent<Renderer>().enabled;
                factMarkerDot.GetComponent<Renderer>().enabled = !factMarkerDot.GetComponent<Renderer>().enabled;
                lightMarkerText.GetComponent<Renderer>().enabled = !lightMarkerText.GetComponent<Renderer>().enabled;
                lightMarkerDot.GetComponent<Renderer>().enabled = !lightMarkerDot.GetComponent<Renderer>().enabled;
                boatMarkerText.GetComponent<Renderer>().enabled = !boatMarkerText.GetComponent<Renderer>().enabled;
                boatMarkerDot.GetComponent<Renderer>().enabled = !boatMarkerDot.GetComponent<Renderer>().enabled;
                mainCamera.enabled = !mainCamera.enabled;
                mapCamera.enabled = !mapCamera.enabled;
            }
        }
        
    }
}
