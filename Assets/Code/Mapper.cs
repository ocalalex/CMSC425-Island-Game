using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/*This file is to create the map item functionality when clicking M. The map is represented a camera ontop of the island, 
and markers as game objects*/

public class Mapper : MonoBehaviour
{

    public Inventory inventory;
    public GameObject map, userMarker, keyMarkerText, keyMarkerDot, spawnMarkerText, spawnMarkerDot, grannyMarkerText,
     grannyMarkerDot, cargoMarkerText, cargoMarkerDot, factMarkerText, factMarkerDot, lightMarkerText, lightMarkerDot, boatMarkerText, boatMarkerDot;
    public Key mapKeyNum = Key.M;
    public Camera mainCamera;
    public Camera mapCamera;

    KeyControl mapKey;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Make all the gameobject markers for the map to be invisible
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
        //Set the map camera intially off
        mainCamera.enabled = true;
        mapCamera.enabled = false;
        //Get the current key, which should be M
        mapKey = Keyboard.current[mapKeyNum];
    }

    //Update is called once per frame
    void Update()
    {
        //Check if the map key is clicked and released in the current frame
        if (mapKey.wasReleasedThisFrame)
        {
            //Check if the map item is picked up and located in inventoy
            if (inventory.CheckItem(map)) 
            {
                //Swap the visibility of the map markers and the camera between the user camera and map camera
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
