using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Mapper : MonoBehaviour
{

    public Inventory inventory;
    public GameObject map;
    public GameObject userMarker;
    public GameObject keyMarkerText, keyMarkerDot, spawnMarkerText, spawnMarkerDot, grannyMarkerText,
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
        mapKey = Keyboard.current[mapKeyNum];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mapKey.wasReleasedThisFrame)
        {
            if (inventory.CheckItem(map)) 
            {
                if (inMapView)
                {
                    inMapView = false;
                    keyMarkerText.GetComponent<Renderer>().enabled = false;
                    keyMarkerDot.GetComponent<Renderer>().enabled = false;
                    userMarker.GetComponent<Renderer>().enabled = false;
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
                } else {
                    inMapView = true;
                    mainCamera.enabled = false;
                    keyMarkerText.GetComponent<Renderer>().enabled = true;
                    keyMarkerDot.GetComponent<Renderer>().enabled = true;
                    userMarker.GetComponent<Renderer>().enabled = true;
                    spawnMarkerText.GetComponent<Renderer>().enabled = true;
                    spawnMarkerDot.GetComponent<Renderer>().enabled = true;
                    grannyMarkerText.GetComponent<Renderer>().enabled = true;
                    grannyMarkerDot.GetComponent<Renderer>().enabled = true;
                    cargoMarkerText.GetComponent<Renderer>().enabled = true;
                    cargoMarkerDot.GetComponent<Renderer>().enabled = true;
                    factMarkerText.GetComponent<Renderer>().enabled = true;
                    factMarkerDot.GetComponent<Renderer>().enabled = true;
                    lightMarkerText.GetComponent<Renderer>().enabled = true;
                    lightMarkerDot.GetComponent<Renderer>().enabled = true;
                    boatMarkerText.GetComponent<Renderer>().enabled = true;
                    boatMarkerDot.GetComponent<Renderer>().enabled = true;
                    userMarker.GetComponent<Renderer>().enabled = true;
                    mapCamera.enabled = true;
                }
            }
        }
        
    }
}
